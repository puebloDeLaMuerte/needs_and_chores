using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YBC.Neemotix
{
    public class NeemoticInteractable : Object
    {
        public Effect[] effects;

        public void OnInteract()
        {
            Debug.Log("You have just intercted, doesn't it feel good!");
        }


        public void OnInteractionStart()
        {
            Debug.Log("You started interacting. Enjoy!");
        }


        public void OnInteractionEnd()
        {
            Debug.Log("You finished interacting. Was it good?");
        }
    }
}
