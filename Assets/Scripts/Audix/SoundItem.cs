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

		public SoundItem() { }

		public void IncrementPickCount()
		{
			picked++;
		}

		public int getPickedCount()
		{
			return picked;
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