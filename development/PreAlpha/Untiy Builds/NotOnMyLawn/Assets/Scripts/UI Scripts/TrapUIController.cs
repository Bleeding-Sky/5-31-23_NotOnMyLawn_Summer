using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapUIController : MonoBehaviour
{
    [SerializeField]
    private UITrapMenu trapMenuUI;

    [SerializeField]
    private TrapMenuSO trapInventoryData;

    public List<TrapItem> initialItems = new List<TrapItem>();

    private void Start()
    {
        PrepareUI();
        PrepareInventoryData();

    }

    private void PrepareInventoryData()
    {
        trapInventoryData.Initialize();
        trapInventoryData.OnInventoryUpdated += UpdateInventoryUI;
        foreach (TrapItem item in initialItems)
        {
            if (item.IsEmpty)
                continue;
            trapInventoryData.AddItem(item);
        }
    }

    private void UpdateInventoryUI(Dictionary<int, TrapItem> inventoryState)
    {
        trapMenuUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            trapMenuUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
        }
    }

    private void PrepareUI()
    {
        trapMenuUI.InitializeInventoryUI(trapInventoryData.Size);
        this.trapMenuUI.OnSwapItems += HandleSwapItems;
        this.trapMenuUI.OnStartDragging += HandleDragging;
        this.trapMenuUI.OnItemActionRequested += HandleItemActionRequest;
    }

    private void HandleItemActionRequest(int itemIndex)
    {
        
    }

    private void HandleDragging(int itemIndex)
    {
        TrapItem trapInventoryItem = trapInventoryData.GetItemAt(itemIndex);

        if (trapInventoryItem.IsEmpty)
            return;
        trapMenuUI.CreateDraggedItem(trapInventoryItem.item.ItemImage, trapInventoryItem.quantity);
        
    }

    private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
    {
        trapInventoryData.SwapItems(itemIndex_1, itemIndex_2);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(trapMenuUI.isActiveAndEnabled == false)
            {
                trapMenuUI.ShowMenu();

                foreach (var item in trapInventoryData.GetCurrentInventoryState())
                {
                    trapMenuUI.UpdateData(item.Key,
                        item.Value.item.ItemImage,
                        item.Value.quantity);
                }
            }
            else
            {
                trapMenuUI.Hidemenu();
            }
        }
    }
}
