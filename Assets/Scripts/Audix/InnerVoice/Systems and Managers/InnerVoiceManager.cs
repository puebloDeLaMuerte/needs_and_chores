using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using UnityEditor.Searcher;
using UnityEngine;
using YBC.Neemotix;
using YBC.Utils;
using YBC.Utils.Error;

namespace YBC.Audix.InnerVoice
{
	public class InnerVoiceManager : MonoBehaviour
	{
		public List<InnerVoiceItemCollection> soundItemCollections;
		//public SoundItemCollection audioCollection;

		[Space]



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



		// Start is called before the first frame update
		void Start()
		{

			timeTillNext = (float)initialPause;
			pool = new ItemPool();

			audioSource = GetComponent<AudioSource>();

			foreach ( InnerVoiceItemCollection itemcollection in soundItemCollections )
			{
				itemcollection.Init();
			}
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

					// TEMP:
					SetTimeTillNext();



					debugPause = timeTillNext;
				}
			}


			debugTimeTillNext = timeTillNext - timeSinceLast;
		}


		private void SetTimeTillNext()
		{
			timeTillNext = minimumPauseSeconds / ( 0.1f + pool.GetAverageWeight() );
		}

		private void SetRandomInterval()
		{	
			randomInterval = YouBeRandom.Instance.FloatZeroTo( intervalRandomnes * debugTimeTillNext );
		}



		private void PopulateItemPool()
		{
			pool = new ItemPool();

			foreach ( InnerVoiceItemCollection collection in soundItemCollections )
			{
				PopulateItemPool( ref pool, collection );
			}
		}
			
			
		

		/// <summary>
		/// Pupulates a fresh instance of itemPool. Querrys the Neemotions and picks Variants according to status. rolls Dice to determine if the variant get's pushed to the stack.
		/// </summary>
		private void PopulateItemPool( ref ItemPool pool, InnerVoiceItemCollection audioCollection )
		{
			IInnerVoiceDataAdapter dataAdapter = audioCollection.getDataAdapter();

			foreach ( var ids in dataAdapter.getAllSelectorIDs() )
			{
				List<InnerVoiceItem> itemsForDataPoint = new List<InnerVoiceItem>();
				
				int id = ids.Item1;

				foreach ( InnerVoiceItem item in audioCollection.GetItems() )
				{

					// For each collectoin:
					// Get all IDs from it's DataAdapter and cycle through each item in the collection to find matching ids/value constellations.

					if( item.Selectors[0].EvaluateID( id ) )
					{

						bool success = true;
						int depth = 0;
						foreach ( Selector sel in item.Selectors )
						{
							switch ( sel.selectorType )
							{
								case SelectorType.FLOAT:
									float f = dataAdapter.getFloatForSelectorID( id, depth );
									success = sel.Evaluate( f );
									break;
								case SelectorType.INT:
									int i = dataAdapter.getIntForSelectorID( id, depth );
									success = sel.Evaluate( i );
									break;
								case SelectorType.STRING:
									success = sel.Evaluate( dataAdapter.getStringForSelectorID( id, depth ) );
									break;
								default:
									break;
							}
						
							if ( !success )
							{
								break;
							}
							depth++;
						}
						if ( success )
						{
							// put item on pool
							itemsForDataPoint.Add( item );
						}
					}
				}
				// deal with variants here!

				if( itemsForDataPoint.Count > 1 )
				{
					InnerVoiceItem pick = PickLeastPlayed( itemsForDataPoint );
					pool.AddItem( pick );
					Debug.Log( "pooling: " + pick.Selectors[0] + " - variant: " + pick.Variant);
				}
			}
		}


		
		private InnerVoiceItem PickLeastPlayed( List<InnerVoiceItem> itemList ) 
		{
			if ( itemList.Count == 1 ) return itemList[0];


			InnerVoiceItem randomItem = itemList[ YouBeRandom.Instance.Roll(0, itemList.Count )];
			InnerVoiceItem removeItem = itemList.Find( x => x.getPickedCount() > randomItem.getPickedCount() );

			if( removeItem == null )
			{
				itemList.Remove( randomItem );
			} else
			{
				itemList.Remove( removeItem );
			}

			return PickLeastPlayed(itemList);
		}



		private void KickLeastImportantFromPool()
		{
			float averageWeight = pool.GetAverageWeight();
			List<InnerVoiceItem> toDelete = new List<InnerVoiceItem>();

			foreach ( var item in pool )
			{
				if ( item.Urgency < averageWeight )
				{
					toDelete.Add( item );
				}
			}
			foreach ( var deleteItem in toDelete )
			{
				Debug.Log( "DEL: " + deleteItem.Urgency + " - " + deleteItem.Text );
				pool.Remove( deleteItem );
			}
		}

	} 
}
