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
		private float totalHoursApplied;

		public float TotalHoursApplied { get => totalHoursApplied; }

		/// <summary>
		/// Increment the counter by h=hours
		/// </summary>
		/// <param name="h">put YBCTimer.getDeltaHours() here</param>
		public void IncrementAppliedHours(float h)
		{
			totalHoursApplied += h;
		}

		/// <summary>
		/// Has this Effect been applied for its durationInHours?
		/// </summary>
		/// <returns>true if totalHoursApplied >= durationInHours</returns>
		public bool IsDue()
		{
			if( totalHoursApplied >= durationInHours )
			{
				return true;
			} else
			{
				return false;
			}
		}
	}
}