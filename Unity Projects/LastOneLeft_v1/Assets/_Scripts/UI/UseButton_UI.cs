using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UseButton_UI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public SelectedItem_UI selectedSlot;
    public bool hoveringButton;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Use the Item");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoveringButton = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoveringButton = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(hoveringButton == true)
        {
            selectedSlot.UseSlotItem();
        }
    }
}
