using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YBC.Utils;

namespace YBC.Neemotix
{
	public class ChangeManager : NeemotixBase
	{
		public GameObject needsCollectionObject;
		public GameObject emotionsCollectionObjects;

		[Space]

		public float generalChangeFactor = 1f;

		private Neemotion[] needs;
		private Neemotion[] emotions;

		private List<Effect> effectsQueue = new List<Effect>();

		// Start is called before the first frame update
		void Start()
		{
			needs = needsCollectionObject.GetComponentsInChildren<Neemotion>();
			emotions = emotionsCollectionObjects.GetComponentsInChildren<Neemotion>();

			string message = "ChangeManager has loaded ";
			message += needs.Length;
			message += " needs and ";
			message += emotions.Length;
			message += " emotions.";
			Debug.Log(message);

		}

		// Update is called once per frame
		void Update()
		{

			// Activate/Deactivate Status-Effects and Overrides, Perform immediate Effects.
			EvaluateNeedsStatus();

			// Apply all sheduled Changes and Overrides
			ApplyEffects();

			// Find and eliminate Expired Effects
			CheckForExpiredEffects();

		}


		public void addEffectToQueue(Effect effect)
		{
			effectsQueue.Add(effect);
			Debug.Log("ChangeManager: Fx added to Queue: " + effect.neemotionAffected.NeemotionName );
		}


		/// <summary>
		/// Updates current status for entire Neemotion[] needs. Performs their immediate Status-Effects.
		/// </summary>
		private void EvaluateNeedsStatus()
		{
			foreach ( var need in needs )
			{
				bool haschanged = need.EvaluateStatus();

				if ( haschanged )
				{
					Effect[] fxs = need.getCurrentStatusFXs();

					foreach ( Effect fx in fxs )
					{
						PerformImmediateEffect(fx);
					}
				}
			}
		}
		
		/// <summary>
		/// Apply one immediate Effect to the neemotion it carries in its neemotionAffected field. Sets its value.
		/// Does Not perform TimeBasedEffects.
		/// </summary>
		/// <param name="fx">the Effect to execute</param>
		private void PerformImmediateEffect(Effect fx)
		{
			//TODO maybe the generalChangeFactor doesn't apply here, because of the difference in nature between timebasedFXs(values below one) and immediateFXs (values >1)
			fx.neemotionAffected.SetCurrentValue(fx.neemotionAffected.currentValue += (fx.instantChangeAmount * generalChangeFactor));
		}

		/// <summary>
		/// Applys time-based  Effect to the neemotion it carries in its neemotionAffected field.
		/// Sets its value and decrements its duration-counter by YBCTimer.GetDeltaHours().
		/// Does Not perform Immediate Effects.
		/// </summary>
		/// <param name="fx">the Effect to execute</param>
		private void PerformTimeBasedEffect(Effect fx)
		{
			float effectHourlyAmount = fx.changeAmountPerHour;
			if( effectHourlyAmount != 0)
			{
				float oldval = fx.neemotionAffected.currentValue;
				float changeAmount = (effectHourlyAmount * YBCTimer.GetDeltaHours() * generalChangeFactor);
				fx.neemotionAffected.SetCurrentValue( oldval += changeAmount );
				fx.IncrementAppliedHours(YBCTimer.GetDeltaHours());
			}
		}


		/// <summary>
		/// The main Evaluation loop that adapts the values of needs and emotins.
		/// Applies Time Based Effects, Time Based Status Effects (immediate fx are performed on status-change in individual Neemtions), and External Effecs (from Objects/Player Interaction).
		/// </summary>
		private void ApplyEffects()
		{
			PerformTimeBasedChanges();
			PerformStatusEffects();
			PerformExternalEffects();
		}


		/// <summary>
		/// 
		/// </summary>
		private void PerformExternalEffects()
		{
			foreach ( Effect fx in effectsQueue )
			{
				PerformTimeBasedEffect(fx);
			}
		}


		/// <summary>
		/// Apply all non-immediate (Time-Based) StatusEffect updates in all needs in the needs[]
		/// </summary>
		private void PerformStatusEffects()
		{ 
			foreach (Neemotion need in needs )
			{
				Effect[] statusFXs = need.getCurrentStatusFXs();

				foreach ( Effect fx in statusFXs )
				{
					PerformTimeBasedEffect(fx);
				}

			}
		}


		/// <summary>
		/// Apply only the Time-Based Changes to every Neemotion in needs[]. No Status Effects are performed here. No Interaction-Based Effects.
		/// </summary>
		private void PerformTimeBasedChanges()
		{
			foreach ( var need in needs )
			{
				float thisChangeAmount = need.changeAmountPerHour * generalChangeFactor * YBCTimer.GetDeltaHours();
				float tempval = need.currentValue + thisChangeAmount;
				need.SetCurrentValue(tempval);
			}
		}


		private void CheckForExpiredEffects()
		{
			foreach ( Effect fx in effectsQueue )
			{
				if ( fx.IsDue() )
				{
					effectsQueue.Remove(fx);
				}
			}
		}

	} 
}
