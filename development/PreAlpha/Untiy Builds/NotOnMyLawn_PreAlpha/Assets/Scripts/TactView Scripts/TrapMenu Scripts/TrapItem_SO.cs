using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Trap_Item")]
public class TrapItem_SO : ScriptableObject
{
    public GameObject Trap;

    public int TrapCost;
}
