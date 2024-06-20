using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Backpack_UI : MonoBehaviour
{
    public GameObject player;
    public Inventory_Player inventory;
    public List<GameObject> gunInventory;

    public List<ItemSlot_UI> slots;
    public int quickAccessAmount;
    public int smallPocketAmount;
    public int largePocketAmount;

    public GameObject background;

    public GameObject radialSlots;
    public GameObject inventorySlots;

    public BackpackSelect_UI backpackQuickMenu;
    public bool invActive;

    public GameObject draggedSlot;
    public GameObject overSlot;
    public bool holdingDown;
    public SelectedItem_UI selectedSlot;
    private void Start()
    {
        backpackQuickMenu.player = player;
        backpackQuickMenu.AddSlots();
        AddSlots();
        
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].slotID = i;
            slots[i].isEmpty = true;
            slots[i].Backpack = this;
        }

        gameObject.SetActive(false);
        background.SetActive(false);
    }

    #region Slot Interaction

    /// <summary>
    /// Passes the item information to the item slots
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(GameObject item)
    {
        //variable that will be used to determine if a spot already associated with 
        //the item is found
        bool spotFound = false;
        Data_Item itemData = item.GetComponent<Data_Item>();

        //Goes through all the slots to determine whether a slot is already occupied by the
        //object that is trying to be added into the inventory
        for (int i = 0; i < slots.Count; i++)
        {
            //Also makes sure that if there is a match with the item and slot
            //that it wont trigger the spotFound boolean if the slot is full
            if (itemData.itemName == slots[i].itemName && !slots[i].isFull)
            {
                spotFound = true;
                Debug.Log(slots[i].isFull);
                slots[i].AddItem(item);
                break;
            }
        }
        
        //If an item slot associated with the item isnt found then the nearest
        //empty item slot will be found
        if (!spotFound)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].isEmpty)
                {
                    slots[i].AddItem(item);
                    break;
                }
            }
        }

        //updates the radial menu
        backpackQuickMenu.CopyRadialSlotInfo();

    }
    #endregion
    #region Slot Management
    /// <summary>
    /// Populates the slots List with the radial and the inventory slots
    /// Only runs once at the beggining of the game
    /// </summary>
    public void AddSlots()
    {
        //Get the radial slots from the game object parent
        foreach(Transform child in radialSlots.transform)
        {
            ItemSlot_UI childInfo = child.GetComponent<ItemSlot_UI>();
            childInfo.isRadialSlot = false;
            childInfo.player = player;
            childInfo.Backpack = this;
            slots.Add(childInfo);
        }

        //Get the inventory slots from the parent object
        foreach (Transform child in inventorySlots.transform)
        {
            ItemSlot_UI childInfo = child.GetComponent<ItemSlot_UI>();
            childInfo.player = player;
            childInfo.Backpack = this;
            slots.Add(childInfo);
        }
    }

    /// <summary>
    /// copy one item slot onto another's component
    /// </summary>
    /// <param name="itemSlot"></param>
    /// <param name="radialSlot"></param>
    public void CopySlots(ItemSlot_UI itemSlot, ItemSlot_UI radialSlot, bool setIcon)
    {
        Debug.Log("Copying");
        radialSlot.itemName = itemSlot.itemName;
        radialSlot.maxUses = itemSlot.maxUses;
        radialSlot.iconSprite = itemSlot.iconSprite;
        radialSlot.maxAmount = itemSlot.maxAmount;
        radialSlot.itemSO = itemSlot.itemSO;
        if (setIcon == true)
        {
            radialSlot.SetIcon(radialSlot.iconSprite);
        }
        radialSlot.currentAmount = itemSlot.currentAmount;
        radialSlot.isEmpty = itemSlot.isEmpty;
        radialSlot.isFull = itemSlot.isFull;
    }

    /// <summary>
    /// Swaps the information of one slot to another and vice versa
    /// </summary>
    /// <param name="itemSlot1"></param>
    /// <param name="itemSlot2"></param>
    public void SwapSlots(ItemSlot_UI itemSlot1, ItemSlot_UI itemSlot2)
    {
        ItemSlot_UI tempSlot = new ItemSlot_UI();

        //copies data from item slot 1 to the temp slot
        CopySlots(itemSlot1, tempSlot, false);

        //Copies data from the item slot 2 to item slot 1
        CopySlots(itemSlot2, itemSlot1,true);

        //Copies the data from the temp item slot holding item slot 1 info into item slot 2
        CopySlots(tempSlot, itemSlot2,true);

        backpackQuickMenu.CopyRadialSlotInfo();
    }
    #endregion

}
