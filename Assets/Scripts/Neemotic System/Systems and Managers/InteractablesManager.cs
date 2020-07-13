using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace YBC.Neemotix
{
	class InteractablesManager : MonoBehaviour
	{

		private Interactable[] knownInteractables;
		private ChangeManager.PushEffectToQueue pushMethod;

		/// <summary>
		/// Initiates the 'knownInteractables' Array.
		/// </summary>
		void Start()
		{
			knownInteractables = GetComponentsInChildren<Interactable>();

			foreach ( Interactable item in knownInteractables )
			{
				item.PushEffectsMethod = pushMethod;
				Debug.Log("InteractablesManager knows: " + item.GetInteractableObjectName());
			}
		}

		public void setPushToEffectsQueueMethod(ChangeManager.PushEffectToQueue pushMethod)
		{
			this.pushMethod = pushMethod;
		}


	} 
}
