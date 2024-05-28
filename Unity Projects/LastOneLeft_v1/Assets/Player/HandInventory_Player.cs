using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInventory_Player : MonoBehaviour
{
    public GameObject objectInHand;
    public GameObject Inventory;
    public bool itemInHand;

    public GameObject AimingArea;

    public Animator gunAnimations;


    /*
     * This Script is heavily relied upon by the Inventory Script
     * Alot of these functions are just meant to be used by it so
     * refer to it if something ggoes wrong
     */

    // Update is called once per frame
    void Update()
    {
        //Used for when the player wants to drop an item from their hand
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItemInHand();
        }

        AimingAnimationHandler();
    }


    public void AimingAnimationHandler()
    {
        if(itemInHand)
        {
            InteractionIdentification_Item objectInfo = objectInHand.GetComponent<InteractionIdentification_Item>();

            if(objectInfo.isGun)
            {
                gunAnimations.SetBool("isAiming", true);
            }
            else
            {
                gunAnimations.SetBool("isAiming", false);
            }
        }
        else
        {
            gunAnimations.SetBool("isAiming", false);
        }
    }
    /// <summary>
    /// Drops the item currently in the player's hand
    /// </summary>
    public void DropItemInHand()
    {

    }

    /// <summary>
    /// Places the object in the player's hand when chosen through the number keys
    /// The number keys functionality is in the Inventory Script
    /// </summary>
    /// <param name="iteminHand"></param>
    public void PlaceObjectInHand(GameObject iteminHand)
    {
        //The item that is being chosen to be in the hand is set as the main object
        objectInHand = iteminHand;

        //Returns message if there is no item
        if (objectInHand == null)
        {
            Debug.Log("No Item in hand");
        }
        //If there is an object it becomes the object in the player's
        else
        {
            objectInHand.SetActive(true);
            objectInHand.transform.parent = gameObject.transform;
            itemInHand = true;
            DetermineIfItemInHand();
        }
    }

    /// <summary>
    /// If there is an item in the player's hand then the 
    /// object currently in the player's hand is swapped
    /// </summary>
    /// <param name="SwappedItem"></param>
    public void SwapItemInHand(GameObject SwappedItem)
    {
        //The swapped item is set as the current object in the players hand
        GameObject holdItem = objectInHand;
        objectInHand = SwappedItem;
        objectInHand.SetActive(true);

        //Object's parent is changed from the inventory to the player's hand to visualize
        objectInHand.transform.parent = gameObject.transform;

        //The item that is currently being swapped out is put back into the inventory
        holdItem.transform.parent = Inventory.transform;
        holdItem.SetActive(false);
        itemInHand = true;
        DetermineIfItemInHand();


    }

    /// <summary>
    /// It unequips an item from the the players hand and places it back into the inventory
    /// </summary>
    public void UnequipItem()
    {
        //parents the object to the inventory
        objectInHand.transform.parent = Inventory.transform;
        objectInHand.SetActive(false);

        //Empties the hands object slot 
        objectInHand = null;
        itemInHand = false;
    }

    public void RemoveItemFromHand()
    {
        objectInHand.transform.SetAsLastSibling();
        objectInHand = null;
    }

    public void DetermineIfItemInHand()
    {
        if (itemInHand)
        {
            InteractionIdentification_Item equippedItem = objectInHand.GetComponent<InteractionIdentification_Item>();
            if (equippedItem.isGun)
            {
                /*
                 * The ienumerator from the gun script would always get stuck if you switched
                 * weapons in between shots so the cool down being false ensures the 
                 * gun is shootable next time that the player equips it if there are bullets
                 */

                GunInformation_Item gunInfo = objectInHand.GetComponent<GunInformation_Item>();
                gunInfo.coolingDown = false;
                //AimingArea.SetActive(true);
            }
            else if (!equippedItem.isGun)
            {
                //AimingArea.SetActive(false);
            }
        }
        else if (!itemInHand)
        {
            //AimingArea.SetActive(false);
        }
    }
}
