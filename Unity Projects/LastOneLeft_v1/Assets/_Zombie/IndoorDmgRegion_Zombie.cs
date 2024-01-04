using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class IndoorDmgRegion_Zombie : MonoBehaviour
{
    //region enum for config
    public enum DmgRegionEnum { Head, Body, Legs };

    [Header("CONFIG")]
    public DmgRegionEnum Region;
    public Health_Zombie zmbHealthScript;

    private void Start()
    {
        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();

        //ensure collider is a trigger
        if (!myCollider.isTrigger)
        {
            myCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            float damageTaken = collision.gameObject.GetComponent<BulletDmg_Item>().damage;

            switch (Region)
            {
                case DmgRegionEnum.Head:
                    zmbHealthScript.Headshot(damageTaken); break;
                case DmgRegionEnum.Body:
                    zmbHealthScript.Bodyshot(damageTaken); break;
                case DmgRegionEnum.Legs:
                    zmbHealthScript.Legshot(damageTaken); break;

            }
        }
    }
}