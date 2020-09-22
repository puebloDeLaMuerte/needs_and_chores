using UnityEngine;
using System.Collections;
using TaiPE_Lib_v3;

public class ZipsInterface : MonoBehaviour {

	public AICharInfo myAIChar = new AICharInfo();
	string myName = "Zips";
	
	// Use this for initialization
	void Start () {
		myAIChar.Init (this.tag,myName,false);
		
		//get all character names
		string[] allCharNames = new string[0];
		
		Debug.Log ("All character names (should include Zips):");
		
		allCharNames = myAIChar.GetCharNames();
		
		for(int i = 0; i < allCharNames.Length; i++)
		{
			Debug.Log (allCharNames[i]);
		}
		/*
		//add the following only if you have the full version of ExAI2!
		string[] myFacetNames = new string[0];
		float[] myFacets = new float[0];
		
		Debug.Log ("Name: " + myName);
		
		myAIChar.CharFacetsTool(ref myFacetNames, ref myFacets);
		for(int i = 1; i <= 30; i++)
		{
			Debug.Log (myFacetNames[i] + ": " + myFacets[i]);
		}*/
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
