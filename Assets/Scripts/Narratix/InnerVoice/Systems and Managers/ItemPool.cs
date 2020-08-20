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
			if ( this.Count <= 0 ) return 0;

			float u = 0f;
			foreach ( VoiceItem item in this )
			{
				u += item.Weight;
			}
			return u / this.Count;
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

			YouBeRandom r = new YouBeRandom();

			int nextOrdinal = r.RollZero( size - 1 );
			next = this.ToArray()[nextOrdinal];
			lastPlayed = next;

			return next.Clip;
		}
	}
}