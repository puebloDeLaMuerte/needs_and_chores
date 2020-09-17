

using System;

namespace YBC.Utils.Error
{
	public class YBCInvalidFacetKeyException : Exception
	{
		public YBCInvalidFacetKeyException( char stub ) : base( "Invalid char. Not a Facet-Key: " + stub )
		{
		}
	}
}