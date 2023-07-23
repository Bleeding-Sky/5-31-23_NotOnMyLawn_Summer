using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviorment_Interaction : MonoBehaviour
{
    public GameObject playerBody;
    public GameObject handInv;
    public GameObject Inventory;
    public GameObject aiming;

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
        Player_InHand objectInHands = handInv.GetComponent<Player_InHand>();

        if (Input.GetKeyDown(KeyCode.E) && !window.Interacting)
        {
            DisablePlayer(objectInHands.objectInHand);
            window.switchToOutside();
            window.Interacting = true;
            
        }
        else if(Input.GetKeyDown(KeyCode.E) && window.Interacting)
        {
            EnablePlayer(objectInHands.objectInHand);
            window.switchToInside();
            window.Interacting = false;
            
        }
    }

    private void DisablePlayer(GameObject objectInHand)
    {
        if(objectInHand == null)
        {
            handInv.SetActive(false);
            playerBody.SetActive(false);
            Inventory.SetActive(false);
        }
        else
        {
            handInv.SetActive(false);
            CheckForGunInHandInteractionOn(objectInHand);
            playerBody.SetActive(false);
            objectInHand.SetActive(false);
            Inventory.SetActive(false);
        }
    }
    private void EnablePlayer(GameObject objectInHand)
    {
        if (objectInHand == null)
        {
            handInv.SetActive(true);
            playerBody.SetActive(true);
            Inventory.SetActive(true);
        }
        else
        {
            handInv.SetActive(true);
            CheckForGunInHandInteractionOn(objectInHand);
            playerBody.SetActive(true);
            objectInHand.SetActive(true);
            Inventory.SetActive(true);
        }
    }
    private void CheckForGunInHandInteractionOn(GameObject objectInHand)
    {
        Interaction_Identification gameObjectInHand = objectInHand.GetComponent<Interaction_Identification>();
        if(gameObjectInHand.isGun && gameObjectInHand.isItem)
        {
            aiming.SetActive(false);
            Debug.Log("object in hand");
        }
        else
        {
            Debug.Log("No object in hand");
        }
    }

    private void CheckForGunInHandInteractionOff(GameObject objectInHand)
    {
        Interaction_Identification gameObjectInHand = objectInHand.GetComponent<Interaction_Identification>();
        if (gameObjectInHand.isGun && gameObjectInHand.isItem)
        {
            aiming.SetActive(true);
        }
        else
        {
            Debug.Log("No object in hand");
        }
    }
}
