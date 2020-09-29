﻿

using System;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using YBC.Audix.InnerVoice;
using YBC.Utils.Error;

namespace YBC.Audix
{

	public static class SoundItemFactory
	{

		//TODO: Make a new Factory one for each Type of SoundItem? (Wegen Dependency, weisst schon...)

		/// <summary>
		/// Parses the AudioClip.name property derived from the corresponding filename on disk. The filename holds all data relevant in this step. Just a parser, no other data-sources, comparison, correllation etc intended here!
		/// </summary>
		/// <param name="inclip"></param>
		/// <returns></returns>
		public static SoundItem TryParseClip(AudioClip inclip)
		{
			try
			{
				String[] filenameElements = inclip.name.Split( '_' );

				if ( !filenameElements[0].Equals( "SND" ) )
				{
					throw new YBCInvalidInputFileException( inclip.name );
				}

				char variant = filenameElements[filenameElements.Length - 1][0];
				string text = filenameElements[filenameElements.Length - 2];

				string soundCategory = filenameElements[1];
				string collectionType = filenameElements[2];

				switch ( soundCategory )
				{
					case "IV":

						switch ( collectionType )
						{

							case "IPIP120": return ParseIPIP120IVitem( inclip, filenameElements, variant, text );

							case "NEEMOTION": return ParseNeemotionIVitem( inclip, filenameElements, variant, text );

							case "INTERACTION": return ParseInteractionIVitem( inclip, filenameElements, variant, text );

							default: throw new YBCInvalidInputFileException( inclip.name );
						}


					default: throw new YBCInvalidInputFileException( inclip.name );
				}
			} catch	(Exception e)
			{
				Debug.LogError( "Error Parsing AudioClip: " +e.ToString() );
				return null;
			}
		}



		private static InnerVoiceItem ParseInteractionIVitem( AudioClip inclip, string[] elements, char variant, string text )
		{
			throw new NotImplementedException();
		}



		private static InnerVoiceItem ParseNeemotionIVitem( AudioClip inclip, string[] elements, char variant, string text )
		{
			Selector[] selects = new Selector[1];

			int selectorID = elements[3].GetHashCode();
			string selectorName = elements[3];

			float urgency, immediacy;
			immediacy = 0.6f;

			urgency = float.Parse(elements[6], CultureInfo.InvariantCulture );

			int neemoStatusint;
			switch ( elements[5] )
			{
				case "vlow": neemoStatusint = 0; break;
				case "low": neemoStatusint = 1; break;
				case "med": neemoStatusint = 2; break;
				case "high": neemoStatusint = 3; break;
				default: neemoStatusint = -1; break;
			}

			selects[0] = new Selector( selectorID, selectorName, SelectorCompareType.EQUALS, neemoStatusint);

			return new InnerVoiceItem( inclip, text, variant, selects, urgency, immediacy);
		}



		private static InnerVoiceItem ParseIPIP120IVitem( AudioClip inclip, string[] elements, char variant, string text )
		{

			Selector[] selects = new Selector[2];

			string selectorName = IPIPLookup.GetFacetName( elements[3] );
			int selectorID = selectorName.GetHashCode();

			float urgency, immediacy;
			immediacy = IPIPLookup.GetImmediacy();
			char key = elements[3][0];
			string accuracy = elements[4];
			urgency = IPIPLookup.CalculateUrgency(key, accuracy);


			(float, float) minMax = IPIPLookup.GetMinMaxSelectorValues( key, accuracy );
			
			selects[0] = new Selector( selectorID, selectorName, SelectorCompareType.BIGGER_INCLUSIVE , minMax.Item1);
			selects[1] = new Selector( selectorID, selectorName, SelectorCompareType.SMALLER_INCLUSIVE, minMax.Item2 );

			return new InnerVoiceItem( inclip, text, variant, selects, urgency, immediacy );
		}
	}




	public static class IPIPLookup
	{

		public static float generalIPIPimmediacy = 0.5f;

		public static float UrgencyMultiplyer = 0.7f;

		public static float accuracy_plusplus = 1f;
		public static float accuracy_plus = 0.7f;
		public static float accuracy_zero = 0f;
		public static float accuracy_minus = 0.7f;
		public static float accuracy_minusminus = 1f;


		/// <summary>
		/// Returns the facet names used in ExtremeAI
		/// </summary>
		/// <param name="facetStub">the stub to look up. Can be in the form of "A1", "+A1" or "-A1". Everything else will throw an Exception</param>
		/// <returns>ExAi facet name</returns>
		public static string GetFacetName( string facetStub )
		{

			if( facetStub.Length == 3)
			{
				facetStub = facetStub.Substring( 1, 2 );
			}
			
			switch ( facetStub )
			{
				case "A1": return "Trust";
				case "A2": return "Compliance";
				case "A3": return "Altruism";
				case "A4": return "Straightforwardness";
				case "A5": return "Modesty";
				case "A6": return "Tender-Mindedness";

				case "C1": return "Competence";
				case "C2": return "Order";
				case "C3": return "Dutifulness";
				case "C4": return "Achievement Striving";
				case "C5": return "Self-Discipline";
				case "C6": return "Deliberation";

				case "E1": return "Warmth";
				case "E2": return "Gregariousness";
				case "E3": return "Assertiveness";
				case "E4": return "Activity";
				case "E5": return "Excitement Seeking";
				case "E6": return "Positive Emotions";

				case "N1": return "Anxiety";
				case "N2": return "Angry Hostility";
				case "N3": return "Depression";
				case "N4": return "Self-Consciousness";
				case "N5": return "Impulsiveness";
				case "N6": return "Vulnerability";

				case "O1": return "Fantasy";
				case "O2": return "Aesthetics";
				case "O3": return "Feelings";
				case "O4": return "Actions";
				case "O5": return "Ideas";
				case "O6": return "Values";

				default: throw new YBCInvalidFacetStubException( facetStub );
			}
		}

		/// <summary>
		/// Gets the integer-ID for lookup purposes. Calls GetFacetName internally.
		/// </summary>
		/// <param name="facetStub">the stub to look up. Can be in the form of "A1", "+A1" or "-A1". Everything else will throw an Exception</param>
		/// <returns>the hash-value of the corresponding facet-name.</returns>
		public static int GetFacetID( string facetStub )
		{
			return GetFacetName( facetStub ).GetHashCode();
		}


		/// <summary>
		/// An algorithm that determines an urgency value for each IPIP-IV-soundItem according to it's Key and Accuracy (-C1_--) -> (KEY C1_SIGN)
		/// </summary>
		/// <param name="key">'+' or '-'</param>
		/// <param name="accuracy"></param>
		/// <returns></returns>
		public static float CalculateUrgency(char key, string accuracy)
		{

			int accInt = GetValueZoneIdentifier( key, accuracy );

			float returnFloat;

			switch ( accInt )
			{
				case  2: returnFloat = accuracy_plusplus   * UrgencyMultiplyer; break;
				case  1: returnFloat = accuracy_plus       * UrgencyMultiplyer; break;
				case  0: returnFloat = accuracy_zero       * UrgencyMultiplyer; break;
				case -1: returnFloat = accuracy_minus      * UrgencyMultiplyer; break;
				case -2: returnFloat = accuracy_minusminus * UrgencyMultiplyer; break;
				default: returnFloat = 0f; break;
			}

			return returnFloat;
		}


		/// <summary>
		/// returns an int that signifies the value-zone for a Key-Accuracy pair.
		/// </summary>
		/// <param name="key">this IPIP-IVitems key</param>
		/// <param name="accuracy">this IPIP-IVitems accuracy</param>
		/// <returns>int: Key-Agnostic: '++' = 2, '+' = 1, '0' = 0, '-' = -1; '--' = -2</returns>
		private static int GetValueZoneIdentifier( char key, string accuracy )
		{
			int accInt;
			switch ( accuracy )
			{
				case "++": accInt = 2; break;
				case "+": accInt = 1; break;
				case "0": accInt = 0; break;
				case "-": accInt = -1; break;
				case "--": accInt = -2; break;
				default: throw new YBCInvalidFacetAccuracyException( accuracy );
			}

			switch ( key )
			{
				case '+': accInt *= 1; break;
				case '-': accInt *= -1; break;
				default: throw new YBCInvalidFacetAccuracyException( accuracy );
			}
			
			return accInt;
		}



		/// <summary>
		/// Get the facet-scale-range for this IVitem to set it's selectors accordingly.
		/// </summary>
		/// <param name="key">this IPIP-IVitems key</param>
		/// <param name="accuracy">this IPIP-IVitems accuracy</param>
		/// <returns>(float, float) min and max values</returns>
		public static (float, float) GetMinMaxSelectorValues( char key, string accuracy )
		{
			int accInt = GetValueZoneIdentifier( key, accuracy );

			switch ( accInt )
			{
				case  2: return (80f, 100f);
				case  1: return (60f, 80f);  
				case  0: return (40f, 60f);
				case -1: return (20f, 40f);
				case -2: return (0f, 20f);
				default: return(0f,0f);
			}
		}



		/// <summary>
		/// Just a fixed value for the immediacy associated with all IPIP items.
		/// </summary>
		/// <returns>float: IPIP general immediacy</returns>
		public static float GetImmediacy()
		{

			return generalIPIPimmediacy;
		}
	}
}