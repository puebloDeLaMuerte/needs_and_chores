using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		private AudioSource audioSource;


		// public ItemPool[] itemPools;

		private int poolPointer;



		[Space]
		[Header( "Talk Options" )]

		public float minimumPauseSeconds = 0.1f;
		[Range( 0f, 1f )]
		public float intervalRandomnes = 0.0f;
		private float randomInterval = 0f;
		private int initialPause = 2;
		private float timeTillNext;
		private float timeSinceLast;
		//public float cooldownRealtime = 0f;

		[Space]
		[Header( "Pool Options" )]

		public List<ItemPool> itemPools;
		public bool kickLeastImportant = true;

		[Space]
		[Header( "Debug Stuff" )]

		public float debugTimeTillNext;
		public float debugPause;



		// Start is called before the first frame update
		void Start()
		{

			timeTillNext = (float)initialPause;

			//itemPools = new ItemPool[3];

			for ( int i = 0; i < itemPools.Count-1; i++ )
			{
				//itemPools[i] = new ItemPool();
			}

			audioSource = GetComponent<AudioSource>();

			foreach ( InnerVoiceItemCollection itemcollection in soundItemCollections )
			{
				itemcollection.Init();
			}
		}



		// Update is called once per frame
		void Update()
		{

			Stopwatch sw = new Stopwatch();

			timeSinceLast += Time.deltaTime;

			if( (timeTillNext + randomInterval < timeSinceLast) && !audioSource.isPlaying ) // wenn es an der Reihe ist etwas zu sagen:
			{

				sw.Start();

				EvaluateCollections();


				foreach ( ItemPool pool in itemPools )
				{
					if( !pool.isEmpty() )
					{
						audioSource.clip = pool.PickNextClip( true );
						audioSource.Play();
						timeSinceLast = 0;

						SetRandomInterval();
						SetTimeTillNext();
						break;
					}
				}

				/*

				if ( itemPools[0].isEmpty() )
				{
					PopulateItemPools();

					SetTimeTillNext();
				}
				else if ( !audioSource.isPlaying )
				{
					audioSource.clip = itemPools[0].PickNextClip( true );
					audioSource.Play();
					timeSinceLast = 0;

					//timeTillNext = minimumPauseSeconds / (1f + pool.GetAverageWeight());
					SetRandomInterval();

					// TEMP:
					SetTimeTillNext();



				}
				*/
					debugPause = timeTillNext;
				sw.Stop();
				UnityEngine.Debug.Log( "IN.Update() timed:" + sw.Elapsed );
				UnityEngine.Debug.Log( "IN.Update() millis:" + sw.ElapsedMilliseconds);
			}


			debugTimeTillNext = timeTillNext - timeSinceLast;
		}





		private void SetTimeTillNext()
		{
			timeTillNext = minimumPauseSeconds; //   / ( 0.1f + itemPools[0].GetAverageWeight() );
		}

		private void SetRandomInterval()
		{	
			randomInterval = YouBeRandom.Instance.FloatZeroTo( intervalRandomnes * debugTimeTillNext );
		}


		private void EvaluateCollections()
		{

			for ( int i = 0; i < itemPools.Count - 1; i++ )
			{
				itemPools[i].Reset();
			}

			foreach ( InnerVoiceItemCollection collection in soundItemCollections )
			{
				IInnerVoiceDataAdapter dataAdapter = collection.getDataAdapter();

				foreach ( var ids in dataAdapter.getAllSelectorIDs() )
				{
					List<InnerVoiceItem> itemsForDataPoint = new List<InnerVoiceItem>();

					int id = ids.Item1;

					foreach ( InnerVoiceItem item in collection.GetItems() )
					{

						// For each collectoin:
						// Get all IDs from it's DataAdapter and cycle through each item in the collection to find matching ids/value constellations.

						if ( item.Selectors[0].EvaluateID( id ) )
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

					if ( itemsForDataPoint.Count > 1 )
					{
						///TODO: Think about how Variants factor into this! using only the first item for now!
						///

						//// variants-code here:
						/// InnerVoiceItem pick = PickLeastPlayed( itemsForDataPoint );
						// pool.AddItem( pick );
						PutSingleItemIntoPool( itemsForDataPoint[0] );
					}
					else if( itemsForDataPoint.Count != 0 )
					{
						PutSingleItemIntoPool( itemsForDataPoint[0] );
					}
				}
			}

		}


		private void PopulateItemPools()
		{
			/*
			foreach ( ItemPool p in itemPools )
			{
				p.Reset();
			}

			foreach ( InnerVoiceItemCollection collection in soundItemCollections )
			{
				foreach ( ItemPool p in itemPools )
				{
					if( p.MyPoolType == collection.sortIntoPool )
					{
						PopulateItemPool( p, collection );
					}
				}
			}
			*/
		}
			
			
		

		/// <summary>
		/// Pupulates a fresh instance of itemPool. Querrys the Neemotions and picks Variants according to status. rolls Dice to determine if the variant get's pushed to the stack.
		/// </summary>
		private void PopulateItemPool( ItemPool pool, InnerVoiceItemCollection audioCollection )
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
					///TODO: Think about how Variants factor into this! using only the first item for now!
					///

					//// variants-code here:
					/// InnerVoiceItem pick = PickLeastPlayed( itemsForDataPoint );
					// pool.AddItem( pick );
					PutSingleItemIntoPool( itemsForDataPoint[0] );
				} else
				{
					PutSingleItemIntoPool( itemsForDataPoint[0] );
				}
			}
		}

		/// <summary>
		/// Sort a List of items into their respective Pools (by urgency-threshold)
		/// </summary>
		/// <param name="items">A List of items</param>
		private void PutItemsIntoPools( List<InnerVoiceItem> items )
		{
			foreach ( InnerVoiceItem item in items )
			{
				PutSingleItemIntoPool(item);
			}
		}

		/// <summary>
		/// Sort one item into it's pool (by urgency-threshold)
		/// </summary>
		/// <param name="item"></param>
		private void PutSingleItemIntoPool( InnerVoiceItem item )
		{
			for ( int i = 0; i < itemPools.Count; i++ )
			{

				ItemPool pool = itemPools[i];

				float threshold = 0f;
				if ( i != itemPools.Count - 1 ) // only if we deal with the last possible pool, accept everything...
				{
					threshold = pool.urgencyThreshold;
				}

				if ( item.Urgency > threshold )
				{
					if( !item.isCooldwonBlocked(Time.time) )
					{
						pool.Add( item );
						return;
					}
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



		private void KickLeastImportantFromPool(ItemPool pool)
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
				UnityEngine.Debug.Log( "DEL: " + deleteItem.Urgency + " - " + deleteItem.Text );
				pool.Remove( deleteItem );
			}
		}

	} 
}
