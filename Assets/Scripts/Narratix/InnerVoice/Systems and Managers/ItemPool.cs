using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using YBC.Narratix.InnerVoice;
using System;
using YBC.Utils;

namespace YBC.Narratix.InnerVoice
{
	public class ItemPool : List<VoiceItem>
	{
		private VoiceItem lastPlayed;
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
			foreach ( VoiceItem item in this )
			{
				u += item.Weight;
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
			
			VoiceItem next = null;

			int size = this.Count;


			int nextOrdinal = YouBeRandom.Instance.RollZero( size - 1 );
			next = this.ToArray()[nextOrdinal];
			lastPlayed = next;

			return next.Clip;
		}



		public new void Add( VoiceItem i )
		{
			base.Add( i );
			CalculateAverageWeight();
		}
	}
}