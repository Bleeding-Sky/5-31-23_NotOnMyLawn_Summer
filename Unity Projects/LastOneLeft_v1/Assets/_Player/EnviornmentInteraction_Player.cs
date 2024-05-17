using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviornmentInteraction_Player : MonoBehaviour
{
    public GameObject player;
    public GameObject playerBody;
    public GameObject handInv;
    public GameObject Inventory;
    public GameObject aiming;
    public States_Player playerStates;

    private void Start()
    {
        playerStates.lookingThroughWindow = false;
    }
    /// <summary>
    /// Interacts with the enviornment depending on which enviornmental item it is
    /// </summary>
    /// <param name="Enviornment"></param>
    public void Interact(GameObject Enviornment, int actionType)
    {
        InteractionIdentification_Item enviornmentType = Enviornment.GetComponent<InteractionIdentification_Item>();
        if (enviornmentType.isWindow && actionType == 0)
        {
            WindowInteraction(Enviornment);
        }
        else if(enviornmentType.isWindow && actionType != 0)
        {
            WindowRebuildInteraction(Enviornment, actionType);
        }
        else if (enviornmentType.isBoardPile && actionType != 0)
        {
            BoardPileInteraction(Enviornment, actionType);
        }
        else if(enviornmentType.isBackgroundDoor && actionType == 0)
        {
            BackgroundDoorInteraction(Enviornment);
        }
    }

    /// <summary>
    /// Interaction for the Board Pile 
    /// </summary>
    /// <param name="Enviornment"></param>
    private void BoardPileInteraction(GameObject Enviornment, int actionType)
    {
        BoardPile_Environment boardPile = Enviornment.GetComponent<BoardPile_Environment>();

        //If Presses W then Picks up board
        if (actionType == 1)
        {
            boardPile.PickUpBoard();
        }
        else if (actionType == 2)
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
        WindowInteraction_Enviornment window = Enviornment.GetComponent<WindowInteraction_Enviornment>();
        WindowBarricade_Enviornment windowRebuild = Enviornment.GetComponent<WindowBarricade_Enviornment>();
        HandInventory_Player objectInHands = handInv.GetComponent<HandInventory_Player>();
        /*
         * Two different interactions based on what key the 
         * player presses. Either the player looks through the window 
         * or the player rebuilds the window.
         */

        //Look through window interaction
        if (!window.Interacting)
        {
            DisablePlayer(objectInHands.objectInHand);
            window.switchToOutside();
            window.Interacting = true;
            playerStates.lookingThroughWindow = true;

        }
        else if (window.Interacting)
        {
            EnablePlayer(objectInHands.objectInHand);
            window.switchToInside();
            window.Interacting = false;
            playerStates.lookingThroughWindow = false;

        }

    }

    private void WindowRebuildInteraction(GameObject Enviornment, int actionType)
    {
        WindowInteraction_Enviornment window = Enviornment.GetComponent<WindowInteraction_Enviornment>();
        WindowBarricade_Enviornment windowRebuild = Enviornment.GetComponent<WindowBarricade_Enviornment>();
        HandInventory_Player objectInHands = handInv.GetComponent<HandInventory_Player>();


        //Rebuild window interaction
        if (windowRebuild.boardsOnWindow != 3 && windowRebuild.boardsInInventory && !window.Interacting && actionType == 1)
        {
            windowRebuild.AddBoard();
        }
        else if(actionType == 2)
        {
            windowRebuild.timer = 0;
        }
    }

    /// <summary>
    /// sorts which door the player will exit out of
    /// </summary>
    /// <param name="Environment"></param>
    private void BackgroundDoorInteraction(GameObject Environment)
    {
        BackgroundDoor_Environment door = Environment.GetComponent<BackgroundDoor_Environment>();

        //Depending on which door the player is at in the set it will teleport the player there
        if(door.currentDoor == door.door1)
        {
            player.transform.position = door.door2.transform.position;
            Debug.Log("door 1");
        }
        else if(door.currentDoor == door.door2)
        {
            player.transform.position = door.door1.transform.position;
            Debug.Log("door 2");
        }
    }

    /// <summary>
    /// Disables the players object and abilities 
    /// </summary>
    /// <param name="objectInHand"></param>
    private void DisablePlayer(GameObject objectInHand)
    {
        Movement_Player playerMovement = player.GetComponent<Movement_Player>();

        //Disabling based on if there is no object in hand
        if (objectInHand == null)
        {
            playerMovement.enabled = false;
            //handInv.SetActive(false);
            playerBody.SetActive(false);
            //Inventory.SetActive(false);
        }
        //Disabling based on if there is an object in the players hand
        else
        {
            playerMovement.enabled = false;
            //handInv.SetActive(false);
            CheckForGunInHandInteractionOn(objectInHand);
            playerBody.SetActive(false);
            //objectInHand.SetActive(false);
            //Inventory.SetActive(false);
        }
    }

    /// <summary>
    /// Renables the player's game object and abilities
    /// </summary>
    /// <param name="objectInHand"></param>
    private void EnablePlayer(GameObject objectInHand)
    {
        Movement_Player playerMovement = player.GetComponent<Movement_Player>();

        //Enabling based on if there is no object in hand
        if (objectInHand == null)
        {
            playerMovement.enabled = true;
            //handInv.SetActive(true);
            playerBody.SetActive(true);
            //Inventory.SetActive(true);
        }
        //Enabling based on if there is an object in the players hand
        else
        {
            playerMovement.enabled = true;
            //handInv.SetActive(true);
            CheckForGunInHandInteractionOn(objectInHand);
            playerBody.SetActive(true);
            //objectInHand.SetActive(true);
            //Inventory.SetActive(true);
        }
    }

    /// <summary>
    /// NOT used currently but was used for the Gun Aiming object.
    /// Kept in case we need to reuse it again.
    /// </summary>
    /// <param name="objectInHand"></param>
    private void CheckForGunInHandInteractionOn(GameObject objectInHand)
    {
        InteractionIdentification_Item gameObjectInHand = objectInHand.GetComponent<InteractionIdentification_Item>();
        if (gameObjectInHand.isGun && gameObjectInHand.isItem)
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
        InteractionIdentification_Item gameObjectInHand = objectInHand.GetComponent<InteractionIdentification_Item>();
        if (gameObjectInHand.isGun && gameObjectInHand.isItem)
        {

        }
        else
        {
            Debug.Log("No object in hand");
        }
    }
}
