using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using YBC.Neemotix;
using YBC.Utils;
using YBC.Utils.Error;

namespace YBC.Narratix.InnerVoice
{
	public class InnerVoiceManager : MonoBehaviour
	{

		private VoiceItemCollection audioCollection;

		public GameObject neemotixAdapterObject;
		private INeemotixAdapter neemotixAdapter;

		private int[] neemotionList;
		private YouBeRandom r = new YouBeRandom();
		private AudioSource audioSource;
		private ItemPool pool;

		public float minimumPauseSeconds = 0.1f;
		[Range( 0f, 1f )]
		public float intervalRandomnes = 0.0f;
		private float randomInterval = 0f;
		public int initialPause = 5;
		private float timeTillNext;
		private float timeSinceLast;

		[Space]
		[Header( "Pool Options" )]

		public bool kickLeastImportant = true;

		[Space]
		[Header( "Debug Stuff" )]

		public float debugTimeTillNext;
		public float debugPause;

		private void OnValidate()
		{
			bool success = false;
			if ( neemotixAdapterObject != null )
			{

				Component[] cpnts = neemotixAdapterObject.GetComponents( typeof( Component ) );

				foreach ( var cpnt in cpnts )
				{
					try
					{
						neemotixAdapter = (INeemotixAdapter)cpnt;
						success = true;
					}
					catch ( System.Exception ) { }
				}
			}
			else
			{
				Debug.LogError( new YBCEditorNotAssignedError().ToString() );
				return;
			}
			if ( !success )
			{
				Debug.LogError( new YBCEditorInterfaceObjNotParseableError().ToString() );
			}
		}


		

		// Start is called before the first frame update
		void Start()
		{
			timeTillNext = (float)initialPause;
			pool = new ItemPool();

			audioSource = GetComponent<AudioSource>();
			audioCollection = new VoiceItemCollection( neemotixAdapter.GetAllNemotionIDs() );
			neemotionList = neemotixAdapter.GetAllNemotionIDs();

		}



		// Update is called once per frame
		void Update()
		{
			timeSinceLast += Time.deltaTime;

			if( timeTillNext + randomInterval < timeSinceLast )
			{
				if ( pool.isEmpty() )
				{
					PopulateItemPool();

					SetTimeTillNext();

				}
				else if ( !audioSource.isPlaying )
				{
					audioSource.clip = pool.PickNextClip();
					audioSource.Play();
					timeSinceLast = 0;

					//timeTillNext = minimumPauseSeconds / (1f + pool.GetAverageWeight());
					SetRandomInterval();

					debugPause = timeTillNext;
				}
			}


			debugTimeTillNext = timeTillNext - timeSinceLast;

		}


		private void SetTimeTillNext()
		{
			timeTillNext = minimumPauseSeconds / (0.1f + pool.GetAverageWeight());
		}

		private void SetRandomInterval()
		{	
			YouBeRandom r = new YouBeRandom();
			randomInterval = r.FloatZeroTo( intervalRandomnes * debugTimeTillNext );
		}




		/// <summary>
		/// Pupulates a fresh instance of itemPool. Querrys the Neemotions and picks Variants according to status. rolls Dice to determine if the variant get's pushed to the stack.
		/// </summary>
		private void PopulateItemPool()
		{
			if ( neemotionList == null ) return;

			Debug.Log( "##### new innerVoice Stack #####" );
			pool = new ItemPool();

			foreach ( int neemoID in neemotionList )
			{
				var neemoState = neemotixAdapter.GetNeemotionStateByID( neemoID );

				// Get a List of VoiceItems fitting the Neemotion and it's state.
				VoiceItem[] tmp = audioCollection.GetItemsForNeemotion( neemoID, neemoState.Item2);

				if( tmp.Length > 0 )
				{
					// Roll Dice and pick one!
					int which = r.RollZero( tmp.Length );
					VoiceItem item = tmp[which];

					if( !kickLeastImportant )
					{
						// Roll Dice to choose if it should be said
						bool sayit = r.HitMe( item.Weight );

						if ( sayit )
						{
							pool.Add( item );
						}
					} else
					{
						// we kick the least important items later anyway, so might as well add all of them in here
						pool.Add( item );
					}
				}
			}

			if( kickLeastImportant )
			{
				KickLeastImportantFromPool();
			}

			foreach ( var item in pool )
			{
				Debug.Log( item.Weight +" - " + item.Text );
			}
		}



		private void KickLeastImportantFromPool()
		{
			float averageWeight = pool.GetAverageWeight();
			List<VoiceItem> toDelete = new List<VoiceItem>();

			foreach ( var item in pool )
			{
				if ( item.Weight < averageWeight )
				{
					toDelete.Add( item );
				}
			}
			foreach ( var deleteItem in toDelete )
			{
				Debug.Log( "DEL: " + deleteItem.Weight + " - " + deleteItem.Text );
				pool.Remove( deleteItem );
			}
		}
	} 
}
