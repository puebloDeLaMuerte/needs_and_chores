using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using YBC.Audix.InnerVoice;
using System;
using YBC.Utils;
using NaughtyAttributes;

namespace YBC.Audix.InnerVoice
{
	public class ItemPool : List<InnerVoiceItem>
	{
		private InnerVoiceItem lastPlayed;
		private float averageWeight;

		public Boolean isEmpty()
		{
			if( this.Count == 0 )
			{
				return true;
			} else
			{
				return false;
			}
		}


		public float GetAverageWeight()
		{
			return averageWeight;
		}

		private void CalculateAverageWeight()
		{
			if ( this.Count <= 0 ) averageWeight = 0f;

			float u = 0f;
			foreach ( NeemotixVoiceItem item in this )
			{
				u += item.Urgency;
			}
			averageWeight = u / this.Count;
		}


		public AudioClip PickNextClip()
		{
			if( lastPlayed != null )
			{
				this.Remove( lastPlayed );
			}

			if ( this.Count == 0 ) return null;
			
			InnerVoiceItem next = null;

			int size = this.Count;

			int nextOrdinal = YouBeRandom.Instance.RollZero( size - 1 );
			next = this.ToArray()[nextOrdinal];
			lastPlayed = next;
			next.IncrementPickCount();
			return next.Clip;
		}


		
		public new void Add( NeemotixVoiceItem i )
		{
			base.Add( i );
			CalculateAverageWeight();
		}
	}
}