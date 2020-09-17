

using System;

namespace YBC.Utils.Error
{
	public class YBCInvalidSelectorException : Exception
	{
		public YBCInvalidSelectorException( object o ) : base( "Invalid value in YBC.Audix.Selector: " + o.ToString() )
		{
		}
	}
}