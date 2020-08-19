using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using YBC.Neemotix;
using YBC.Perceptix.PPVData;

namespace YBC.Perceptix
{
	public class PPVManager : MonoBehaviour
	{

		public NeemotixValuesAdapter neemotixAdapter;
		private PPVDataObject ppvDataObject;

		[Space]

		public Volume bloomPPV;
		private List<(float, float)> bloomWeightValues;

		public Volume chromaticAberrationPPV;
		private List<(float, float)> chromaticAberrationWeightValues;

		public Volume colorAdjustmentsPPV;
		private List<(float, float)> colorAdjustmentWeightValues;
		private List<(float, float)> postExposureValues;
		ColorAdjustments colorAdjustments;

		public Volume depthOfFieldPPV;
		private List<(float, float)> depthOfFieldWeightValues;
		private List<(float, float)> focusDistanceValues;
		DepthOfField depthOfField;

		public Volume filmGrainPPV;
		private List<(float, float)> filmGrainWeightValues;

		public Volume lensDistortionPPV;
		private List<(float, float)> lensDistortionWeightValues;

		public Volume motionBlurPPV;
		private List<(float, float)> motionBlurWeightValues;

		public Volume splitToningPPV;
		private List<(float, float)> splitToningWeightValues;

		public Volume vignettePPV;
		private List<(float, float)> vignetteWeightValues;

		public Volume smhPPV;
		private List<(float, float)> smhhWeightValues;

		public Volume channelMixerPPV;
		private List<(float, float)> channelMixerWeightValues;



		/// <summary>
		/// Check if all PPVs are assigned in editor. Find the Overrides in Volumes which change values in Overrides. Instanciate the Data Object.
		/// </summary>
		private void Awake()
		{
			CheckPublicFields();
			ppvDataObject = new PPVDataObject();

			//colorAdjustments = colorAdjustmentsPPV.GetComponent<ColorAdjustments>();
			//depthOfField = depthOfFieldPPV.GetComponent<DepthOfField>();

			ColorAdjustments tmpCA;
			if ( colorAdjustmentsPPV.profile.TryGet<ColorAdjustments>(out tmpCA) )
			{
				colorAdjustments = tmpCA;
			}


			DepthOfField tmpDOF;
			if ( depthOfFieldPPV.profile.TryGet<DepthOfField>(out tmpDOF) )
			{
				depthOfField = tmpDOF;
			}
		}


		/// <summary>
		/// Throw Errors at Runtime if the PPVs are not connected!
		/// </summary>
		private void CheckPublicFields()
		{
			if ( bloomPPV == null ) throw new System.Exception("YBC.NotAssignedError - A public field has not been assigned in the editor. Things will break!");
			if ( chromaticAberrationPPV == null ) throw new System.Exception("YBC.NotAssignedError - A public field has not been assigned in the editor. Things will break!");
			if ( colorAdjustmentsPPV == null ) throw new System.Exception("YBC.NotAssignedError - A public field has not been assigned in the editor. Things will break!");
			if ( depthOfFieldPPV == null ) throw new System.Exception("YBC.NotAssignedError - A public field has not been assigned in the editor. Things will break!");
			if ( filmGrainPPV == null ) throw new System.Exception("YBC.NotAssignedError - A public field has not been assigned in the editor. Things will break!");
			if ( lensDistortionPPV == null ) throw new System.Exception("YBC.NotAssignedError - A public field has not been assigned in the editor. Things will break!");
			if ( motionBlurPPV == null ) throw new System.Exception("YBC.NotAssignedError - A public field has not been assigned in the editor. Things will break!");
			if ( splitToningPPV == null ) throw new System.Exception("YBC.NotAssignedError - A public field has not been assigned in the editor. Things will break!");
			if ( vignettePPV == null ) throw new System.Exception("YBC.NotAssignedError - A public field has not been assigned in the editor. Things will break!");
			if ( smhPPV == null ) throw new System.Exception("YBC.NotAssignedError - A public field has not been assigned in the editor. Things will break!");
			if ( channelMixerPPV == null ) throw new System.Exception("YBC.NotAssignedError - A public field has not been assigned in the editor. Things will break!");
		}


		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			ResetAllValues();
			ReadValues();
			AdjustPPVs();
		}

		/// <summary>
		/// Put new empty Lists in place for a new frame to be calculated.
		/// </summary>
		private void ResetAllValues()
		{
			bloomWeightValues = new List<(float, float)>();
			chromaticAberrationWeightValues = new List<(float, float)>();
			colorAdjustmentWeightValues = new List<(float, float)>();
			depthOfFieldWeightValues = new List<(float, float)>();
			filmGrainWeightValues = new List<(float, float)>();
			lensDistortionWeightValues = new List<(float, float)>();
			motionBlurWeightValues = new List<(float, float)>();
			splitToningWeightValues = new List<(float, float)>();
			vignetteWeightValues = new List<(float, float)>();
			smhhWeightValues = new List<(float, float)>();
			channelMixerWeightValues = new List<(float, float)>();

			postExposureValues = new List<(float, float)>();
			focusDistanceValues = new List<(float, float)>();
		}

		/// <summary>
		/// Loop through Emotions and read their DataAdapters.
		/// </summary>
		private void ReadValues()
		{
			float currentValue;
			PPVDataAdapter adapter;
			
			//TODO: find a way to loop through these!!!

			currentValue = neemotixAdapter.GetNeemotionValueByName( "Freude" );
			adapter = ppvDataObject.GetAdapterByNeemotion(PerceptibleNeemotions.Freude);
			ReadDataAdapter(adapter, currentValue);

			currentValue = neemotixAdapter.GetNeemotionValueByName("Trauer");
			adapter = ppvDataObject.GetAdapterByNeemotion(PerceptibleNeemotions.Trauer);
			ReadDataAdapter(adapter, currentValue);

			currentValue = neemotixAdapter.GetNeemotionValueByName("Angst");
			adapter = ppvDataObject.GetAdapterByNeemotion(PerceptibleNeemotions.Angst);
			ReadDataAdapter(adapter, currentValue);

			currentValue = neemotixAdapter.GetNeemotionValueByName("Wut");
			adapter = ppvDataObject.GetAdapterByNeemotion(PerceptibleNeemotions.Wut);
			ReadDataAdapter(adapter, currentValue);

			currentValue = neemotixAdapter.GetNeemotionValueByName("Ekel");
			adapter = ppvDataObject.GetAdapterByNeemotion(PerceptibleNeemotions.Ekel);
			ReadDataAdapter(adapter, currentValue);

			currentValue = neemotixAdapter.GetNeemotionValueByName("Stress");
			adapter = ppvDataObject.GetAdapterByNeemotion(PerceptibleNeemotions.Stress);
			ReadDataAdapter(adapter, currentValue);

			currentValue = neemotixAdapter.GetNeemotionValueByName("Gesundheit");
			adapter = ppvDataObject.GetAdapterByNeemotion(PerceptibleNeemotions.Gesundheit);
			ReadDataAdapter(adapter, currentValue);

			currentValue = neemotixAdapter.GetNeemotionValueByName("Schmerz");
			adapter = ppvDataObject.GetAdapterByNeemotion(PerceptibleNeemotions.Schmerz);
			ReadDataAdapter(adapter, currentValue);
		}




		

		/// <summary>
		/// Read out one DataAdapter completely, write all values to the corresponding Lists in this Manager.
		/// </summary>
		/// <param name="dadapt">the dataAdapter to read</param>
		/// <param name="currentNeemotionValue">The corresponding Neemotions current Value</param>
		private void ReadDataAdapter(PPVDataAdapter dadapt, float currentNeemotionValue )
		{

			ReadPPVDataObject(dadapt.bloomPPVdata, bloomWeightValues, currentNeemotionValue);
			ReadPPVDataObject(dadapt.chromaticAberrationPPVdata, chromaticAberrationWeightValues, currentNeemotionValue);
			ReadPPVDataObject(dadapt.colorAdjustmentsPPVdata, colorAdjustmentWeightValues, currentNeemotionValue);
			ReadPPVDataObject(dadapt.depthOfFieldPPVdata, depthOfFieldWeightValues, currentNeemotionValue);
			ReadPPVDataObject(dadapt.filmGrainPPVdata, filmGrainWeightValues, currentNeemotionValue);
			ReadPPVDataObject(dadapt.lensDistortionPPVdata, lensDistortionWeightValues, currentNeemotionValue);
			ReadPPVDataObject(dadapt.motionBlurPPVdata, motionBlurWeightValues, currentNeemotionValue);
			ReadPPVDataObject(dadapt.splitToningPPVdata, splitToningWeightValues, currentNeemotionValue);
			ReadPPVDataObject(dadapt.vignettePPVdata, vignetteWeightValues, currentNeemotionValue);
			ReadPPVDataObject(dadapt.smhPPVdata, smhhWeightValues, currentNeemotionValue);
			ReadPPVDataObject(dadapt.channelMixerPPVdata, channelMixerWeightValues, currentNeemotionValue);

		}

		/// <summary>
		/// writes lerped Data to the interrims-List for further processing this frame.
		/// </summary>
		/// <param name="data">the PPVdata Object to read</param>
		/// <param name="weights">the interrims-List to write to</param>
		/// <param name="currentvalue">the currentValue for the neemotion currently evaluated</param>
		private void ReadPPVDataObject( PPVData.PPVData data, List<(float,float)> weights, float currentvalue )
		{
			if ( data != null )
			{
				if ( data.weight != null )
				{
					MapValue(data.weight, currentvalue, out float lerpedValue);
					weights.Add( (currentvalue,lerpedValue) );
				}
				if( data.postExposure != null )
				{
					MapValue(data.postExposure, currentvalue, out float lerpedValue);
					postExposureValues.Add((currentvalue, lerpedValue));
				}
				if( data.focusDistance != null )
				{
					MapValue(data.focusDistance, currentvalue, out float lerpedValue);
					focusDistanceValues.Add((currentvalue, lerpedValue));
				}
			}
		}


		/// <summary>
		/// Finds the lerped output-value (for setting a PPV) for a given input value (neemotion intensity) according to the data array.
		/// </summary>
		/// <param name="data">The data-array from PPVDataObject containing settings for a PPV</param>
		/// <param name="val">The current neemotion-intensity</param>
		/// <param name="lerpedValue">the lerped output value (not yet multiplied by neemotion intensity)</param>
		private void MapValue((float,float)[] data, float val, out float lerpedValue)
		{
			lerpedValue = -1f;

			// if val < first
			if ( val <= data[0].Item1 )
			{
				lerpedValue = data[0].Item2;
			}
			// if val > last
			else if ( val >= data[data.Length - 1].Item1 )
			{
				lerpedValue = data[data.Length - 1].Item2;
			}
			// else, loop to find the spot
			else for ( int i = 0; i < data.Length - 1; i++ )
			{
				if ( val <= data[i + 1].Item1 )
				{
					lerpedValue = LerpTuples(val, data[i], data[i + 1]);
					break;
				}
			}
		}

		/// <summary>
		/// Convenience Method to lerp output value accordings to input value
		/// </summary>
		/// <param name="thevalue">the value to interpret</param>
		/// <param name="from">Tuple with lower range in/out numbers</param>
		/// <param name="to">Tuple with higher rande in/out numbers</param>
		/// <returns></returns>
		private float LerpTuples(float thevalue, (float,float) from, (float,float) to )
		{
			return (thevalue - from.Item1) / (to.Item1 - from.Item1) * (to.Item2 - from.Item2) + from.Item2;
		}



		/// <summary>
		/// For each PPV: check Neemotion affecting it, get value, Lerp and combine values and adjust the PPV accordingly.
		/// </summary>
		private void AdjustPPVs()
		{
			
			bloomPPV.weight = InterpolateValues(bloomWeightValues);
			chromaticAberrationPPV.weight = InterpolateValues(chromaticAberrationWeightValues);
			colorAdjustmentsPPV.weight = InterpolateValues(colorAdjustmentWeightValues);
			colorAdjustments.postExposure.SetValue(new FloatParameter( InterpolateValues(postExposureValues) ) );
			depthOfFieldPPV.weight = InterpolateValues(depthOfFieldWeightValues);
			depthOfField.focusDistance.SetValue(new FloatParameter(InterpolateValues(focusDistanceValues)));
			filmGrainPPV.weight = InterpolateValues(filmGrainWeightValues);
			lensDistortionPPV.weight = InterpolateValues(lensDistortionWeightValues);
			motionBlurPPV.weight = InterpolateValues(motionBlurWeightValues);
			splitToningPPV.weight = InterpolateValues(splitToningWeightValues);
			vignettePPV.weight = InterpolateValues(vignetteWeightValues);
			smhPPV.weight = InterpolateValues(smhhWeightValues);
			channelMixerPPV.weight = InterpolateValues(channelMixerWeightValues);
		}

		/// <summary>
		/// Find the weighted average for the different influences on a specific PPVsetting-
		/// </summary>
		/// <param name="values">List of (float,float). Item1: the desired value for the PPVsetting. Item2: The weight/intensity of that value</param>
		/// <returns>float: the weighted average of all entries in the input list</returns>
		private float InterpolateValues(List<(float,float)> values)
		{
			float valuesTotal = 0f;
			float intensitiesTotal = 0f;

			foreach ( (float,float) pair in values )
			{
				valuesTotal += pair.Item1 * pair.Item2;
				intensitiesTotal += pair.Item1;
			}

			if ( intensitiesTotal == 0f)
			{
				return 0f;
			}
			else
			{
				return valuesTotal / intensitiesTotal;
			}
		}
	} 
}