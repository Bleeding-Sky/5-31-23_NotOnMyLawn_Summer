using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]


public class DamageRegion_Zombie : MonoBehaviour
{

    [Header("CONFIG")]
    public DmgRegionEnum Region;

    [Header("DEBUG")]
    public DmgReporter_Zombie damageReporterScript;

    private void Awake()
    {
        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();

        //ensure collider is a trigger
        if (!myCollider.isTrigger)
        {
            myCollider.isTrigger = true;
        }
    }

    private void Start()
    {
        damageReporterScript = GetComponentInParent<DmgReporter_Zombie>();
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
