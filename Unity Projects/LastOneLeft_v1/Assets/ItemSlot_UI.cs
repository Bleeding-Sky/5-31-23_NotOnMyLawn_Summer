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

    public bool AddItem(GameObject item)
    {
        if(isEmpty)
        {
            Data_Item itemData = item.GetComponent<Data_Item>();
            itemName = itemData.itemName;

            maxUses = itemData.maxUses;
            iconSprite = itemData.iconSprite;
            maxAmount = itemData.maxStackable;
            SetIcon(iconSprite);
            currentAmount += 1;
            isEmpty = false;
            checkFull();
            return true;
        }
        else if(!isEmpty && !isFull)
        {
            Data_Item itemInfo = item.GetComponent<Data_Item>();
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

    public void checkFull()
    {
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
        if (Backpack.draggedSlot != gameObject)
        {
            Backpack.overSlot = gameObject;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Backpack.overSlot == gameObject)
        {
            Backpack.overSlot = null;
        }
        Backpack.draggedSlot = gameObject;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(Backpack.overSlot == gameObject)
        {
            Backpack.overSlot = null;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if(Backpack.overSlot != null && Backpack.draggedSlot != null)
        {
            ItemSlot_UI overSlotInfo = Backpack.overSlot.GetComponent<ItemSlot_UI>();
            ItemSlot_UI draggedSlotInfo = Backpack.draggedSlot.GetComponent<ItemSlot_UI>();
            Backpack.SwapSlots(overSlotInfo, draggedSlotInfo);
        }
        Backpack.draggedSlot = null;
    }
}
