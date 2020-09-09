using System;
using System.Collections;
using YBC.Utils.Error;

namespace YBC.Utils.Error
{
	public class YBCMultipleHighlanderClassesError : YBCErrorMessage
	{
		public YBCMultipleHighlanderClassesError(Type highlanderClass)
		{
			this.errorText = "You have multiple Instances of " + highlanderClass.ToString() + " in your project.There can only be one!";
		}
		
	}
}