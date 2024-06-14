using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This Script is just the base class for all the Use Item logic
 * Create a new script that inherits this class and override the function
 * to create custom logic for each individual item
 */
[CreateAssetMenu(menuName = "Default Use Item")]
public class UseItem_SO : ScriptableObject
{
    public GameObject player;

    /// <summary>
    /// This function will be the base for all other inhereted classes to override the function logic
    /// </summary>
    public virtual void UseItem()
    {
        Debug.Log("Use default item");
    }
}
