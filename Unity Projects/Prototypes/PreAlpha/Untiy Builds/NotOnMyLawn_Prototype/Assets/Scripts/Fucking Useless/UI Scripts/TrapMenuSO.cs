using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class TrapMenuSO : ScriptableObject
{
    [SerializeField]
    private List<TrapItem> trapInventoryItems;

    [field: SerializeField]
    public int Size { get; set; } = 10;

    public event Action<Dictionary<int, TrapItem>> OnInventoryUpdated;

    public void Initialize()
    {
        trapInventoryItems = new List<TrapItem>();

        for(int i = 0; i < Size; i++)
        {
            trapInventoryItems.Add(TrapItem.GetEmptyItem());
        }
    }

    public int AddItem(ItemScriptableObjects item, int quantity)
    {
        if(item.IsStackable == false)
        {
            for (int i = 0; i < trapInventoryItems.Count; i++)
            {
                while (quantity > 0 && isInventoryFull() == false)
                {
                    quantity -= AddItemToFirstFreeSlot(item, 1);
                }
                InformAboutChange();
                return quantity;
            }
        }
        quantity = AddStackableItem(item, quantity);
        InformAboutChange();
        return quantity;
    }

    private int AddItemToFirstFreeSlot(ItemScriptableObjects item, int quantity)
    {
        TrapItem newItem = new TrapItem
        {
            item = item,
            quantity = quantity,

        };

        for (int i = 0; i < trapInventoryItems.Count; i++)
        {
            if (trapInventoryItems[i].IsEmpty)
            {
                trapInventoryItems[i] = newItem;
                return quantity;
            }
        }
        return 0;
    }


    private bool isInventoryFull()
        => trapInventoryItems.Where(item => item.IsEmpty).Any() == false;

    private int AddStackableItem(ItemScriptableObjects item, int quantity)
    {
        for (int i = 0; i < trapInventoryItems.Count; i++)
        {
            if (trapInventoryItems[i].IsEmpty)
                continue;
            if (trapInventoryItems[i].item.ID == item.ID)
            {
                int amountPossibleToTake =
                        trapInventoryItems[i].item.MaxStackSize - trapInventoryItems[i].quantity;

                if (quantity > amountPossibleToTake)
                {
                    trapInventoryItems[i] = trapInventoryItems[i]
                        .ChangeQuantity(trapInventoryItems[i].item.MaxStackSize);
                    quantity -= amountPossibleToTake;
                }
                else
                {
                    trapInventoryItems[i] = trapInventoryItems[i]
                        .ChangeQuantity(trapInventoryItems[i].quantity + quantity);
                    InformAboutChange();
                    return 0;
                }
            }
        }
        while(quantity > 0 && isInventoryFull() == false)
        {
            int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
            quantity -= newQuantity;
            AddItemToFirstFreeSlot(item, newQuantity);
        }
        return quantity;
    }

    public void AddItem(TrapItem item)
    {
        AddItem(item.item, item.quantity);
    }

    public Dictionary<int, TrapItem> GetCurrentInventoryState()
    {
        Dictionary<int, TrapItem> returnValue =
            new Dictionary<int, TrapItem>();

        for (int i = 0; i < trapInventoryItems.Count; i++)
        {
            if (trapInventoryItems[i].IsEmpty)
                continue;
            returnValue[i] = trapInventoryItems[i];
        }
        return returnValue;
    }
    public TrapItem GetItemAt(int itemIndex)
    {
        return trapInventoryItems[itemIndex];
    }

    public void SwapItems(int itemIndex_1, int itemIndex_2)
    {
        TrapItem item1 = trapInventoryItems[itemIndex_1];
        trapInventoryItems[itemIndex_1] = trapInventoryItems[itemIndex_2];
        trapInventoryItems[itemIndex_2] = item1;
        InformAboutChange();
    }

    private void InformAboutChange()
    {
        OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
    }
}

[Serializable]

public struct TrapItem
{
    public int quantity;
    public ItemScriptableObjects item;
    public bool IsEmpty => item == null;

    public TrapItem ChangeQuantity(int newQuantity)
    {
        return new TrapItem
        {
            item = this.item,
            quantity = newQuantity,
        };

    }

    public static TrapItem GetEmptyItem() => new TrapItem
    {
        item = null,
        quantity = 0,
    };


}
