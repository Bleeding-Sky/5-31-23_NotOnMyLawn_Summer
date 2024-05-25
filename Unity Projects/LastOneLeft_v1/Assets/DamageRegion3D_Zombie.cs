using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DamageRegion3D_Zombie : MonoBehaviour
{
    [Header("CONFIG")]
    public DmgRegionEnum Region;

    [Header("DEBUG")]
    public DmgReporter_Zombie damageReporterScript;

    private void Awake()
    {
        BoxCollider myCollider = GetComponent<BoxCollider>();

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            float damageTaken = other.gameObject.GetComponent<BulletDmg_Item>().damage;
            damageReporterScript.TakeDamage(damageTaken, Region);
        }
    }
}


