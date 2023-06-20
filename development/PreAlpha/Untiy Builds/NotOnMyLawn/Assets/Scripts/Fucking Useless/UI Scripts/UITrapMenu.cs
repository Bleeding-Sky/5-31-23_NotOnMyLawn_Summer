using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrapMenu : MonoBehaviour
{
    [SerializeField]
    private UITrapItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private MouseFollow mouseFollower;


    List<UITrapItem> listOfUIItems = new List<UITrapItem>();

    public event Action<int>
                OnItemActionRequested,
                OnStartDragging; 

    public event Action<int, int> OnSwapItems;

    public int currentlyDraggedItemIndex = -1;

    private void Awake()
    {
        Hidemenu();
        mouseFollower.Toggle(false);
    }
    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UITrapItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }

    internal void ResetAllItems()
    {
        foreach (var item in listOfUIItems)
        {
            item.ResetData();
        }
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (listOfUIItems.Count > itemIndex)
        {
            listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
        }
    }
    private void HandleShowItemActions(UITrapItem inventoryItemUI)
    {
       
    }

    private void HandleEndDrag(UITrapItem inventoryItemUI)
    {
        ResetDraggedItem();
    }

    private void HandleSwap(UITrapItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);

    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
        return;
    }

    private void HandleBeginDrag(UITrapItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
            return;
        currentlyDraggedItemIndex = index;
        HandleItemSelection(inventoryItemUI);
        OnStartDragging?.Invoke(index);
    }

    public void CreateDraggedItem(Sprite sprite, int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }

    private void HandleItemSelection(UITrapItem inventoryItemUI)
    {
        Debug.Log(inventoryItemUI.name);
    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);
        
    }

    public void Hidemenu()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }
}
