

using System;
using UnityEditor.Experimental.GraphView;
using YBC.Utils.Error;

namespace YBC.Audix
{
	public class Selector
	{
		private int selectorID;
		private string selectorName;
		private SelectorCompareType compareType;
		public readonly SelectorType selectorType;

		private string selectValueString = null;
		private int selectValueInt = int.MinValue;
		private float selectValueFloat = float.MinValue;

		public Selector( int id, string name, SelectorCompareType compareType, string selectValueString )
		{
			this.selectorID = id;
			this.selectorName = name;
			this.compareType = compareType;
			this.selectValueString = selectValueString;
			this.selectorType = SelectorType.STRING;
		}

		public Selector( int id, string name, SelectorCompareType compareType, int selectValueInt )
		{
			this.selectorID = id;
			this.selectorName = name;
			this.compareType = compareType;
			this.selectValueInt = selectValueInt;
			this.selectorType = SelectorType.INT;
		}

		public Selector( int id, string name, SelectorCompareType compareType, float selectValueFloat )
		{
			this.selectorID = id;
			this.selectorName = name;
			this.compareType = compareType;
			this.selectValueFloat = selectValueFloat;
			this.selectorType = SelectorType.FLOAT;
		}


		/// <summary>
		/// The ID is the same for a Selector and it's corresponding Data-Point-ID in the IInnerVoiceDataAdapter.
		/// </summary>
		public int getSelectorID()
		{
			return selectorID;
		}

		public bool EvaluateID( int idQuery )
		{
			if ( selectorID == idQuery )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Mostly for human-readability and debug printing. String representation, should be what the ID is a HashValue from (not guaranteed).
		/// </summary>
		public string getName()
		{
			return selectorName;
		}


		private bool Compare(int evaluationInt)
		{
			switch ( compareType )
			{
				case SelectorCompareType.EQUALS:
					if ( evaluationInt == selectValueInt ) return true;
					else return false;
				case SelectorCompareType.BIGGER_INCLUSIVE:
					if ( evaluationInt >= selectValueInt ) return true;
					else return false;
				case SelectorCompareType.BIGGER:
					if ( evaluationInt > selectValueInt ) return true;
					else return false;
				case SelectorCompareType.SMALLER_INCLUSIVE:
					if ( evaluationInt <= selectValueInt ) return true;
					else return false;
				case SelectorCompareType.SMALLER:
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
				case SelectorCompareType.EQUALS:
					if ( evaluationFloat == selectValueFloat ) return true;
					else return false;
				case SelectorCompareType.BIGGER_INCLUSIVE:
					if ( evaluationFloat >= selectValueFloat ) return true;
					else return false;
				case SelectorCompareType.BIGGER:
					if ( evaluationFloat > selectValueFloat ) return true;
					else return false;
				case SelectorCompareType.SMALLER_INCLUSIVE:
					if ( evaluationFloat <= selectValueFloat ) return true;
					else return false;
				case SelectorCompareType.SMALLER:
					if ( evaluationFloat < selectValueFloat ) return true;
					else return false;
				default:
					return false;
			}
		}

		/// <summary>
		/// Compares the stored select-Value against the given, according to this Selectors CompareType(EQUALS, BIGGER, BIGGER_INCLUSIVE etc)
		/// </summary>
		/// <param name="evaluationString">The value to check against this Selector</param>
		/// <returns>true if conditions are met. defaults to false</returns>
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


		/// <summary>
		/// Compares the stored select-Value against the given, according to this Selectors CompareType(EQUALS, BIGGER, BIGGER_INCLUSIVE etc)
		/// Will also compare the given int against a FloatSelector.
		/// </summary>
		/// <param name="evaluationint">The value to check against this Selector</param>
		/// <returns>true if conditions are met. defaults to false</returns>
		public bool Evaluate( int evaluationint )
		{
			if ( selectValueInt != int.MinValue ) return Compare( evaluationint );
			else if ( selectValueFloat != float.MinValue ) return Compare( (float)evaluationint );
			else throw new YBCInvalidSelectorException( evaluationint );
		}


		/// <summary>
		/// Compares the stored select-Value against the given, according to this Selectors CompareType(EQUALS, BIGGER, BIGGER_INCLUSIVE etc)
		/// Will also compare the given float against an intSelector. !! Rounding involved !!
		/// </summary>
		/// <param name="evaluationfloat">The value to check against this Selector</param>
		/// <returns>true if conditions are met. defaults to false</returns>
		public bool Evaluate( float evaluationfloat )
		{
			if ( selectValueFloat != float.MinValue ) return Compare( evaluationfloat );
			else if ( selectValueInt != int.MinValue ) return Compare( (int)Math.Round( evaluationfloat, 0 ) );
			else throw new YBCInvalidSelectorException( evaluationfloat );
		}



		public override string ToString()
		{
			string r = "Selector: " + this.selectorID + " - " + this.selectorName + ": " + this.compareType + " ";
			switch ( this.selectorType )
			{
				case SelectorType.FLOAT:
					r += selectValueFloat;
					break;
				case SelectorType.INT:
					r += selectValueInt;
					break;
				case SelectorType.STRING:
					r += selectValueString;
					break;
				default:
					break;
			}
			return r;
		}
	}
}