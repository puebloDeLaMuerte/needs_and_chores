using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YBC.Neemotix;

public class NeemotixValuesAdapter : MonoBehaviour
{

	private Neemotion[] neemotions;

	public void Awake()
	{
		PopulatetheArrays();
	}

	/// <summary>
	/// Populates the adapters neemotions[] with the neemotion that the changeManager knows. Emotions first, Needs second (in case you want to know for performance reasons)
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
	/// Looks up the Neemotion by the given String (beware! String-compare! bad performance!)
	/// </summary>
	/// <param name="neemotionName">The Name of the Neemotion in Question</param>
	/// <returns>the Neemotions current value. zero if none found.</returns>
	public float GetNeemotionValueByName(String neemotionName)
	{
		float returnVal = 0;

		foreach ( Neemotion neemotion in neemotions )
		{
			if( neemotion.NeemotionName.Equals(neemotionName) )
			{
				returnVal = neemotion.currentValue;
			}
		}

		return returnVal;
	}
}
