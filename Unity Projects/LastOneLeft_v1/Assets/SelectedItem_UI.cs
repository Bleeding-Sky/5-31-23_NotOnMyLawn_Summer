using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItem_UI : MonoBehaviour
{
    public ItemSlot_UI selectedSlot;
    public Backpack_UI backpack;

    public int slotID;
    public Sprite spr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Is used when an item is selected giving the user an
    /// option to drop or use the item
    /// </summary>
    /// <param name="item"></param>
    public void SelectSlot(ItemSlot_UI item)
    {
        selectedSlot = item;
        slotID = item.slotID;
        spr = item.iconSprite;
        SetIcon(spr);

    }

    /// <summary>
    /// Used to set the profile icon for the slot
    /// </summary>
    /// <param name="spr"></param>
    public void SetIcon(Sprite sprite)
    {
        Image image = GetComponent<Image>();
        image.sprite = sprite;
    }

    /// <summary>
    /// Clears this slot and nullifies all fields
    /// </summary>
    public void ClearSelectedSlot()
    {
        selectedSlot = null;
        slotID = 0;
        spr = null;
    }
}
