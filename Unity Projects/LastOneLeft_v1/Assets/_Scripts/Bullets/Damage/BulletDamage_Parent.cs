using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletInfo))]

public class BulletDamage_Parent : MonoBehaviour
{

    [Header("DEBUG")]
    [SerializeField] BulletInfo myBulletInfo;

    private void Awake()
    {
        myBulletInfo = GetComponent<BulletInfo>();
    }

    /// <summary>
    /// passes all bullet info values to an enemy's damage region script
    /// </summary>
    /// <param name="damageRegionScript"></param>
    protected virtual void DealDamage(DamageRegion_Parent damageRegionScript)
    {
        damageRegionScript.TakeDamage(  myBulletInfo.damage,
                                        myBulletInfo.critDamageMultiplier,
                                        myBulletInfo.armoredDamageMultiplier, 
                                        myBulletInfo.weakDamageMultiplier,
                                        myBulletInfo.statusMultiplier
                                        );
    }
}
