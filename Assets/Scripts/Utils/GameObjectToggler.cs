using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectToggler : MonoBehaviour
{
    public string hotKey;
    public GameObject toggleObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if ( Input.GetKeyDown( hotKey ) )
        {
            toggleObject.SetActive( !toggleObject.activeInHierarchy );
        }
    }
}
