using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InHand : MonoBehaviour
{
    public GameObject objectInHand;
    public GameObject Inventory;
    public bool itemInHand;

    public GameObject AimingArea;
    // Update is called once per frame
    void Update()
    {
        DetermineIfItemInHand();
        if(Input.GetKeyDown(KeyCode.Q))
        {
            DropItemInHand();
        }
    }

    public void DropItemInHand()
    {

    }

    public void PlaceObjectInHand(GameObject iteminHand)
    {
        objectInHand = iteminHand;
        if (objectInHand == null)
        {
            Debug.Log("No Item in hand");
        }
        else
        {
            objectInHand.SetActive(true);
            objectInHand.transform.parent = gameObject.transform;
            itemInHand = true;
        }
    }

    public void SwapItemInHand(GameObject SwappedItem)
    {
        GameObject holdItem = objectInHand;
        objectInHand = SwappedItem;
        objectInHand.SetActive(true);
        objectInHand.transform.parent = gameObject.transform;
        holdItem.transform.parent = Inventory.transform;
        holdItem.SetActive(false);
        itemInHand = true;

    }

    public void UnequipItem()
    {
        objectInHand.transform.parent = Inventory.transform;
        objectInHand.SetActive(false);
        objectInHand = null;
        itemInHand = false;
    }

    public void RemoveItemFromHand()
    {
        objectInHand.transform.SetAsLastSibling();
        objectInHand = null;
    }

    public void DetermineIfItemInHand()
    {
        if(itemInHand)
        {
            Interaction_Identification equippedItem = objectInHand.GetComponent<Interaction_Identification>();
            if(equippedItem.isGun)
            {
                //AimingArea.SetActive(true);
            }
            else if(!equippedItem.isGun)
            {
                //AimingArea.SetActive(false);
            }
        }
        else if(!itemInHand)
        {
            //AimingArea.SetActive(false);
        }
    }
}
