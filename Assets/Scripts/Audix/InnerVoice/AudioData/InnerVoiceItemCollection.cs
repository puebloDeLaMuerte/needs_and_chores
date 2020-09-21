using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YBC.Audix.InnerVoice;
using YBC.Utils.Error;

namespace YBC.Audix.InnerVoice
{
	[Serializable]
	public class InnerVoiceItemCollection : IEditorInterfaceReceiver
	{

		private List<InnerVoiceItem> voiceItems;

		public String folderpath;
		public GameObject IVDataAdapterObject;
		private IInnerVoiceDataAdapter ivDataAdapter;

		/*
		[Obsolete]
		public SoundItemCollection( int[] neemotionIDs )
		{
			List<SoundItem> tempVoiceItems = new List<SoundItem>();

			UnityEngine.Object[] clips = Resources.LoadAll("InnerVoice/Neemotions");

			// create the VoiceItems and store in temporary List
			foreach ( UnityEngine.Object o in clips )
			{
				String[] filenameElements = ((AudioClip)o).name.Split( '_' );
				SoundItem item = new NeemotixVoiceItem( (AudioClip)o, filenameElements );
				tempVoiceItems.Add( item );
			}


			// Group VoiceItems by NeemotionID
			voiceItems = new Dictionary<int, SoundItem[]>();

			foreach ( int id in neemotionIDs )
			{
				List<SoundItem> itemsForNeemotion = new List<SoundItem>();

				foreach ( NeemotixVoiceItem item in tempVoiceItems )
				{
					if( item.RelatedNeemotionID == id )
					{
						itemsForNeemotion.Add( item );
					}
				}

				voiceItems.Add( id, itemsForNeemotion.ToArray() );
			}
		}
		*/




		/// <summary>
		/// Validate Interface-References. Get Identifiers from DataAdapter and populate the SoundItems by parsing files on disk and comparing to what dataAdapter has to offer.
		/// </summary>
		public void Init()
		{
			if ( !ValidateAndAssignAdapter() ) return; // Fail early


			(int, string)[] adapterItemSelectorIDs = ivDataAdapter.getAllSelectorIDs();

			List<InnerVoiceItem> tempVoiceItems = new List<InnerVoiceItem>();
			UnityEngine.Object[] clips = Resources.LoadAll( folderpath );  // Load all from Disk
			

			foreach ( UnityEngine.Object o in clips )  // Parse Filenames to VoiceItems
			{
				SoundItem item = SoundItemFactory.TryParseClip( (AudioClip)o );

				if( item != null )
				{
					if ( item.GetType() == typeof( InnerVoiceItem ) )
					{
						tempVoiceItems.Add( (InnerVoiceItem)item );
					}
				}
			}

			voiceItems = new List<InnerVoiceItem>();

			foreach ( (int, string) idNamePair in adapterItemSelectorIDs ) // compare DataAdapter offerings
			{
				foreach ( InnerVoiceItem item in tempVoiceItems )
				{
					if ( item.Selectors[0].EvaluateID( idNamePair.Item1) )
					{
						voiceItems.Add( item );
					}
				}
			}

			Debug.Log( "SoundItemCollection initialized with " + voiceItems.Count + " items. path: " + folderpath);
		}



		public bool ValidateAndAssignAdapter()
		{
			bool success = false;
			if ( IVDataAdapterObject != null )
			{

				Component[] cpnts = IVDataAdapterObject.GetComponents( typeof( Component ) );

				foreach ( var cpnt in cpnts )
				{
					try
					{
						ivDataAdapter = (IInnerVoiceDataAdapter)cpnt;
						success = true;
					}
					catch ( System.Exception ) { }
				}
			}
			else
			{
				Debug.LogError( new YBCEditorNotAssignedError().ToString() );
				return success;
			}
			if ( !success )
			{
				Debug.LogError( new YBCEditorInterfaceObjNotParseableError().ToString() );
			}
			return success;
		}



		public IInnerVoiceDataAdapter getDataAdapter()
		{
			return ivDataAdapter;
		}



		public void DebugListItems()
		{
			foreach ( SoundItem item in voiceItems )
			{
				Debug.Log(  "SoundItem loaded: " + item.Selectors[0].getName() +": "+ item.Text );
			}
		}

		public SoundItem[] GetItems()
		{

			return voiceItems.ToArray();
		}


		[Obsolete]
		public SoundItem[] GetItemsForNeemotion(int neemotionID, NeemotionStatus status)
		{
			List<SoundItem> returnItems = new List<SoundItem>();

			foreach( SoundItem tempItem in voiceItems )
			{

				///////// T E M P   O N L Y /////////
				////						    ////					HERE BE DRAGONS!!!!!
				//								 //

				int statusInt;
				switch ( status )
				{
					case NeemotionStatus.Undefined:
						statusInt = -1;
						break;
					case NeemotionStatus.vlow:
						statusInt = 0;
						break;
					case NeemotionStatus.low:
						statusInt = 1;
						break;                                                      /// we need a better solution for this
					case NeemotionStatus.med:
						statusInt = 2;                          //// one that dows the lookup no matter how
						break;
					case NeemotionStatus.high:
						statusInt = 3;                  /// many Selectors are in the array1!!!!!!
						break;
					default:
						statusInt = -1;
						break;
				}

				//								 //
				//////						//////

				if ( tempItem.Selectors[0].Evaluate( statusInt ) ) returnItems.Add( tempItem );
			}

			return returnItems.ToArray();
		}
	}
}