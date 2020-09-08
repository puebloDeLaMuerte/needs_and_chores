using UnityEngine;
using System.Collections;

namespace YBC.Utils.Error
{
	public class YBCErrorMessage
	{
		protected string errorText;

		public override string ToString()
		{
			return errorText;
		}
	}
}