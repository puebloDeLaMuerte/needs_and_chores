using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using YBC.Audix.InnerVoice;
using System;
using YBC.Utils;
using NaughtyAttributes;
using Unity.Collections.LowLevel.Unsafe;

namespace YBC.Audix.InnerVoice
{
	[Serializable]
	public class ItemPool : List<InnerVoiceItem>
	{
		private InnerVoiceItem lastPlayed;
		private float averageWeight;

		public float urgencyThreshold = 0f;
		public float cooldownRealtime = 0f;


		public void Reset()
		{
			this.Clear();
			lastPlayed = null;
			averageWeight = 0f;
		}


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
			foreach ( InnerVoiceItem item in this )
			{
				u += item.Urgency;
			}
			averageWeight = u / this.Count;
		}


		public AudioClip PickNextClip( bool random)
		{
			if( lastPlayed != null )
			{
				this.Remove( lastPlayed );
			}

			if ( this.Count == 0 ) return null;
			
			InnerVoiceItem next = null;

			int size = this.Count;

			int nextOrdinal = 0;
			if ( random )
			{
				nextOrdinal = YouBeRandom.Instance.RollZero( size - 1 );
			}

			next = this[nextOrdinal];
			lastPlayed = next;
			next.IncrementPickCount();
			next.setcoolDownMark( Time.time + cooldownRealtime );
			return next.Clip;
		}


		
		public void AddItem( InnerVoiceItem i )
		{
			base.Add( i );
			CalculateAverageWeight();
		}


		public void PrintPool()
		{

		}
	}
}