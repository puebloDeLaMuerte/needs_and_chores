using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace YBC.Audix
{
	public class SoundItem
	{
		public AudioClip Clip { get => clip; }
		protected AudioClip clip;

		public string Text { get => text; }
		protected string text;

		public char Variant { get => variant; }
		protected char variant;

		public Selector[] Selectors { get => selectors; }
		protected Selector[] selectors;

		private int picked = 0;
		private  float cooldownMark = 0;

		public SoundItem() {}


		/// <summary>
		/// Should be called when an item is picked from a pool to be said/used/played.
		/// </summary>
		public void IncrementPickCount()
		{
			picked++;
		}

		/// <summary>
		/// How many times has this SoundItem been picked for playing?
		/// </summary>
		/// <returns></returns>
		public int getPickedCount()
		{
			return picked;
		}


		/// <summary>
		/// Set a time until which this VoiceItem will be deemed "in cooldown" and should not be said.
		/// </summary>
		/// <param name="mark">the absolute time at which the item will leave cooldown (become available)</param>
		public void setCooldownMark( float mark )
		{
			cooldownMark = mark;
		}


		/// <summary>
		/// Determines wether the item is "in cooldown" at a given time.
		/// </summary>
		/// <param name="currentTime">the absolute time for which the cooldwon is checked</param>
		/// <returns>true if the item is considered to be "in cooldown". false if not</returns>
		public bool isCooldwonBlocked( float currentTime)
		{
			if( currentTime < cooldownMark )
			{
				return true;
			} else
			{
				return false;
			}
			//return currentTime > cooldownMark ? true : false;
		}

		public SoundItem(AudioClip clip, string text, char variant, Selector[] selectors )
		{
			this.clip = clip;
			this.text = text;
			this.variant = variant;
			this.selectors = selectors;
		}
	}
}