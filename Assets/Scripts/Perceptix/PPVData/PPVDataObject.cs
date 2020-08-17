using Assets.Scripts.Perceptix;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YBC.Perceptix.PPVData.PPVolumes;

namespace YBC.Perceptix.PPVData
{
	public class PPVDataObject : ScriptableObject
	{


		// F R E U D E

		public PPVDataAdapter freudeAdapter = new PPVDataAdapter()
		{
			bloomPPV = new BloomPPV()
			{
				weight = new (float, float)[2]
				{
					(5f, 0f), (10f, 0.7f)
				}
			},

			chromaticAberrationPPV = new ChromaticAberrationPPV()
			{
				weight = new (float, float)[1]
				{
					(-1f, 0.1f)
				}
			},

			ColorAdjustmentsPPV = new ColorAdjustmentsPPV()
			{
				weight = new (float, float)[1]
				{
					(-1f, 0.6f)
				},

				postExposure = new (float, float)[1]
				{
					(-1f, 1f)
				}
			}
		};



		/// A N G S T

		public PPVDataAdapter angstAdapter = new PPVDataAdapter()
		{
			ColorAdjustmentsPPV = new ColorAdjustmentsPPV()
			{
				weight = new (float, float)[2]
				{
					(5f, 0f), (10f, 0.6f)
				},
				

				postExposure = new (float, float)[1]
				{
					(-1f, -2f)
				}
			},

			vignettePPV = new VignettePPV()
			{
				weight = new (float, float)[2]
				{
					(6f, 0f), (10f, 0.6f)
				}
			},

			smhPPV = new SmhPPV()
			{
				weight = new (float, float)[2]
				{
					(5f, 0f), (10f, 0.2f)
				}
			},

			motionBlurPPV = new MotionBlurPPV()
			{
				weight = new (float, float)[2]
				{
					(8f, 0f), (10f, 0.5f)
				}
			}
		};



		//   S C H M E R Z

		PPVDataAdapter schmerzAdapter = new PPVDataAdapter()
		{
			//......................!
		};
	}
}
