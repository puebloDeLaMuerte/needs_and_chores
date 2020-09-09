using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YBC.Neemotix
{
	class Interactable : MonoBehaviour
	{
		public Effect[] effects = null;

		private string myInteractableObjectName;
		private Transform myInteractableCollectionTransform;

		private ChangeManager.PushEffectToQueue pushEffects;
		public ChangeManager.PushEffectToQueue PushEffectsMethod {set => pushEffects = value; }

		public void Awake()
		{
			myInteractableObjectName = transform.name;
			myInteractableCollectionTransform = transform.parent.transform;

			SetIssuerToEffects();
		}


		private void SetIssuerToEffects()
		{
			foreach ( Effect effect in effects )
			{
				effect.SetIssuerName(myInteractableObjectName);
			}
		}


		public void OnInteract()
		{
			foreach ( Effect e in effects )
			{
				pushEffects(e);
			}

			Debug.Log("Interaction: " + myInteractableObjectName);
		}


		public void OnInteractionStart()
		{
			foreach ( Effect e in effects )
			{
				pushEffects(e);
			}
			Debug.Log("Interaction Start: " + myInteractableObjectName);
		}


		public void OnInteractionEnd()
		{
			foreach ( Effect e in effects )
			{
				e.Revoke();
			}
			Debug.Log("Interaction End: " + myInteractableObjectName);

		}


		public string GetInteractableObjectName()
		{
			return myInteractableObjectName;
		}


		public void RebaseGameobject()
		{
			transform.SetParent(myInteractableCollectionTransform);
		}
	}
}
