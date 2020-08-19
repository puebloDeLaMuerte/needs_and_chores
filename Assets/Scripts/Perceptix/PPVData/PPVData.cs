using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using YBC.Perceptix;

namespace YBC.Perceptix.PPVData
{
	/// <summary>
	/// Data Storage Class that has fields for all Data the Perceptix Framework could need to know about a PostProcessVolume.
	/// For Use inside the DataAdapter Class
	/// </summary>
	public class PPVData
	{
		public (float, float)[] weight;
		public (float, float)[] postExposure;
		public (float, float)[] focusDistance;
	}
}