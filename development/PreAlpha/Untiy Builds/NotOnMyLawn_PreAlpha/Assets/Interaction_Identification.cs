using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Identification : MonoBehaviour
{
    [Header("Interaction Type")]
    public bool isItem;
    public bool isEnviormentObject;

    [Header("Enviornment Type")]
    public bool isWindow;
    public bool isDoor;

    [Header("Item Type")]
    public bool isGun;
    public bool isBandage;
}
