using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Item : MonoBehaviour
{
    //These are Item details that every item will have
    //This information will be passed to the item slots through the 
    //Backpack UI and this information will be used to also drop items
    [SerializeField] public string itemName;
    [SerializeField] public enum itemType{ Health, Trap };
    [SerializeField] public int maxUses;
    [SerializeField] public Sprite iconSprite;
    [SerializeField] public int maxStackable;
}
