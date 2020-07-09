using UnityEngine;

namespace YBC.Neemotix
{
	[System.Serializable]
	public class Effect
	{
		public Neemotion neemotionAffected;
		
		public float instantChangeAmount = -0f;

		[Space]
		public float changeAmountPerHour = 0f;

		public float durationInHours = 0f;
	}
}