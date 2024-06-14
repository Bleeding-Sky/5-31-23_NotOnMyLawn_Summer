using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Use Bandage")]

//Class that inherets the Use Item logic and customizes it for the use of bandages
public class UseBandage_SO : UseItem_SO
{
    public int heal;
    public override void UseItem()
    {
        HealthManager_Player health = player.GetComponent<HealthManager_Player>();
        health.AddHealth(heal);
        Debug.Log("Heal Player");
    }
}
