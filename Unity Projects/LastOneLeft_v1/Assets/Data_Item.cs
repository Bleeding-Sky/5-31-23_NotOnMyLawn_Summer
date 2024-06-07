using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Item : MonoBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] public enum itemType{ Health, Trap };
    [SerializeField] public int maxUses;
    [SerializeField] public Sprite iconSprite;
    [SerializeField] public int maxStackable;
}
