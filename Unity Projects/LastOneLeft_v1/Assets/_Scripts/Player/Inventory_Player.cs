using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory_Player : MonoBehaviour
{
    [Header("Inventory")]
    public List<GameObject> item;

    public int largeAmmo;
    public int mediumAmmo;
    public int smallAmmo;

    [Header("CONFIG")]
    public int maxInventorySize;
    [SerializeField]private int currentMaxInventorySize;

    [Header("DEBUG")]
    public Interaction_Player ItemInteractionScript;
    public ItemInteraction_Player itemAssignment;
    public GameObject handInv;
    public GameObject Backpack;
    public GameObject RadialMenu;
    public int numberPressed;
    public int lastInventorySlotChosen;
    public bool invActive;
    public bool equipped;
    public States_Player playerStates;
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

        Backpack_UI backpackInfo = Backpack.GetComponent<Backpack_UI>();
        backpackInfo.gunInventory = item;
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
            equipped = true;
            Debug.Log("Item to equip");
        }
        //if there is an object in the players hand it swaps it out for the chosen inventory slot
        else if (putItemInHand.itemInHand && lastInventorySlotChosen != inventorySlot)
        {
            //if they want to swap with an empty slot it unequips the object
            //otherwise it swaps the items
            if (item[inventorySlot] == null)
            {
                Debug.Log("No item to swap");
            }
            else
            {
                putItemInHand.SwapItemInHand(item[inventorySlot]);
                equipped = true;
                Debug.Log("swap");
            }
        }

        lastInventorySlotChosen = inventorySlot;
    }

    
    public void HolsterDrawToggler()
    {
        if(equipped)
        {
            playerStates.gunIsDrawn = true;
        }
        else if(!equipped)
        {
            playerStates.gunIsDrawn = false;
        }
    }

    /// <summary>
    /// Toggles the Radial menu for the player
    /// </summary>
    /// <param name="actionContext"></param>
    public void ToggleInventory(InputAction.CallbackContext actionContext)
    {
        //Determines whether the radial menu is already active or not and then
        //Decides how to interact with it otherwise
        if(RadialMenu.activeInHierarchy == true)
        {
            invActive = true;
        }
        else
        {
            invActive = false;
        }

        //Sets the Backpack and radial menu to their appropriate states
        if (!invActive)
        {
            RadialMenu.SetActive(true);
            Backpack.SetActive(false);
            invActive = true;
        }
        else
        {
            RadialMenu.SetActive(false);
            Backpack.SetActive(false);
            invActive = false;
        }
        
    }

    public void EquipItem(InputAction.CallbackContext actionContext)
    {
        if (actionContext.started && !equipped && (item[0] != null || item[1] != null))
        {
            if(item[0] != null)
            {
                ChosenInventorySlot(1);
            }
            else if(item[1] != null)
            {
                ChosenInventorySlot(2);
            }
            
        }
        else if(actionContext.started && equipped)
        {
            HandInventory_Player putItemInHand = handInv.GetComponent<HandInventory_Player>();
            putItemInHand.UnequipItem();
            equipped = false;
        }
        HolsterDrawToggler();
    }

    public void ChooseFirstSlot(InputAction.CallbackContext actionContext)
    {
        if (actionContext.started && equipped && item[0] != null)
        {
            ChosenInventorySlot(1);
        }
        HolsterDrawToggler();
    }

    public void ChooseSecondSlot(InputAction.CallbackContext actionContext)
    {
        if (actionContext.started && equipped && item[1] != null)
        {
            ChosenInventorySlot(2);
        }
        HolsterDrawToggler();
    }

}
