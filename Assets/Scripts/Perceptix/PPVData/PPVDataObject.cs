using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YBC.Perceptix.PPVData;

namespace YBC.Perceptix.PPVData
{
	public class PPVDataObject : ScriptableObject
	{


		// F R E U D E

		public PPVDataAdapter freudeAdapter = new PPVDataAdapter()
		{
			bloomPPVdata = new BloomPPVdata()
			{
				weight = new (float, float)[2]
				{
					(5f, 0f), (10f, 0.7f)
				}
			},

			chromaticAberrationPPVdata = new ChromaticAberrationPPVdata()
			{
				weight = new (float, float)[1]
				{
					(-1f, 0.1f)
				}
			},

			ColorAdjustmentsPPVdata = new ColorAdjustmentsPPVdata()
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
			ColorAdjustmentsPPVdata = new ColorAdjustmentsPPVdata()
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

			vignettePPVdata = new VignettePPVdata()
			{
				weight = new (float, float)[2]
				{
					(6f, 0f), (10f, 0.6f)
				}
			},

			smhPPVdata = new SmhPPVdata()
			{
				weight = new (float, float)[2]
				{
					(5f, 0f), (10f, 0.2f)
				}
			},

			motionBlurPPVdata = new MotionBlurPPVdata()
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
