using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviorment_Interaction : MonoBehaviour
{
    public GameObject playerBody;
    
    public void Interact(GameObject Enviornment)
    {
        Interaction_Identification enviornmentType = Enviornment.GetComponent<Interaction_Identification>();
        if(enviornmentType.isWindow)
        {
            WindowInteraction(Enviornment);
        }
    }

    private void WindowInteraction(GameObject Enviornment)
    {
        Window_Interact window = Enviornment.GetComponent<Window_Interact>();

        if (Input.GetKeyDown(KeyCode.E) && !window.Interacting)
        {
            window.switchToOutside();
            window.Interacting = true;
            playerBody.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.E) && window.Interacting)
        {
            window.switchToInside();
            window.Interacting = false;
            playerBody.SetActive(true);
        }
    }
}
