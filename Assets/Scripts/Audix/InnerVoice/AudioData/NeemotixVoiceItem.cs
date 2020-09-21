using UnityEngine;
using System.Collections;
using System.CodeDom;
using System;
using System.Globalization;

namespace YBC.Audix.InnerVoice
{
	[Obsolete]
	public class NeemotixVoiceItem : InnerVoiceItem
	{


		public NeemotionStatus NeemoStatus { get => neemoStatus; }
		private NeemotionStatus neemoStatus;

		public TriggerType TriggerType { get => triggerType; }
		private TriggerType triggerType;

		public string RelatedNeemotion { get => relatedNeemotion; }
		private string relatedNeemotion;

		public int RelatedNeemotionID { get => relatedNeemotionID;  }
		private int relatedNeemotionID;


		public NeemotixVoiceItem( AudioClip clip, string[] dataFromFileName )	
		{
			
			base.clip = clip;
			base.text = dataFromFileName[7];
			base.urgency = float.Parse( dataFromFileName[5], CultureInfo.InvariantCulture );
			base.variant = char.Parse( dataFromFileName[4] );
			
			this.relatedNeemotion = dataFromFileName[2];
			this.relatedNeemotionID = relatedNeemotion.GetHashCode();
			ParseStatus( dataFromFileName[3], out neemoStatus );
			Enum.TryParse<TriggerType>( dataFromFileName[6], out triggerType );
		}


		private void ParseStatus(string s, out NeemotionStatus stat)
		{
			switch ( s )
			{
				case "vlow":
					stat = NeemotionStatus.vlow;
					break;
				case "low":
					stat = NeemotionStatus.low;
					break;
				case "med":
					stat = NeemotionStatus.med;
					break;
				case "high":
					stat = NeemotionStatus.high;
					break;
				default:
					stat = NeemotionStatus.Undefined;
					break;
			}
		}
	}
}