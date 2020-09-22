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

			Debug.Log( "value for: " + aiCharacter.CharSingleFacetTool("Achievement Striving") );
			Debug.Log( "value for fun: " + aiCharacter.CharSingleFacetTool( "placeholder" ) );
			PrintAllFacets();
		}


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

		public float getFloatForSelectorID( int selectorID, int selectorDepth )
		{
			float f;
			string s;

			facetNameLookup.TryGetValue( selectorID, out s  );
			f = aiCharacter.CharSingleFacetTool( s );

			return f;
		}

		public int getIntForSelectorID( int selectorID, int selectorDepth )
		{
			throw new NotImplementedException();
		}

		public string getStringForSelectorID( int selectorID, int selectorDepth )
		{
			throw new NotImplementedException();
		}
	}
}