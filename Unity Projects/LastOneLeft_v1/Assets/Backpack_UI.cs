using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Backpack_UI : MonoBehaviour
{
    public Inventory_Player inventory;
    public List<GameObject> gunInventory;

    public List<ItemSlot_UI> slots;
    public int quickAccessAmount;
    public int smallPocketAmount;
    public int largePocketAmount;
    private void Start()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].slotID = i;
        }
    }
    public void AddItem(GameObject item)
    {
        Debug.Log("adding item");
        bool spotFound = false;
        Data_Item itemData = item.GetComponent<Data_Item>();
        for (int i = 0; i < slots.Count; i++)
        {
            if(itemData.name == slots[i].name && !slots[i].isFull)
            {
                spotFound = true;
                Debug.Log("spot found");
                slots[i].AddItem(item);
                break;
            }
        }
        
        if (!spotFound)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                Debug.Log("Looking");
                if (slots[i].isEmpty)
                {
                    Debug.Log("empty spot found");
                    slots[i].AddItem(item);
                    break;
                }
            }
        }

    }
}
