using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;


namespace YBC.Narratix.InnerVoice
{

	public class VoiceItemCollection
	{

		private Dictionary<int, VoiceItem[]> voiceItems;





		public VoiceItemCollection( int[] neemotionIDs )
		{
			List<VoiceItem> tempVoiceItems = new List<VoiceItem>();

			UnityEngine.Object[] clips = Resources.LoadAll("InnerVoice/Neemotions");

			// create the VoiceItems and store in temporary List
			foreach ( UnityEngine.Object o in clips )
			{
				String[] filenameElements = ((AudioClip)o).name.Split( '_' );
				VoiceItem item = new VoiceItem( (AudioClip)o, filenameElements );
				tempVoiceItems.Add( item );
			}


			// Group VoiceItems by NeemotionID
			voiceItems = new Dictionary<int, VoiceItem[]>();

			foreach ( int id in neemotionIDs )
			{
				List<VoiceItem> itemsForNeemotion = new List<VoiceItem>();

				foreach ( VoiceItem item in tempVoiceItems )
				{
					if( item.RelatedNeemotionID == id )
					{
						itemsForNeemotion.Add( item );
					}
				}

				voiceItems.Add( id, itemsForNeemotion.ToArray() );
			}
		}



		public void DebugListItems()
		{
			foreach ( KeyValuePair<int, VoiceItem[]> pair in voiceItems )
			{
				foreach ( VoiceItem item in pair.Value )
				{
					UnityEngine.Debug.Log( item.RelatedNeemotion + ": " + item.NeemoStatus +": "+item.Text);
				}
			}
		}



		public VoiceItem[] GetItemsForNeemotion(int neemotionID, NeemotionStatus status)
		{
			List<VoiceItem> returnItems = new List<VoiceItem>();

			VoiceItem[] tempItems;
			voiceItems.TryGetValue( neemotionID, out tempItems );

			foreach ( VoiceItem item in tempItems )
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