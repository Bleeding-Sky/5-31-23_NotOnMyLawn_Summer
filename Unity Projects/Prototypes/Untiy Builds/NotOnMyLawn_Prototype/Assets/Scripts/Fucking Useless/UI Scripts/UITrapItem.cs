using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITrapItem : MonoBehaviour , IPointerClickHandler,IBeginDragHandler,IEndDragHandler,IDropHandler, IDragHandler
{
    [SerializeField]
    private Image trapItemImage;

    [SerializeField]
    private TMP_Text quantityText;

    public event Action<UITrapItem> OnItemClicked, OnItemDroppedOn, 
        OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;

    private bool empty = true;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    
    public void ResetData()
    {
        trapItemImage.gameObject.SetActive(false);
    }
    public void SetData(Sprite sprite, int quantity)
    {
        trapItemImage.gameObject.SetActive(true);
        trapItemImage.sprite = sprite;
        quantityText.text = quantity + "";
        empty = false;
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
    }
}
