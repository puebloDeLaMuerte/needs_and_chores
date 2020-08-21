using UnityEngine;
using System.Collections;
using System.CodeDom;
using System;
using System.Globalization;

namespace YBC.Narratix.InnerVoice
{
	public class VoiceItem
	{
		public float Weight { get => weight; }
		private float weight;

		public AudioClip Clip { get => clip; }
		private AudioClip clip;

		public string Text { get => text; }
		private string text;

		public NeemotionStatus NeemoStatus { get => neemoStatus; }
		private NeemotionStatus neemoStatus;

		public char Variant { get => variant; }
		private char variant;

		public TriggerType TriggerType { get => triggerType; }
		private TriggerType triggerType;

		public string RelatedNeemotion { get => relatedNeemotion; }
		private string relatedNeemotion;

		public int RelatedNeemotionID { get => relatedNeemotionID;  }
		private int relatedNeemotionID;


		public VoiceItem( AudioClip clip, string[] dataFromFileName )
		{
			this.clip = clip;

			this.relatedNeemotion = dataFromFileName[2];
			this.relatedNeemotionID = relatedNeemotion.GetHashCode();
			this.text = dataFromFileName[7];
			this.weight = float.Parse( dataFromFileName[5], CultureInfo.InvariantCulture );
			this.variant = char.Parse( dataFromFileName[4] );
			ParseStatus( dataFromFileName[3], out neemoStatus );
			Enum.TryParse<TriggerType>( dataFromFileName[6], out triggerType );
		}


		private void ParseStatus(string s, out NeemotionStatus stat)
		{
			switch ( s )
			{
				case "vlow":
					stat = NeemotionStatus.Urgent;
					break;
				case "low":
					stat = NeemotionStatus.Unsatisfied;
					break;
				case "med":
					stat = NeemotionStatus.Satisfied;
					break;
				case "high":
					stat = NeemotionStatus.Oversatisfied;
					break;
				default:
					stat = NeemotionStatus.Undefined;
					break;
			}
		}
	}
}