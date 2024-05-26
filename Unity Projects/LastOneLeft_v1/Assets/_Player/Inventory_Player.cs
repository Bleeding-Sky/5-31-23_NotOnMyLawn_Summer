using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : MonoBehaviour
{
    [Header("Inventory")]
    public List<GameObject> item;

    public int largeAmmo;
    public int mediumAmmo;
    public int smallAmmo;

    [Header("CONFIG")]
    public int maxInventorySize;
    private int currentMaxInventorySize;

    [Header("DEBUG")]
    public Interaction_Player ItemInteractionScript;
    public ItemInteraction_Player itemAssignment;
    public GameObject handInv;
    public int numberPressed;
    public int lastInventorySlotChosen;

    /*
    * The item list essentially acts as the abstract version of 
    * the inventory. The order and way that the inventory looks in 
    * this list will be how it is in the visual one once that gets
    * integrated
    */

    // Start is called before the first frame update
    void Start()
    {
        //Sets what the current MaxInventory size is so that it can be changed later with upgrades
        currentMaxInventorySize = maxInventorySize;

        largeAmmo = 0;
        mediumAmmo = 0;
        smallAmmo = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //gets what inventory slot is chosen and runs it through a function to select the item
        numberPressed = GetPressedNumber();
        if (numberPressed > 0)
        {
            ChosenInventorySlot(numberPressed);
        }
    }

    /// <summary>
    /// Stores the item in the item list
    /// </summary>
    /// <param name="InteractedItem"></param>
    public void StoreItems(GameObject InteractedItem)
    {
        InteractionIdentification_Item itemType = InteractedItem.GetComponent<InteractionIdentification_Item>();
        
        //Goes through each item in the list and looks for the nearest empty slot
        for (int i = 0; i < maxInventorySize; i++)
        {
            //if a slot if empty it fills it with the interacted item
            if (item[i] == null && !itemType.isBullet)
            {
                //Sets the item in the list and determines what type of item it is giving it all the necessary information and setting the object in as the child of the inventory game object to visualize it better
                //TLDR sets it in the inventory and inherits information from the player
                item[i] = InteractedItem;
                itemAssignment.DetermineItemType(InteractedItem);
                InteractedItem.transform.parent = gameObject.transform;
                InteractedItem.SetActive(false);
                maxInventorySize = 0;   
            }
            else if(itemType.isBullet)
            {
                AmmoDrop_Item ammoType = InteractedItem.GetComponent<AmmoDrop_Item>();
                
                if(ammoType.BulletType == AmmoDrop_Item.BulletTypes.Large)
                {
                    largeAmmo += ammoType.bulletCount;
                }
                else if(ammoType.BulletType == AmmoDrop_Item.BulletTypes.Medium)
                {
                    mediumAmmo += ammoType.bulletCount;
                }
                else if(ammoType.BulletType == AmmoDrop_Item.BulletTypes.Small)
                {
                    smallAmmo += ammoType.bulletCount;
                }

                ammoType.DestroyPack();
                break;
            }

        }
        maxInventorySize = currentMaxInventorySize;
    }

    /// <summary>
    /// Gets the players input from the number keys and uses that to choose an item in the inventory
    /// </summary>
    /// <param name="numberKeyPressed"></param>
    public void ChosenInventorySlot(int numberKeyPressed)
    {
        int inventorySlot = numberKeyPressed - 1;
        HandInventory_Player putItemInHand = handInv.GetComponent<HandInventory_Player>();
        //if there is no item in hand it'll place the object in the player's hand
        if (!putItemInHand.itemInHand)
        {
            putItemInHand.PlaceObjectInHand(item[inventorySlot]);
        }
        //if there is an object in the players hand it swaps it out for the chosen inventory slot
        else if (putItemInHand.itemInHand && lastInventorySlotChosen != inventorySlot)
        {
            //if they want to swap with an empty slot it unequips the object
            //otherwise it swaps the items
            if (item[inventorySlot] == null)
            {
                Debug.Log("No item to swap");
                putItemInHand.UnequipItem();
            }
            else
            {
                putItemInHand.SwapItemInHand(item[inventorySlot]);
            }
        }
        //if  the player chooses the same inventory slot that is already selected it unequips the item
        else if (putItemInHand.itemInHand && lastInventorySlotChosen == inventorySlot)
        {
            putItemInHand.UnequipItem();
        }
        lastInventorySlotChosen = inventorySlot;
    }

    /// <summary>
    /// function that gets the inputted number and returns it 
    /// to the numberPressed variable to select the inventory slot
    /// </summary>
    /// <returns></returns>
    public int GetPressedNumber()
    {
        for (int number = 0; number <= 9; number++)
        {
            if (Input.GetKeyDown(number.ToString()))
                return number;
        }
        return -1;
    }
}
