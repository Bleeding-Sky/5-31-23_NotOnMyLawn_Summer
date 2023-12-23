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

    /// <summary>
    /// Interacts with the enviornment depending on which enviornmental item it is
    /// </summary>
    /// <param name="Enviornment"></param>
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

    /// <summary>
    /// Interaction for the Board Pile 
    /// </summary>
    /// <param name="Enviornment"></param>
    private void BoardPileInteraction(GameObject Enviornment)
    {
        BoardPile_Script boardPile = Enviornment.GetComponent<BoardPile_Script>();

        //If Presses W then Picks up board
        if(Input.GetKey(KeyCode.W))
        {
            boardPile.PickUpBoard();
        }
        else if(Input.GetKeyUp(KeyCode.W))
        //resets if the key is let go
        {
            boardPile.ResetBoardPickUp();
        }
    }

    /// <summary>
    /// Interaction if the enviornment is a window object
    /// </summary>
    /// <param name="Enviornment"></param>
    private void WindowInteraction(GameObject Enviornment)
    {
        Window_Interact window = Enviornment.GetComponent<Window_Interact>();
        Window_Barricade windowRebuild = Enviornment.GetComponent<Window_Barricade>();
        Player_InHand objectInHands = handInv.GetComponent<Player_InHand>();
        /*
         * Two different interactions based on what key the 
         * player presses. Either the player looks through the window 
         * or the player rebuilds the window.
         */

        //Look through window interaction
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

        //Rebuild window interaction
        if (Input.GetKey(KeyCode.W) && windowRebuild.boardsOnWindow != 3 && windowRebuild.boardsInInventory && !window.Interacting)
        {
            windowRebuild.AddBoard();
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            windowRebuild.timer = 0;
        }
    }

    /// <summary>
    /// Disables the players object and abilities 
    /// </summary>
    /// <param name="objectInHand"></param>
    private void DisablePlayer(GameObject objectInHand)
    {
        Player_InstantLRMovement playerMovement = player.GetComponent<Player_InstantLRMovement>();

        //Disabling based on if there is no object in hand
        if(objectInHand == null)
        {
            playerMovement.enabled = false;
            handInv.SetActive(false);
            playerBody.SetActive(false);
            Inventory.SetActive(false);
        }
        //Disabling based on if there is an object in the players hand
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

    /// <summary>
    /// Renables the player's game object and abilities
    /// </summary>
    /// <param name="objectInHand"></param>
    private void EnablePlayer(GameObject objectInHand)
    {
        Player_InstantLRMovement playerMovement = player.GetComponent<Player_InstantLRMovement>();

        //Enabling based on if there is no object in hand
        if (objectInHand == null)
        {
            playerMovement.enabled = true;
            handInv.SetActive(true);
            playerBody.SetActive(true);
            Inventory.SetActive(true);
        }
        //Enabling based on if there is an object in the players hand
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

    /// <summary>
    /// NOT used currently but was used for the Gun Aiming object.
    /// Kept in case we need to reuse it again.
    /// </summary>
    /// <param name="objectInHand"></param>
    private void CheckForGunInHandInteractionOn(GameObject objectInHand)
    {
        Interaction_Identification gameObjectInHand = objectInHand.GetComponent<Interaction_Identification>();
        if(gameObjectInHand.isGun && gameObjectInHand.isItem)
        {
            
            Debug.Log("object in hand");
        }
        else
        {
            Debug.Log("No object in hand");
        }
    }

    /// <summary>
    /// NOT used currently but was used for the Gun Aiming object.
    /// Kept in case we need to reuse it again.
    /// </summary>
    /// <param name="objectInHand"></param>
    private void CheckForGunInHandInteractionOff(GameObject objectInHand)
    {
        Interaction_Identification gameObjectInHand = objectInHand.GetComponent<Interaction_Identification>();
        if (gameObjectInHand.isGun && gameObjectInHand.isItem)
        {
            
        }
        else
        {
            Debug.Log("No object in hand");
        }
    }
}
