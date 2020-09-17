

using System;

namespace YBC.Utils.Error
{
	public class YBCInvalidInputFileException : Exception
	{
		public YBCInvalidInputFileException( object o ) : base( "Invalid File-Input in YBC.Audix.SoundItemFactory: " + o.ToString() ) {}
	}
}