using System;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

namespace YBC.Utils
{
	class YBCTimer : MonoBehaviour
	{
		public Text timerGuiText;
		public float startTimeHours = 0;

		public int timeScale = 40;

		private static YBCTimer _instance;
		internal static YBCTimer Instance { get => _instance; }

		public void Awake()
		{
			_instance = this;
		}

		private float getYBCTime()
		{
			return (Time.time * timeScale) + ( startTimeHours * 3600f );
		}


		public String GetTimeAsString()
		{
			decimal d = Math.Floor( (Decimal)getYBCTime() / 86400 % 24);
			decimal h = Math.Floor( (Decimal)getYBCTime() / 3600 % 24 );
			decimal m = Math.Floor( (Decimal)getYBCTime() / 60 % 60 );
			decimal s = Math.Floor( (Decimal)getYBCTime() % 60 );

			//return h.ToString("00") + ":" + m.ToString("00") + ":" + s.ToString("00");
			return "Tag " + d + "\n" + h.ToString("00") + ":" + m.ToString("00");
		}


		public float GetDeltaHours()
		{
			float delta = (Time.deltaTime * timeScale / 3600f);
			return delta;
		}


		public void Update()
		{
			timerGuiText.text = GetTimeAsString();
		}
	}
}
