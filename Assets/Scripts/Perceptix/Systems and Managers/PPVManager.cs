using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using YBC.Neemotix;

namespace YBC.Perceptix
{
	public class PPVManager : MonoBehaviour
	{

		public NeemotixValuesAdapter neemotions;

		[Space]

		public Volume bloomPPV;
		public Volume chromaticAberrationPPV;
		public Volume ColorAdjustmentsPPV;
		public Volume depthOfFieldPPV;
		public Volume filmGrainPPV;
		public Volume lensDistortionPPV;
		public Volume motionBlurPPV;
		public Volume splitToningPPV;
		public Volume vignettePPV;
		public Volume smhPPV;
		public Volume channelMixerPPV;

		
		
		private void Awake()
		{
			checkPublicFields();
		}


		/// <summary>
		/// Throw Errors at Runtime if the PPVs are not connected!
		/// </summary>
		private void checkPublicFields()
		{
			if ( bloomPPV == null ) throw new System.Exception("YBC.NotAssignedError - A public field has not been assigned in the editor. Things will break!");
			if ( chromaticAberrationPPV == null ) throw new System.Exception("YBC.NotAssignedError - A public field has not been assigned in the editor. Things will break!");
			if ( ColorAdjustmentsPPV == null ) throw new System.Exception("YBC.NotAssignedError - A public field has not been assigned in the editor. Things will break!");
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

		}
	} 
}
