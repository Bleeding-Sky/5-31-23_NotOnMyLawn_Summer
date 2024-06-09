using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleInventory_UI : MonoBehaviour, IPointerDownHandler
{

    public GameObject backpack;
    public GameObject radialMenu;
    public bool active;
    public void OnPointerDown(PointerEventData eventData)
    {

        if (backpack.activeInHierarchy == true)
        {
            active = true;
        }
        else
        {
            active = false;
        }

        if (!active)
        {
            backpack.SetActive(true);
            radialMenu.SetActive(false);
            active = true;
        }
    }
}
