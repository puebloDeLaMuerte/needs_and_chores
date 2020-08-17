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
		private ChangeManager.DebugRegisterEffects registerEffectforDebugMethod;

		/// <summary>
		/// Initiates the 'knownInteractables' Array.
		/// </summary>
		void Start()
		{
			knownInteractables = GetComponentsInChildren<Interactable>();

			foreach ( Interactable thisInteractable in knownInteractables )
			{
				thisInteractable.PushEffectsMethod = pushMethod;
				foreach ( Effect effect in thisInteractable.effects )
				{
					registerEffectforDebugMethod( effect );
				}
				//Debug.Log("InteractablesManager knows: " + thisInteractable.GetInteractableObjectName());
			}
		}


		/// <summary>
		/// The ChangeManager must tell it's PushEffectsToQueue Method through this Method in OnAwake().
		/// This InteractablesManager then sets it to all the Interactables in it's Start() Method.
		/// The Method to list the Effects for Debuging is also set here.
		/// </summary>
		/// <param name="pushMethod"></param>
		public void setCallbackMethods(ChangeManager.PushEffectToQueue pushMethod, ChangeManager.DebugRegisterEffects listMethod )
		{
			this.pushMethod = pushMethod;
			this.registerEffectforDebugMethod = listMethod;
		}


	} 
}
