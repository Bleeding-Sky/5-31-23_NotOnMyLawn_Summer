using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Script : MonoBehaviour
{
    public List<GameObject> item;
    public Item_Interaction ItemInteractionScript;
    public GameObject handInv;
    public int maxInventorySize;
    public int currentInventoryUsed;
    // Start is called before the first frame update
    void Start()
    {
        maxInventorySize = 5;
    }

    // Update is called once per frame
    void Update()
    {
        currentInventoryUsed = item.Count;
    }

    public void storeItems(GameObject InteractedItem)
    {
        if (currentInventoryUsed < maxInventorySize)
        {
            if (currentInventoryUsed == 0)
            {
                ItemInteractionScript.PickUpItem();
                item.Add(InteractedItem);
                InteractedItem.transform.parent = gameObject.transform;
                Player_InHand HandInventory = handInv.GetComponent<Player_InHand>();
                HandInventory.PlaceObjectInHand(InteractedItem);
            }
            else
            {
                item.Add(InteractedItem);
                InteractedItem.transform.parent = gameObject.transform;
                InteractedItem.SetActive(false);
            }

        }
        else
        {
            Debug.Log("INVENTORY FULL");
        }
    }
}
