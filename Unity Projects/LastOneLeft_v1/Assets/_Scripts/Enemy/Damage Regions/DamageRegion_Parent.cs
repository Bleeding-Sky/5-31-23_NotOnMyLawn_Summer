using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageRegion_Parent : MonoBehaviour
{

    [Header("CONFIG")]
    public DmgRegionEnum Region;

    [Header("DEBUG")]
    public DmgReporter_Zombie damageReporterScript;


    private void Start()
    {
        damageReporterScript = GetComponentInParent<DmgReporter_Zombie>();
    }

    /// <summary>
    /// recieves incoming bullet info, and then reports the correct values based on the region hit.
    /// called by bullets when they impact the enemy.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="critDamageMultiplier"></param>
    /// <param name="statusMultiplier"></param>
    public void TakeDamage(float damage, float critDamageMultiplier, float armoredDamageMultiplier,
                            float weakDamageMultiplier, float statusMultiplier)
    {
        //determine which damage multiplier to use
        float correctDamageMultiplier = 0;
        switch (Region)
        {
            case DmgRegionEnum.Crit:
                correctDamageMultiplier = critDamageMultiplier;
                break;

            case DmgRegionEnum.Armored:
                correctDamageMultiplier = armoredDamageMultiplier;
                break;

            case DmgRegionEnum.Weak:
                correctDamageMultiplier = weakDamageMultiplier;
                break;
        }

        damageReporterScript.TakeDamage(damage, Region, correctDamageMultiplier, statusMultiplier);
    }

}
