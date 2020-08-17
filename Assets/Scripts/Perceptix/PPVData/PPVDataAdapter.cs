using UnityEngine;
using System.Collections;
using YBC.Perceptix.PPVData.PPVolumes;

namespace YBC.Perceptix.PPVData
{
	public class PPVDataAdapter
	{
		public BloomPPV bloomPPV;
		public ChromaticAberrationPPV chromaticAberrationPPV;
		public ColorAdjustmentsPPV ColorAdjustmentsPPV;
		public DepthOfFieldPPV depthOfFieldPPV;
		public FilmGrainPPV filmGrainPPV;
		public LensDistortionPPV lensDistortionPPV;
		public MotionBlurPPV motionBlurPPV;
		public SplitToningPPV splitToningPPV;
		public VignettePPV vignettePPV;
		public SmhPPV smhPPV;

		public PPVbase[] GetAllVolumeData()
		{
			
		}
	}
}