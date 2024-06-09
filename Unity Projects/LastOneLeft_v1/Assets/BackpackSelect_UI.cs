using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackSelect_UI : MonoBehaviour
{
    public Backpack_UI backpack;
    public List<ItemSlot_UI> radialSlots;
    public GameObject radialSlot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSlots()
    {
        //Get the radial slots from the game object parent
        foreach (Transform child in radialSlot.transform)
        {
            ItemSlot_UI childInfo = child.GetComponent<ItemSlot_UI>();
            radialSlots.Add(childInfo);
        }
    }

    public void CopyRadialSlotInfo()
    {
        for (int i = 0; i < backpack.quickAccessAmount; i++)
        {
            backpack.CopySlots(backpack.slots[i], radialSlots[i],true);
        }
    }
}
