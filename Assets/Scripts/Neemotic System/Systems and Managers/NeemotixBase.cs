using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeemotixBase : MonoBehaviour
{
	protected static float neemotionMaxValue = 10f;
	protected static float neemotionMinValue = 0f;

	protected static Color urgendColor			= new Color(1, 0, 0);
	protected static Color unsatisfiedColor		= new Color(1, 0.6f, 0,2f);
	protected static Color satisfiedColor		= new Color(0.1f, 0.7f, 0.29f);
	protected static Color oversatisfiedColor	= new Color(0.8f, 0.67f, 0);
}
