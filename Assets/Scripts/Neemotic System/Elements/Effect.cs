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
		private bool revoked = false;

		public float TotalHoursApplied { get => totalHoursApplied; }


		/// <summary>
		/// Resets totalHoursApplied and revoked values so this Effect can be re-used in the future.
		/// Everything that has to be done to make this re-useable needs to be done in here!
		/// </summary>
		public void Reset()
		{
			totalHoursApplied = 0f;
			revoked = false;
		}


		/// <summary>
		/// Sets the flag which tells the ChangeManager to pop this effect from it's stack.
		/// </summary>
		public void Revoke()
		{
			Debug.Log("Revoking " + neemotionAffected);
			revoked = true;
		}


		/// <summary>
		/// Increment the counter by h=hours
		/// </summary>
		/// <param name="h">put YBCTimer.getDeltaHours() here</param>
		public void IncrementAppliedHours(float h)
		{
			totalHoursApplied += h;
		}


		/// <summary>
		/// Has this Effect been applied for its durationInHours or is it an immediate Effect?
		/// </summary>
		/// <returns>true if it has a duration and totalHoursApplied >= durationInHours.
		/// Also true if it has no duration and no changePerHour value (probably an immediate FX then!)</returns>
		public bool IsDue()
		{
			if ( durationInHours != 0)
			{
				if( totalHoursApplied >= durationInHours )
				{
					return true;
				} else
				{
					return false;
				}
			} else
			{
				if( changeAmountPerHour == 0 )
				{
					return true;
				} else
				{
					return false;
				}
			}
		}


		/// <summary>
		/// Has this Effect been revoked through it's Revoke() Delegate-Method?
		/// </summary>
		/// <returns>true if Revoke() has been called on it</returns>
		public bool IsRevoked()
		{
			return revoked;
		}


		/// <summary>
		/// Determines wether the durationInHours is greater than zero
		/// </summary>
		/// <returns></returns>
		public bool hasDuration()
		{
			return durationInHours > 0;
		}
	}
}