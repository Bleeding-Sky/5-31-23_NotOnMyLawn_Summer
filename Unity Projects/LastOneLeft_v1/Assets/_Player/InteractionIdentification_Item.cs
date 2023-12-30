using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionIdentification_Item : MonoBehaviour
{
    /* 
     * This script will be attached to every type of 
     * enviornment and or object to determine what type
     * of interaction the player will have with the object
     */
    [Header("Interaction Type")]
    public bool isItem;
    public bool isEnviormentObject;

    [Header("Enviornment Type")]
    public bool isWindow;
    public bool isDoor;
    public bool isBackgroundDoor;
    public bool isBoardPile;

    [Header("Item Type")]
    public bool isGun;
    public bool isBandage;
    public bool isMeleeWeapon;
}
