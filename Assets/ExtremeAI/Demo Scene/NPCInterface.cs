using UnityEngine;
using System.Collections;
using TaiPE_Lib_v3;

public class NPCInterface : MonoBehaviour {

	public AICharInfo myAIChar = new AICharInfo();
	string myName = "Buttons";
	
	// Use this for initialization
	void Start () {
	
		Debug.Log ("NOTE: The scripts in the Demo Scene folder contain the complete code for the QuickStart demo included as a pdf with Extreme AI. In order to fully understand the code, please follow the steps in the QuickStart guide!");
		Debug.Log ("Also note that the character Buttons has been created in the Editor so that this Demo Scene will work.");
	
		myAIChar.Init (this.tag,myName);
		
		int am_I_Intimidating = myAIChar.AIReturnResult ("Player","intimidating",false);
		
		Debug.Log ("value of am_I_intimidating: " + am_I_Intimidating);
		
		string well_am_i = "";
		
		switch(am_I_Intimidating) {
		case 0: //Nope! Just the opposite
			well_am_i = "I shiver even to think of it!";
			break;
		case 1: //Not so much
			well_am_i = "Not really";
			break;
		case 2: //Average
			well_am_i = "Sometimes, but sometimes not ...";
			break;
		case 3: //Yeah, kinda intimidating
			well_am_i = "Yeah, I am. What of it?";
			break;
		case 4: //You betcha!
			well_am_i = "Are you lookin' at me? Are you lookin' at me?!";
			break;
		}
		
		Debug.Log ("Do I feel intimidating? " + well_am_i);
		
		
		//change character's intimidation score and check again
		for(int i = 0; i < 15; i++)
		{
			myAIChar.AINoResult ("Player","intimidating",false);
		}
		
		am_I_Intimidating = myAIChar.AIReturnResult ("Player","intimidating",false);
		
		Debug.Log ("After several situations making Buttons feel less intimidating ...");
		Debug.Log ("new value of am_I_intimidating: " + am_I_Intimidating);
		
		//now create new character
		float[] myFacets = new float[30];
		for(int i=0;i<30;i++)
		{
			myFacets[i] = 50f;
		}
		string newChar = myAIChar.CreateCharacter ("Zips",myFacets,false,"");
		Debug.Log ("Created character? " + newChar);

        myAIChar.SavePersonalityAndInit();
		
		if(newChar=="success")
		{
			GameObject.Find("Zips").AddComponent<ZipsInterface>();
		}
    }
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
