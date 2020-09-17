

using System;

namespace YBC.Utils.Error
{
	public class YBCInvalidFacetAccuracyException : Exception
	{
		public YBCInvalidFacetAccuracyException(string accord) : base ("Invalid Facet-Accuracy-String: " + accord)
		{

		}
	}
}