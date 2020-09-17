

using System;
using YBC.Utils.Error;

namespace YBC.Audix
{
	public class Selector
	{
		private int id;
		private string name;
		private CompareType compareType;

		private string selectValueString = null;
		private int selectValueInt = int.MinValue;
		private float selectValueFloat = float.MinValue;

		public Selector( int id, string name, CompareType compareType, string selectValueString )
		{
			this.id = id;
			this.name = name;
			this.compareType = compareType;
			this.selectValueString = selectValueString;
		}

		public Selector( int id, string name, CompareType compareType, int selectValueInt )
		{
			this.id = id;
			this.name = name;
			this.compareType = compareType;
			this.selectValueInt = selectValueInt;
		}

		public Selector( int id, string name, CompareType compareType, float selectValueFloat )
		{
			this.id = id;
			this.name = name;
			this.compareType = compareType;
			this.selectValueFloat = selectValueFloat;
		}


		private bool Compare(int evaluationInt)
		{
			switch ( compareType )
			{
				case CompareType.EQUALS:
					if ( evaluationInt == selectValueInt ) return true;
					else return false;
				case CompareType.BIGGER_INCLUSIVE:
					if ( evaluationInt >= selectValueInt ) return true;
					else return false;
				case CompareType.BIGGER:
					if ( evaluationInt > selectValueInt ) return true;
					else return false;
				case CompareType.SMALLER_INCLUSIVE:
					if ( evaluationInt <= selectValueInt ) return true;
					else return false;
				case CompareType.SMALLER:
					if ( evaluationInt < selectValueInt ) return true;
					else return false;
				default:
					return false;
			}
		}

		private bool Compare( float evaluationFloat )
		{
			switch ( compareType )
			{
				case CompareType.EQUALS:
					if ( evaluationFloat == selectValueFloat ) return true;
					else return false;
				case CompareType.BIGGER_INCLUSIVE:
					if ( evaluationFloat >= selectValueFloat ) return true;
					else return false;
				case CompareType.BIGGER:
					if ( evaluationFloat > selectValueFloat ) return true;
					else return false;
				case CompareType.SMALLER_INCLUSIVE:
					if ( evaluationFloat <= selectValueFloat ) return true;
					else return false;
				case CompareType.SMALLER:
					if ( evaluationFloat < selectValueFloat ) return true;
					else return false;
				default:
					return false;
			}
		}


		public bool Evaluate(string evaluationString)
		{
			if ( selectValueString == null )
			{
				throw new YBCInvalidSelectorException( evaluationString );
			}
			else if( selectValueString == evaluationString)
			{
				return true;
			} else
			{
				return false;
			}
		}

		public bool Evaluate( int evaluationint )
		{
			if ( selectValueInt != int.MinValue ) return Compare( evaluationint );
			else if ( selectValueFloat != float.MinValue ) return Compare( (float)evaluationint );
			else throw new YBCInvalidSelectorException( evaluationint );
		}


		public bool Evaluate( float evaluationfloat )
		{
			if ( selectValueFloat != float.MinValue ) return Compare( evaluationfloat );
			else if ( selectValueInt != int.MinValue ) return Compare( (int)Math.Round( evaluationfloat, 0 ) );
			else throw new YBCInvalidSelectorException( evaluationfloat );
		}
	}
}