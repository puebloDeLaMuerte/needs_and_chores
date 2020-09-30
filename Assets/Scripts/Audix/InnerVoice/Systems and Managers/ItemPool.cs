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

		/// <summary>
		/// Clears the internal List, resets lastPlayed and averageWeight. Makes pool ready for re-use .
		/// </summary>
		public void Reset()
		{
			this.Clear();
			lastPlayed = null;
			averageWeight = 0f;
		}


		/// <summary>
		/// are there any InnerVoiceItems in the pool?
		/// </summary>
		/// <returns></returns>
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


		/// <summary>
		/// This Pools current Average (Ua += Un)/n  of all item.Urgency values.
		/// </summary>
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


		/// <summary>
		/// If Pool not empty, pick a random Item to play next, increment its pickCount and set its cooldown
		/// </summary>
		/// <param name="random"></param>
		/// <returns></returns>
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
			next.setCooldownMark( Time.time + cooldownRealtime );
			return next.Clip;
		}


		/// <summary>
		/// Adds an Item to the internal List<InnerVoiceItem>. Calculates the pools new averageWeight.
		/// </summary>
		/// <param name="i"></param>
		public void AddItem( InnerVoiceItem i )
		{
			base.Add( i );
			CalculateAverageWeight();
		}

		/// <summary>
		/// DEBUG: Printall items currently in this pool
		/// </summary>
		/// <param name="poolnr"></param>
		public void PrintPool(int poolnr)
		{
			Debug.Log( "printing pool nr: " + poolnr );
			foreach ( InnerVoiceItem item in this )
			{
				Debug.Log( "pool " + poolnr + ": " + item.Text + " u: " + item.Urgency );
			}
		}
	}
}