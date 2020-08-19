using UnityEngine;
using System.Collections;
using YBC.Perceptix.PPVData;

namespace YBC.Perceptix.PPVData
{
	/// <summary>
	/// HelperClass for the PPVDataObject. Stores a dataAdapter for all PostProcessVolumes present in the game.
	/// </summary>
	public class PPVDataAdapter
	{
		public PPVData bloomPPVdata;
		public PPVData chromaticAberrationPPVdata;
		public PPVData colorAdjustmentsPPVdata;
		public PPVData depthOfFieldPPVdata;
		public PPVData filmGrainPPVdata;
		public PPVData lensDistortionPPVdata;
		public PPVData motionBlurPPVdata;
		public PPVData splitToningPPVdata;
		public PPVData vignettePPVdata;
		public PPVData smhPPVdata;
		public PPVData channelMixerPPVdata;
	}
}