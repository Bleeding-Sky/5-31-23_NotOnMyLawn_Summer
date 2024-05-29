using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to all child views of a zombie. reports damage to the health script on the
/// zombie's master object
/// </summary>
public class DmgReporter_Zombie : MonoBehaviour
{

    [Header("DEBUG")]
    public Health_Zombie zmbHealthScript;

    private void Start()
    {
        zmbHealthScript = GetComponentInParent<Health_Zombie>();
    }


    /// <summary>
    /// calls damage region methods on the zombie's health script
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="damageRegion"></param>
    public void TakeDamage( float damage, DmgRegionEnum damageRegion, 
                            float regionDamageMultiplier, float statusMultiplier)
    {
        switch (damageRegion)
        {
            case DmgRegionEnum.Crit:
                zmbHealthScript.DamageCrit(damage, regionDamageMultiplier, statusMultiplier); break;
            case DmgRegionEnum.Armored:
                zmbHealthScript.DamageArmored(damage, regionDamageMultiplier, statusMultiplier); break;
            case DmgRegionEnum.Weak:
                zmbHealthScript.DamageWeak(damage, regionDamageMultiplier, statusMultiplier); break;

        }
    }


}
