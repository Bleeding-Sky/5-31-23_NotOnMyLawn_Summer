using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot_UI : MonoBehaviour, IPointerEnterHandler,IPointerDownHandler,IPointerExitHandler,IPointerUpHandler
{
    public Backpack_UI Backpack;
    public bool isEmpty;
    public bool isFull;

    public int maxAmount;
    public int currentAmount;

    public int slotID;
    public bool isRadialSlot;

    public string itemName;
    [SerializeField] public int maxUses;
    [SerializeField] public Sprite iconSprite;

    // Start is called before the first frame update
    void Start()
    {
        isEmpty = true;
        isFull = false;
        currentAmount = 0;
    }

    /// <summary>
    /// Add the item to the slot
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool AddItem(GameObject item)
    {
        //Depending on the availability of the inventory slot it chooses a way of handeling adding an item
        if(isEmpty)
        {
            //recieves the incoming item's data and passes it along to slot
            Data_Item itemData = item.GetComponent<Data_Item>();
            itemName = itemData.itemName;
            maxUses = itemData.maxUses;
            iconSprite = itemData.iconSprite;
            maxAmount = itemData.maxStackable;
            SetIcon(iconSprite);
            currentAmount += 1;

            //Chanes the availability of the slot so that new items 
            isEmpty = false;
            checkFull();
            return true;
        }
        //When the slot is already populated by another item but it is not full
        else if(!isEmpty && !isFull)
        {
            Data_Item itemInfo = item.GetComponent<Data_Item>();
            //It will add one to the number of items in the slot
            if(itemName == itemInfo.itemName)
            {
                currentAmount += 1;
                checkFull();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// A function that checks whether the slot is full or not
    /// typically only used when adding items to the slot
    /// </summary>
    public void checkFull()
    {

        //max amount of supplies will be passed down from the item details in Data_Item
        if(currentAmount >= maxAmount)
        {
            isFull = true;
        }
        else if(currentAmount < maxAmount)
        {
            isFull = false;
        }
    }

    public void clearSlot()
    {
        itemName = null;
        maxUses = 0;
        iconSprite = null;
        maxAmount = 0;
        currentAmount = 0;
    }

    public void UseItem()
    {
        Debug.Log("Use Item");
    }

    public void SetIcon(Sprite spr)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.sprite = spr;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isRadialSlot)
        {
            if (Backpack.draggedSlot != gameObject)
            {
                Backpack.overSlot = gameObject;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isRadialSlot)
        {
            if (Backpack.overSlot == gameObject)
            {
                Backpack.overSlot = null;
            }
            Backpack.draggedSlot = gameObject;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isRadialSlot)
        {
            if (Backpack.overSlot == gameObject)
            {
                Backpack.overSlot = null;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isRadialSlot)
        {
            if (Backpack.overSlot != null && Backpack.draggedSlot != null)
            {
                ItemSlot_UI overSlotInfo = Backpack.overSlot.GetComponent<ItemSlot_UI>();
                ItemSlot_UI draggedSlotInfo = Backpack.draggedSlot.GetComponent<ItemSlot_UI>();
                Backpack.SwapSlots(overSlotInfo, draggedSlotInfo);
            }
            Backpack.draggedSlot = null;
        }
    }
}
