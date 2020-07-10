using System;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

namespace YBC.Utils
{
	class YBCTimer : MonoBehaviour
	{
		public Text timerGui;
		public float startTimeHours = 0;

		[OnValueChanged("TimeScaleEditorCallback")]
		public int timeScale = 40;
		private void TimeScaleEditorCallback()
		{
			SetTimeScales(timeScale);
		}

		public void Awake()
		{
			SetTimeScales(timeScale);
		}

		public static void SetTimeScales( float scale )
		{
			UnityEngine.Time.timeScale = scale;
			//UnityEngine.Time.fixedDeltaTime = scale;

			Debug.Log("The Time-Scale is: " + UnityEngine.Time.timeScale + ". FixedDeltaTime is " + UnityEngine.Time.fixedDeltaTime);
		}

		private float getYBCTime()
		{
			return Time.time + ( startTimeHours * 3600f );
		}

		public String GetTimeAsString()
		{
			decimal h = Math.Floor( (Decimal)getYBCTime() / 3600 % 24 );
			decimal m = Math.Floor( (Decimal)getYBCTime() / 60 % 60 );
			decimal s = Math.Floor( (Decimal)getYBCTime() % 60 );

			return h.ToString("00") + ":" + m.ToString("00") + ":" + s.ToString("00");
		}

		public static float GetDeltaHours()
		{
			float delta = (Time.deltaTime / 3600f);
			return delta;
		}

		public void Update()
		{
			timerGui.text = GetTimeAsString();
		}
	}
}
