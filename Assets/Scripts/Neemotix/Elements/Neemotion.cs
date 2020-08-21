using NaughtyAttributes;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YBC.Neemotix
{
	public class Neemotion : NeemotixBase
	{
		public Slider guiSlider;

		[Space]
		
		public float startValue = neemotionMaxValue;
		public float currentValue = neemotionMaxValue;
		public float changeAmountPerHour = -0.1f;


		[Space]
		[Space]

		public bool isEmotion = false;
		public bool isItBad = false; //this one only temporary. Use only in the debug-gui!


		[Range(0f, 10f)]
		public float urgendZoneMax = neemotionMaxValue / 4f;
		[Range(0f, 10f)]
		public float unsatisfiedZoneMax = neemotionMaxValue * 2f / 4f;
		[Range(0f, 10f)]
		public float satisfiedZoneMax = neemotionMaxValue * 3f / 4f;

		[Space]
		[Space]

		public string statusString;
		public NeemotionStatus Status { get => status; }
		private NeemotionStatus status;

		[BoxGroup("urgend")]
		public Effect[] urgendZoneEffects;
		[BoxGroup("unsatisfied")]
		public Effect[] unsatisfiedEffects;
		[BoxGroup("satisfied")]
		public Effect[] satisfiedEffects;
		[BoxGroup("oversatisfied")]
		public Effect[] oversatisfiedEffects;

		[Space]

		public Override[] urgencyOverrides;

		public string neemotionName;
		private int neemotionID;



		private List<KeyValuePair<String, float>> myInfluencers = new List<KeyValuePair<string, float>>();


		public void AddInfluencer(String name, float value)
		{
			myInfluencers.Add(new KeyValuePair<String, float>(name, value));
		}

		public void ListInfluences()
		{
			foreach ( KeyValuePair<String,float> item in myInfluencers )
			{
				Debug.Log(neemotionName + ":  " + item.Key + ":  " + item.Value);
			}
		}

		/// <summary>
		/// Initialize Gui-Slider color Stuff. Set Nemotion Name. Initialize CurrentValue and Status. Set IssuerName to the Effects
		/// </summary>
		private void Awake()
		{
			this.neemotionName = gameObject.name;
			this.neemotionID = neemotionName.GetHashCode();
			SetCurrentValue(startValue);
			EvaluateStatus();
			this.guiSlider.value = currentValue;
			this.guiSlider.maxValue = neemotionMaxValue;
			this.guiSlider.minValue = neemotionMinValue;

			sliderGradient = new Gradient();

			SetIssuerToEffects();
		}

		/// <summary>
		/// Sets the name of this Neemotion as the IssuerAdress of it's Effects.
		/// </summary>
		private void SetIssuerToEffects()
		{
			foreach ( Effect effect in urgendZoneEffects )
			{
				effect.SetIssuerName(neemotionName + ":urgent");

				if( effect.changeAmountPerHour != 0 )
				{
					effect.neemotionAffected.AddInfluencer(effect.GetIssuerName(), effect.changeAmountPerHour );
				}
				else if ( effect.instantChangeAmount != 0 )
				{
					effect.neemotionAffected.AddInfluencer(effect.GetIssuerName(), effect.instantChangeAmount);
				}

			}
			foreach ( Effect effect in unsatisfiedEffects )
			{
				effect.SetIssuerName(neemotionName + ":unsatisfied");

				if ( effect.changeAmountPerHour != 0 )
				{
					effect.neemotionAffected.AddInfluencer(effect.GetIssuerName(), effect.changeAmountPerHour);
				}
				else if ( effect.instantChangeAmount != 0 )
				{
					effect.neemotionAffected.AddInfluencer(effect.GetIssuerName(), effect.instantChangeAmount);
				}
			}
			foreach ( Effect effect in satisfiedEffects )
			{
				effect.SetIssuerName(neemotionName + ":satisfied");

				if ( effect.changeAmountPerHour != 0 )
				{
					effect.neemotionAffected.AddInfluencer(effect.GetIssuerName(), effect.changeAmountPerHour);
				}
				else if ( effect.instantChangeAmount != 0 )
				{
					effect.neemotionAffected.AddInfluencer(effect.GetIssuerName(), effect.instantChangeAmount);
				}
			}
			foreach ( Effect effect in oversatisfiedEffects )
			{
				effect.SetIssuerName(neemotionName + ":oversatisfied");

				if ( effect.changeAmountPerHour != 0 )
				{
					effect.neemotionAffected.AddInfluencer(effect.GetIssuerName(), effect.changeAmountPerHour);
				}
				else if ( effect.instantChangeAmount != 0 )
				{
					effect.neemotionAffected.AddInfluencer(effect.GetIssuerName(), effect.instantChangeAmount);
				}
			}
		}



		/// <summary>
		/// Initialize Gui-Slider Color Stuff (Probably not the best place to do it here as a Spagetti).
		/// </summary>
		private void Start()
		{
			Color leftColor, rightColor;

			if( isItBad )
			{
				leftColor = satisfiedColor;
				rightColor = urgendColor;
			} else
			{
				leftColor = urgendColor;
				rightColor = satisfiedColor;
			}

			// Populate the color keys at the relative time 0 and 1 (0 and 100%)
			sliderColorKey = new GradientColorKey[2];
			sliderColorKey[0].color = leftColor;
			sliderColorKey[0].time = 0.0f;
			sliderColorKey[1].color = rightColor;
			sliderColorKey[1].time = 1.0f;

			GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
			alphaKey[0].alpha = 1.0f;
			alphaKey[0].time = 0.0f;
			alphaKey[1].alpha = 1.0f;
			alphaKey[1].time = 1.0f;

			sliderGradient.SetKeys(sliderColorKey, alphaKey);
		}


		/// <summary>
		/// Updates the Gui-Slider Color.
		/// </summary>
		private void Update()
		{
			guiSlider.value = currentValue;

			if( isEmotion )
			{
				LerpSliderColor();
			}
		}



		private void LerpSliderColor()
		{
			float sliderVal = currentValue / neemotionMaxValue;
			guiSlider.GetComponentInChildren<Image>().color = sliderGradient.Evaluate( sliderVal);
		}



		/// <summary>
		/// Evaluate current Status according to currentValue of this Neemotion. Calls ChangeStatus() to actually change it
		/// </summary>
		/// <returns>true if status has changed, false if it didn't</returns>
		public bool EvaluateStatus()
		{
			// if ( isEmotion ) return false; // Don't do any Status-Stuff if you're just an Emotion. Your Slider will lerp it's color according to value in that case!

			NeemotionStatus tempstat = NeemotionStatus.Undefined;

			if ( currentValue <= urgendZoneMax )
			{
				tempstat = NeemotionStatus.Urgent;
			}
			else if ( currentValue <= unsatisfiedZoneMax )
			{
				tempstat = NeemotionStatus.Unsatisfied;
			}
			else if ( currentValue <= satisfiedZoneMax )
			{
				tempstat = NeemotionStatus.Satisfied;
			}
			else if ( currentValue > satisfiedZoneMax )
			{
				tempstat = NeemotionStatus.Oversatisfied;
			}
			else
			{
				Debug.LogError("Neemotion could not determine it's status: " + neemotionName + " current value: " + currentValue);
			}

			if ( tempstat != status )
			{
				ChangeStatus(tempstat);
				return true;
			} else return false;
		}



		/// <summary>
		/// Set status according to currentValue. Update Slider Color.
		/// </summary>
		/// <param name="newstat">The new Status to be switched to</param>
		/// 
		private void ChangeStatus(NeemotionStatus newstat)
		{
			this.status = newstat;
			this.statusString = status.ToString();

			Debug.Log("Status Change: " + neemotionName + " --> " + newstat);

			// update Slider Color.
			switch ( newstat )
			{
				case NeemotionStatus.Undefined:
					guiSlider.GetComponentInChildren<Image>().color = new Color(1, 1, 1);
					break;
				case NeemotionStatus.Urgent:
					guiSlider.GetComponentInChildren<Image>().color = urgendColor;
					//PerformImmediateStatusEffets(urgendZoneEffects);
					break;
				case NeemotionStatus.Unsatisfied:
					guiSlider.GetComponentInChildren<Image>().color = unsatisfiedColor;
					//PerformImmediateStatusEffets(unsatisfiedEffects);
					break;
				case NeemotionStatus.Satisfied:
					guiSlider.GetComponentInChildren<Image>().color = satisfiedColor;
					//PerformImmediateStatusEffets(satisfiedEffects);
					break;
				case NeemotionStatus.Oversatisfied:
					guiSlider.GetComponentInChildren<Image>().color = oversatisfiedColor;
					//PerformImmediateStatusEffets(oversatisfiedEffects);
					break;
				default:
					break;
			}
		}



		/// <summary>
		/// Performs all Status Effects associated with it's current status (Doesn't evaluate, call EvaluateStatus() before, if yo're not shure.)
		/// Does only perform Effects that have non-zero instantChangeAmount. Only this instantChangeAmount will be performed. Any other values will be ignored.
		/// </summary>
		
		private void PerformImmediateStatusEffets()
		{
			Debug.LogError("Thou shalt not Perform Effects directly within a Neemotion!");
		}



		/// <summary>
		/// Set the current Value from outside. Clamps the Value to neemotionMin/Max Values. No further checks or actions.
		/// </summary>
		/// <param name="newVal">The value to change to. Can be any Number, will be clamped.</param>
		public void SetCurrentValue( float newVal )
		{
			if ( newVal < 0 ) newVal = neemotionMinValue;
			if ( newVal > 10 ) newVal = neemotionMaxValue;

			currentValue = newVal;
		}



		/// <summary>
		/// Get currently valid Status Effects as a List.
		/// </summary>
		/// <returns>A Effect[] depending on the current status of this Neemotion. Either of (urgendZoneEffects, unsatisfiedEffects, satisfiedEffects, oversatisfiedEffects). Returns NULL if Status is undefined</returns>
		internal Effect[] getCurrentStatusFXs()
		{
			switch ( status )
			{
				case NeemotionStatus.Urgent:
					return urgendZoneEffects;
				case NeemotionStatus.Unsatisfied:
					return unsatisfiedEffects;
				case NeemotionStatus.Satisfied:
					return satisfiedEffects;
				case NeemotionStatus.Oversatisfied:
					return oversatisfiedEffects;
				default:
					return null;
			}
		}


		/// <summary>
		/// The neemotionID is the HashCode of it's neemotionName, generated on Awake(). No guarantees if someone changes it afterwards, which is NOT intended!
		/// </summary>
		/// <returns></returns>
		public int GetID()
		{
			return neemotionID;
		}
	} 
}
