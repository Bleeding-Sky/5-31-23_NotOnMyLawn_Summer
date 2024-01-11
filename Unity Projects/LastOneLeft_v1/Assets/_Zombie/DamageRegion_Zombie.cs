using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//region enum for config
public enum DmgRegionEnum { Head, Body, Legs };

[RequireComponent(typeof(BoxCollider2D))]
public class DamageRegion_Zombie : MonoBehaviour
{
    

    [Header("CONFIG")]
    public DmgRegionEnum Region;
    public DmgReporter_Zombie damageReporterScript;

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
            damageReporterScript.TakeDamage(damageTaken, Region);
        }
    }
}
