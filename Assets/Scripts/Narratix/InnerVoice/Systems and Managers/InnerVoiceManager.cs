using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YBC.Narratix;
using YBC.Utils;

namespace YBC.Narratix.InnerVoice
{
	public class InnerVoiceManager : MonoBehaviour
	{

		private VoiceItemCollection audioCollection;
		public NeemotixAdapter neemotixAdapter;
		private int[] neemotionList;
		private YouBeRandom r = new YouBeRandom();
		private AudioSource audioSource;
		private ItemPool pool;

		public float laberlust = 1;
		public int initialPause = 111;
		private float timeTillNext;
		private float timeSinceLast;


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

			if( timeTillNext < timeSinceLast )
			{
				if( pool.isEmpty() )
				{
					PopulateItemPool();
					
				}
				else
				{
					audioSource.clip = pool.PickNextClip();
					audioSource.Play();
					timeSinceLast = 0;

					timeTillNext = 2 * pool.GetTotalWeight() / laberlust;
				}
			}

		}


		/// <summary>
		/// Pupulates a fresh instance of itemPool. Querrys the Neemotions and picks Variants according to status. rolls Dice to determine if the variant get's pushed to the stack.
		/// </summary>
		private void PopulateItemPool()
		{
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

					// Roll Dice to choose if it should be said
					bool sayit = r.HitMe( item.Weight );

					if( sayit )
					{
						pool.Add( item );
					}
				}
			}

			foreach ( var item in pool )
			{
				Debug.Log( item.Text );
			}
		}

	} 
}
