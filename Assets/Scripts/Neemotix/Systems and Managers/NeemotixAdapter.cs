using System;
using UnityEngine;
using YBC.Neemotix;

public class NeemotixAdapter : MonoBehaviour, INeemotixAdapter
{

	private Neemotion[] neemotions;

	public void Awake()
	{
		PopulatetheArrays();
		
	}

	/// <summary>
	/// Populates the adapters neemotions[] with the neemotion that the changeManager knows. Emotions first, Needs second (in case you want to know for performance reasons when looking them up later on)
	/// </summary>
	private void PopulatetheArrays()
	{
		ChangeManager changeManager = GetComponentInChildren<ChangeManager>();
		Neemotion[] needs = changeManager.needsCollectionObject.GetComponentsInChildren<Neemotion>();
		Neemotion[] emotions = changeManager.emotionsCollectionObject.GetComponentsInChildren<Neemotion>();

		neemotions = new Neemotion[needs.Length + emotions.Length];

		Array.Copy(emotions, neemotions, emotions.Length);
		Array.Copy(needs, 0, neemotions, emotions.Length, needs.Length);
	}



	/// <summary>
	/// Looks up the Neemotions current Status by the given String (beware! String-compare! bad performance!)
	/// </summary>
	/// <param name="neemotionName">The Name of the Neemotion in Question</param>
	/// <returns>a touple containing the Neemotions current value and status. (0f,undefined) if no Neemotion found.</returns>
	public (float, NeemotionStatus) GetNeemotionStateByName(String neemotionName)
	{
		(float, NeemotionStatus) returnVal = (0,NeemotionStatus.Undefined);

		foreach ( Neemotion neemotion in neemotions )
		{
			if( neemotion.neemotionName.Equals(neemotionName) )
			{
				returnVal =  (neemotion.currentValue, neemotion.Status);
			}
		}

		return returnVal;
	}

	/// <summary>
	/// Looks up the Neemotions current Status by the given ID 
	/// </summary>
	/// <param name="neemotionName">The ID of the Neemotion in Question</param>
	/// <returns>a touple containing the Neemotions current value and status. (0f,undefined) if no Neemotion found..</returns>
	public (float, NeemotionStatus) GetNeemotionStateByID( int neemotionID )
	{
		(float, NeemotionStatus) returnVal = (0, NeemotionStatus.Undefined);

		foreach ( Neemotion neemotion in neemotions )
		{
			if ( neemotion.GetID() == neemotionID )
			{
				returnVal = (neemotion.currentValue, neemotion.Status);
			}
		}

		return returnVal;
	}

	/// <summary>
	/// Get all the IDs of all Neemotions in the Game
	/// </summary>
	/// <returns>An array of type int that holds all the ids</returns>
	public int[] GetAllNemotionIDs()
	{
		int[] returnarray = new int[neemotions.Length];
		int i = 0;
		foreach ( Neemotion n in neemotions )
		{
			returnarray[i] = n.GetID();
			i++;
		}

		return returnarray;
	}
}
