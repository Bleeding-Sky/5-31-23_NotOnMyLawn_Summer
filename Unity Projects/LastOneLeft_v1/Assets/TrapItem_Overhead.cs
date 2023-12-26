using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Trap Item")]
public class TrapItem_Overhead : ScriptableObject
{
    public GameObject Trap;
    public int TrapCost;
}
