using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YBC.Neemotix
{
    class Interactable : MonoBehaviour
    {
        public Effect[] effects;

        private string myInteractableObjectName;

        private ChangeManager.PushEffectToQueue pushEffects;
        public ChangeManager.PushEffectToQueue PushEffectsMethod {set => pushEffects = value; }

        public void Awake()
        {
            myInteractableObjectName = transform.name;
        }
        public void OnInteract()
        {
            foreach ( Effect e in effects )
            {
                pushEffects(e);
            }
            Debug.Log("You have just intercted, doesn't it feel good!");
        }


        public void OnInteractionStart()
        {
            foreach ( Effect e in effects )
            {
                pushEffects(e);
            }
            Debug.Log("You started interacting with " + myInteractableObjectName + ". Enjoy!");
        }


        public void OnInteractionEnd()
        {
            foreach ( Effect e in effects )
            {
                if( e.durationInHours == 0)
                {
                    e.Revoke();
                }
            }
            Debug.Log("You finished interacting with " + myInteractableObjectName  + ". Was it good?");
        }

        public string GetInteractableObjectName()
        {
            return myInteractableObjectName;
        }
    }
}
