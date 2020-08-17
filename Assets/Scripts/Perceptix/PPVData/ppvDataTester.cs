using UnityEngine;
using System.Collections;
using YBC.Perceptix.PPVData;

namespace Assets.Scripts.Perceptix
{
	public class ppvDataTester : MonoBehaviour
	{

		private PPVDataObject pPVDataObject;


		public void Awake()
		{
			pPVDataObject = new PPVDataObject();
		}


		// Use this for initialization
		void Start()
		{

			Debug.Log( pPVDataObject.angstAdapter.ColorAdjustmentsPPV.weight[0] );

		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}