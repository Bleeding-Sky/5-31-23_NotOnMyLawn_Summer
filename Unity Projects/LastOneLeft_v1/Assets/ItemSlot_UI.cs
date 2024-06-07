using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot_UI : MonoBehaviour
{
    public bool isEmpty;
    public bool isFull;

    public int maxAmount;
    public int currentAmount;

    public int slotID;

    [SerializeField] private string itemName;
    [SerializeField] private int maxUses;
    [SerializeField] private Sprite iconSprite;

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

            Image image = GetComponent<Image>();
            image.sprite = iconSprite;
            currentAmount += 1;
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
}
