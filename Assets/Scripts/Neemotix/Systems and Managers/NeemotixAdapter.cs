using System;
using UnityEngine;
using YBC;
using YBC.Neemotix;

namespace YBC.Audix.InnerVoice
{
	public class NeemotixAdapter : MonoBehaviour, INeemotixAdapter, IInnerVoiceDataAdapter
	{

		///TODO: Check member-visibility to make this DataAdapter Class the propper API for Neemotix

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

			Array.Copy( emotions, neemotions, emotions.Length );
			Array.Copy( needs, 0, neemotions, emotions.Length, needs.Length );
		}



		/// <summary>
		/// Looks up the Neemotions current Status by the given String (beware! String-compare! bad performance!)
		/// </summary>
		/// <param name="neemotionName">The Name of the Neemotion in Question</param>
		/// <returns>a touple containing the Neemotions current value and status. (0f,undefined) if no Neemotion found.</returns>
		public (float, NeemotionStatus) GetNeemotionStateByName( String neemotionName )
		{
			(float, NeemotionStatus) returnVal = (0, NeemotionStatus.Undefined);

			foreach ( Neemotion neemotion in neemotions )
			{
				if ( neemotion.neemotionName.Equals( neemotionName ) )
				{
					returnVal = (neemotion.currentValue, neemotion.Status);
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
					return returnVal = (neemotion.currentValue, neemotion.Status);

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

		public string getStringForSelectorID( int selectorID, int selectorDepth )
		{
			throw new NotImplementedException();
		}

		public float getFloatForSelectorID( int selectorID, int selectorDepth )
		{
			throw new NotImplementedException();
		}

		public int getIntForSelectorID( int selectorID, int selectorDepth )
		{
			NeemotionStatus status = GetNeemotionStateByID( selectorID ).Item2;

			switch ( status )
			{
				case NeemotionStatus.Undefined:
					return -1;
				case NeemotionStatus.vlow: return 0;
				case NeemotionStatus.low:  return 1;
				case NeemotionStatus.med:  return 2;
				case NeemotionStatus.high: return 3;
				default:
					return -1;
			}
		}

		public (int, string)[] getAllSelectorIDs()
		{
			(int, string)[] returnarray = new (int, string)[neemotions.Length];
			int i = 0;
			foreach ( Neemotion n in neemotions )
			{
				returnarray[i] = (n.GetID(), n.neemotionName);
				i++;
			}

			return returnarray;
		}
	}

}
