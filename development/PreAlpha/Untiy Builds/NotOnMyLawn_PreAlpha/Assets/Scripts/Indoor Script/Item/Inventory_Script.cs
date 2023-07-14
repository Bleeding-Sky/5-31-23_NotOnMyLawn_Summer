using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Script : MonoBehaviour
{
    [Header("Inventory")]
    public List<GameObject> item;

    [Header("CONFIG")]
    public int maxInventorySize;
    private int currentMaxInventorySize;
    
    [Header("DEBUG")]
    public Item_Interaction ItemInteractionScript;
    public Item_PickedUp itemAssignment;
    public GameObject handInv;
    public int numberPressed;
    public int lastInventorySlotChosen;
    // Start is called before the first frame update
    void Start()
    {
        //Sets what the current MaxInventory size is so that it can be changed later with upgrades
        currentMaxInventorySize = maxInventorySize;
    }

    // Update is called once per frame
    void Update()
    {

        //gets what inventory slot is chosen and runs it through a function to select the item
        numberPressed = GetPressedNumber();
        if(numberPressed > 0)
        {
            ChosenInventorySlot(numberPressed);
        }
    }

    //Stores the item in an item list
    public void StoreItems(GameObject InteractedItem)
    {
        //Goes through each item in the list and looks for the nearest empty slot
        for (int i = 0; i < maxInventorySize; i++)
        {
            //if a slot if empty it fills it with the interacted item
            if (item[i] == null)
            {
                //Sets the item in the list and determines what type of item it is giving it all the necessary information and setting the object in as the child of the inventory game object to visualize it better
                //TLDR sets it in the inventory and inherits information from the player
                item[i] = InteractedItem;
                itemAssignment.DetermineItemType(InteractedItem);
                InteractedItem.transform.parent = gameObject.transform;   
                InteractedItem.SetActive(false);
                maxInventorySize = 0;
            }

        }
        maxInventorySize = currentMaxInventorySize;
    }

    //gets the players input from the number keys and uses that to choose an item in the inventory
    public void ChosenInventorySlot(int numberKeyPressed)
    {
        int inventorySlot = numberKeyPressed - 1;
        Player_InHand putItemInHand = handInv.GetComponent<Player_InHand>();
        //if there is no item in hand it'll place the object in the player's hand
        if (!putItemInHand.itemInHand)
        {
            putItemInHand.PlaceObjectInHand(item[inventorySlot]);
        }
        //if there is an object in the players hand it swaps it out for the chosen inventory slot
        else if(putItemInHand.itemInHand && lastInventorySlotChosen != inventorySlot)
        {
            //if they want to swap with an empty slot it unequips the object
            //otherwise it swaps the items
            if(item[inventorySlot] == null)
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
        else if(putItemInHand.itemInHand && lastInventorySlotChosen == inventorySlot)
        {
            putItemInHand.UnequipItem();
        }
        lastInventorySlotChosen = inventorySlot;
    }

    //function that gets the inputted number and returns it to the numberPressed variable to select the inventory slot
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
