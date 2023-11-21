using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviorment_Interaction : MonoBehaviour
{
    public GameObject player;
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
        else if(enviornmentType.isBoardPile)
        {
            BoardPileInteraction(Enviornment);
        }
    }

    private void BoardPileInteraction(GameObject Enviornment)
    {
        BoardPile_Script boardPile = Enviornment.GetComponent<BoardPile_Script>();
        if(Input.GetKey(KeyCode.W))
        {
            boardPile.PickUpBoard();
        }
        else if(Input.GetKeyUp(KeyCode.W))
        {
            boardPile.ResetBoardPickUp();
        }
    }

    private void WindowInteraction(GameObject Enviornment)
    {
        Window_Interact window = Enviornment.GetComponent<Window_Interact>();
        Window_Barricade windowRebuild = Enviornment.GetComponent<Window_Barricade>();
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
        if (Input.GetKey(KeyCode.W) && windowRebuild.boardsOnWindow != 3 && windowRebuild.boardsInInventory && !window.Interacting)
        {
            windowRebuild.AddBoard();
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            windowRebuild.timer = 0;
        }
    }

    private void DisablePlayer(GameObject objectInHand)
    {
        Player_InstantLRMovement playerMovement = player.GetComponent<Player_InstantLRMovement>();
        if(objectInHand == null)
        {
            playerMovement.enabled = false;
            handInv.SetActive(false);
            playerBody.SetActive(false);
            Inventory.SetActive(false);
        }
        else
        {
            playerMovement.enabled = false;
            handInv.SetActive(false);
            CheckForGunInHandInteractionOn(objectInHand);
            playerBody.SetActive(false);
            objectInHand.SetActive(false);
            Inventory.SetActive(false);
        }
    }
    private void EnablePlayer(GameObject objectInHand)
    {
        Player_InstantLRMovement playerMovement = player.GetComponent<Player_InstantLRMovement>();
        if (objectInHand == null)
        {
            playerMovement.enabled = true;
            handInv.SetActive(true);
            playerBody.SetActive(true);
            Inventory.SetActive(true);
        }
        else
        {
            playerMovement.enabled = true;
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
