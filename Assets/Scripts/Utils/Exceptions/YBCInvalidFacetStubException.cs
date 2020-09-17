

using System;

namespace YBC.Utils.Error
{
	public class YBCInvalidFacetStubException : Exception
	{
		public YBCInvalidFacetStubException( string stub ) : base( "Invalid String. Not a Facet-Stub: " + stub )
		{
		}
	}
}