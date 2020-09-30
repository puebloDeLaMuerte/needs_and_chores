using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TaiPE_Lib_v3;
using UnityEngine;
using YBC.Audix.InnerVoice;

namespace YBC.Personalix
{
	public class ExAiAdapter : MonoBehaviour, IInnerVoiceDataAdapter
	{

		public string characterName;
		public AICharInfo aiCharacter = new AICharInfo();

		private Dictionary<int, string> facetNameLookup;

		void Start()
		{
			aiCharacter.Init( this.tag, characterName );

			InitFacetNameLookupTable();

			PrintAllFacets();
		}

		/// <summary>
		/// Set up the lookup table with hashed Facet-Names from the strings array returned by ExAi.CharFacetsTool.
		/// </summary>
		private void InitFacetNameLookupTable()
		{
			facetNameLookup = new Dictionary<int, string>();

			string[] myFacetNames = new string[0];
			float[] myFacets = new float[0];
			aiCharacter.CharFacetsTool( ref myFacetNames, ref myFacets );

			foreach ( string name in myFacetNames )
			{
				facetNameLookup.Add( name.GetHashCode(), name );
			}
		}

		/// <summary>
		/// DEBUG: Print all character names known to ExAi.
		/// </summary>
		public void PrintAllCharacters()
		{
			Debug.Log( "### All character names known to ExAi ###" );

			string[] allCharNames = new string[0];

			allCharNames = aiCharacter.GetCharNames();
			for ( int i = 0; i < allCharNames.Length; i++ )
			{
				Debug.Log( allCharNames[i] );
			}
		}
		/// <summary>
		/// DEBUG: print all Facets and their values for this character
		/// </summary>
		public void PrintAllFacets()
		{
			Debug.Log( "### Character-Facet Info for: " + characterName + " ###" );

			string[] myFacetNames = new string[0];
			float[] myFacets = new float[0];
			aiCharacter.CharFacetsTool( ref myFacetNames, ref myFacets );

			for ( int i = 1; i <= 30; i++ )
			{

				string white = " ";
				for ( int w = 0; w < (25 - myFacetNames[i].Length); w++ )
				{
					white += " ";
				}
				Debug.Log( myFacetNames[i] + ":" + white + myFacets[i] );
			}
		}


		// IInnerVoiceDataAdapter Methods:

		/// <summary>
		/// Tuple-Array for ExAi Data Values
		/// </summary>
		/// <returns>(facet-Integer-ID, facet-name)</returns>
		public (int, string)[] getAllSelectorIDs()
		{
			(int, string)[] returnarray = new (int, string)[facetNameLookup.Count];

			int i = 0;
			foreach ( var item in facetNameLookup )
			{
				returnarray[i] = (item.Key, item.Value);
				i++;
			}
			return returnarray;
		}

		/// <summary>
		/// Looks up this Characters Value for a Single Faced corresponding to the int ID given.
		/// </summary>
		/// <param name="selectorID">The Integer-ID of the facet to look up (Compared against internal Dicitonary)</param>
		/// <param name="selectorDepth">not used here</param>
		/// <returns>a float value (0-100) for the facet specified. Defaults to -1f on failure</returns>
		public float getFloatForSelectorID( int selectorID, int selectorDepth )
		{
			float f = -1f;
			string s;

			facetNameLookup.TryGetValue( selectorID, out s  );
			f = aiCharacter.CharSingleFacetTool( s );

			return f;
		}


		/// <summary>
		/// Integers not supported for ExAi. Will throw an Exception.
		/// </summary>
		public int getIntForSelectorID( int selectorID, int selectorDepth )
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Strings not supported for ExAi. Will throw an Exception.
		/// </summary>
		public string getStringForSelectorID( int selectorID, int selectorDepth )
		{
			throw new NotImplementedException();
		}
	}
}