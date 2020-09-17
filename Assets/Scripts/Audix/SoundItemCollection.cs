using System;
using System.Collections.Generic;
using UnityEngine;
using YBC.Audix.InnerVoice;
using YBC.Utils.Error;

namespace YBC.Audix
{
	[Serializable]
	public class SoundItemCollection : IEditorInterfaceReceiver
	{

		private Dictionary<int, SoundItem[]> voiceItems;

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
		/// Validate Interface-References. Get Identifiers from DataAdapter and populate the SoundItems by parsing files on disk.
		/// </summary>
		public void Init()
		{
			if ( !ValidateAndAssignAdapter() ) return;

			(int, string)[] dataItemLookup = ivDataAdapter.getAllSelectors();

			List<SoundItem> tempVoiceItems = new List<SoundItem>();
			UnityEngine.Object[] clips = Resources.LoadAll( folderpath );
			

			foreach ( UnityEngine.Object o in clips )
			{
				SoundItem item = SoundItemFactory.TryParseClip( (AudioClip)o );
				if( item != null )
				{
					tempVoiceItems.Add( item );
					
					Debug.Log( item.Text );
				}
			}


			foreach ( (int, string) idNamePair in dataItemLookup )
			{
				/*
				List<SoundItem> itemsForNeemotion = new List<SoundItem>();

				foreach ( NeemotixVoiceItem item in tempVoiceItems )
				{
					if ( item.RelatedNeemotionID == id )
					{
						itemsForNeemotion.Add( item );
					}
				}
				voiceItems.Add( id, itemsForNeemotion.ToArray() );
				*/
			}
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



		public void DebugListItems()
		{
			foreach ( KeyValuePair<int, SoundItem[]> pair in voiceItems )
			{
				foreach ( NeemotixVoiceItem item in pair.Value )
				{
					UnityEngine.Debug.Log( item.RelatedNeemotion + ": " + item.NeemoStatus +": "+item.Text);
				}
			}
		}



		public SoundItem[] GetItemsForNeemotion(int neemotionID, NeemotionStatus status)
		{
			List<SoundItem> returnItems = new List<SoundItem>();

			SoundItem[] tempItems;
			voiceItems.TryGetValue( neemotionID, out tempItems );

			foreach ( NeemotixVoiceItem item in tempItems )
			{
				if( item.NeemoStatus == status )
				{
					returnItems.Add( item );
				}
			}

			return returnItems.ToArray();
		}
	}
}