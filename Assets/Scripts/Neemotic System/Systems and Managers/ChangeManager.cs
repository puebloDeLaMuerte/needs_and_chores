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

		private Effect[] effectsQueue;

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

			// Activate/Deactivate Status-Effects and Overrides
			EvaluateNeedsStatus();

			// Apply all sheduled Changes and Overrides
			ApplyEffects();

			// Find and eliminate Expired Effects
			CheckExpiredEffects();

		}
 

		/// <summary>
		/// Makes all the needs in Neemotion[] needs; evaluate their current status.
		/// </summary>
		private void EvaluateNeedsStatus()
		{
			foreach ( var need in needs )
			{
				bool haschanged = need.EvaluateStatus();

				if ( haschanged )
				{
					//need.PerformImmediateStatusEffects();
				}
			}
		}
		

		/// <summary>
		/// The main Evaluation loop that adapts the values of needs and emotins.
		/// Applies Time Based Effects, Time Based Status Effects (immediate fx are performed on status-change in individual Neemtions), and External Effecs (from Objects/Player Interaction).
		/// </summary>
		private void ApplyEffects()
		{
			ApplyTimeBasedChanges();
			ApplyStatusEffects();
			ApplyExternalEffects();
		}


		/// <summary>
		/// 
		/// </summary>
		private void ApplyExternalEffects()
		{
		}


		/// <summary>
		/// Apply all non-immediate (Time-Based) StatusEffect updates in all needs in the needs[]
		/// </summary>
		private void ApplyStatusEffects()
		{ 
			foreach (var need in needs )
			{
				Effect[] statusFXs = need.getCurrentStatusFXs();
				
			}
		}


		/// <summary>
		/// Apply only the Time-Based Changes to every Neemotion in needs[]. No Status Effects are performed here. No Interaction-Based Effects.
		/// </summary>
		private void ApplyTimeBasedChanges()
		{
			foreach ( var need in needs )
			{
				float thisChangeAmount = need.changeAmountPerHour * generalChangeFactor * YBCTimer.GetDeltaHours();
				float tempval = need.currentValue + thisChangeAmount;
				need.SetCurrentValue(tempval);
			}
		}


		private void CheckExpiredEffects()
		{
		}

	} 
}
