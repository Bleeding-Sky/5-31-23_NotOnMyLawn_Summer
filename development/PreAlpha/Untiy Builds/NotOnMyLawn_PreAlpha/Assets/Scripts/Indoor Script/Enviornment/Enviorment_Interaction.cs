using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviorment_Interaction : MonoBehaviour
{
    
    public void Interact(GameObject Enviornment)
    {
        Interaction_Identification enviornmentType = Enviornment.GetComponent<Interaction_Identification>();
        if(enviornmentType.isWindow)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Using Window");
            }
        }
    }
}
