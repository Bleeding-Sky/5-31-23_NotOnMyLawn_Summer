using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Use Trap")]
public class UseTrap_SO : UseItem_SO
{
    public override void UseItem()
    {
        Debug.Log("Use Trap");
    }
}
