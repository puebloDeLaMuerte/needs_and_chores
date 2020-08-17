using YBC.Perceptix;
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
					(4f, 0.1f)
				}
			},

			colorAdjustmentsPPVdata = new ColorAdjustmentsPPVdata()
			{
				weight = new (float, float)[2]
				{
					(0f, 0f), (10f, 0.6f)
				},

				postExposure = new (float, float)[1]
				{
					(-1f, 1f)
				}
			}
		};



		// T R A U E R

		public PPVDataAdapter trauerAdapter = new PPVDataAdapter()
		{
			channelMixerPPVdata = new ChannelMixerPPVdata()
			{
				weight = new (float, float)[2]
				{
					(4f,0f), (10f,1f)
				}
			}
		};



		/// A N G S T

		public PPVDataAdapter angstAdapter = new PPVDataAdapter()
		{
			colorAdjustmentsPPVdata = new ColorAdjustmentsPPVdata()
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


			motionBlurPPVdata = new MotionBlurPPVdata()
			{
				weight = new (float, float)[2]
				{
					(8f, 0f), (10f, 0.5f)
				}
			}
		};



		//   S C H M E R Z

		public PPVDataAdapter schmerzAdapter = new PPVDataAdapter()
		{
			chromaticAberrationPPVdata = new ChromaticAberrationPPVdata()
			{
				weight = new (float, float)[2]
				{
					(5f, 0f), (10f, 1f)
				}
			},


			colorAdjustmentsPPVdata = new ColorAdjustmentsPPVdata()
			{
				weight = new (float, float)[2]
				{
					(0f, 0f), (10f, 0.5f)
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
					(3f, 0f), (10f, 0.4f)
				}
			},


			depthOfFieldPPVdata = new DepthOfFieldPPVdata()
			{
				weight = new (float, float)[2]
				{
					(6f, 0f), (6.1f, 1f)
				},


				focusDistance = new (float, float)[2]
				{
					(6f, 1000f), (10f, 0.1f)
				}
			},


			lensDistortionPPVdata = new LensDistortionPPVdata()
			{
				weight = new (float, float)[2]
				{
					(7f, 0f), (10f, 0.8f)
				},
			},

			smhPPVdata = new SmhPPVdata()
			{
				weight = new (float, float)[2]
				{
					(3f, 0f), (10f, 0.6f)
				},
			},

			
			motionBlurPPVdata = new MotionBlurPPVdata()
			{
				weight = new (float, float)[2]
				{
					(7f, 0f), (10f, 0.6f)
				}
			}
		};



		// W U T

		public PPVDataAdapter wutAdapter = new PPVDataAdapter()
		{
			colorAdjustmentsPPVdata = new ColorAdjustmentsPPVdata()
			{
				weight = new (float, float)[2]
				{
					(5f, 0f), (10f, 0.8f)
				},


				postExposure = new (float, float)[1]
				{
					(-1f, 0.5f)
				}
			},


			vignettePPVdata = new VignettePPVdata()
			{
				weight = new (float, float)[2]
				{
					(5f, 0f), (10f, 0.8f)
				}
			},


			smhPPVdata = new SmhPPVdata()
			{
				weight = new (float, float)[2]
				{
					(5f, 0f), (10f, 0.9f)
				}
			},


			filmGrainPPVdata = new FilmGrainPPVdata()
			{
				weight = new (float, float)[2]
				{
					(7f, 0f), (10f, 1f)
				}
			}

		};



		// E K E L

		public PPVDataAdapter ekelAdapter = new PPVDataAdapter()
		{
			splitToningPPVdata = new SplitToningPPVdata()
			{
				weight = new (float, float)[2]
				{
					(5f,0f), (10f,1f)
				}
			},


			filmGrainPPVdata = new FilmGrainPPVdata()
			{
				weight = new (float, float)[2]
				{
					(4f, 0f), (10f, 1f)
				}
			}

		};



		// S T R E S S

		public PPVDataAdapter stressAdapter = new PPVDataAdapter()
		{
			motionBlurPPVdata = new MotionBlurPPVdata()
			{
				weight = new (float, float)[2]
				{
					(7f, 0f), (10f, 0.7f)
				}
			}
		};



		//   G E S U N D H E I T

		public PPVDataAdapter gesundheitAdapter = new PPVDataAdapter()
		{
			bloomPPVdata = new BloomPPVdata()
			{
				weight = new (float, float)[2]
				{
					(5f, 0f), (10f, 0.7f)
				}
			},


			colorAdjustmentsPPVdata = new ColorAdjustmentsPPVdata()
			{
				weight = new (float, float)[2]
				{
					(0f, 0f), (10f, 0.3f)
				},


				postExposure = new (float, float)[4]
				{
					(0f, -2f), (3f, 0f), (6f, 0f), (10f, 1f)
				}
			},


			vignettePPVdata = new VignettePPVdata()
			{
				weight = new (float, float)[2]
				{
					(0f, 0.7f), (3f, 0f)
				}
			},


			depthOfFieldPPVdata = new DepthOfFieldPPVdata()
			{
				weight = new (float, float)[2]
				{
					(2f, 1f), (2.1f, 0f)
				},


				focusDistance = new (float, float)[2]
				{
					(0f, 0.1f), (2f, 1000f)
				}
			},


			lensDistortionPPVdata = new LensDistortionPPVdata()
			{
				weight = new (float, float)[2]
				{
					(0f, 0.3f), (3f, 0f)
				}
			}

		};

	}
}
