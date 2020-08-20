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

		public float GetTotalWeight()
		{
			float u = 0f;
			foreach ( VoiceItem item in this )
			{
				u += item.Weight;
			}
			return u;
		}


		public AudioClip PickNextClip()
		{
			if( lastPlayed != null )
			{
				this.Remove( lastPlayed );
			}

			VoiceItem next = null;

			int size = this.Count;

			YouBeRandom r = new YouBeRandom();

			next = this.ToArray()[r.RollZero( size )];
			lastPlayed = next;

			return next.Clip;
		}
	}
}