using UnityEngine;

namespace YBC.Audix.InnerVoice
{
	public class InnerVoiceItem : SoundItem
	{
		public float Urgency { get => urgency; }
		protected float urgency;

		public float Immediacy { get => immediacy; }
		protected float immediacy;

		public InnerVoiceItem() { }

		/// <summary>
		/// A new InnerVoiceItem to be used in InnerVoiceManager.
		/// </summary>
		/// <param name="clip">the audio clip itself</param>
		/// <param name="text">a text representation of the clip</param>
		/// <param name="variant">Is is a variant that corresponds to the exact same data?</param>
		/// <param name="urgency">unrgency-value for IVManager</param>
		/// <param name="selectors">the selectors used to pull data through the InnerVoiceDataAdapter. [0] First to process, [n] last to process</param>
		public InnerVoiceItem(AudioClip clip, string text, char variant, Selector[] selectors, float urgency, float immediacy ) 
			: base( clip, text, variant, selectors)
		{
			this.urgency = urgency;
		}
	}
}