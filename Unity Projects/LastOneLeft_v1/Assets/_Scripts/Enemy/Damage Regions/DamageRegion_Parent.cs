using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageRegion_Parent : MonoBehaviour
{

    [Header("CONFIG")]
    public DmgRegionEnum Region;

    [Header("DEBUG")]
    public Health_Zombie healthScript;


    private void Start()
    {
        healthScript = GetComponentInParent<Health_Zombie>();
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
        switch (Region)
        {
            case DmgRegionEnum.Crit:
                healthScript.DamageCrit(damage, critDamageMultiplier, statusMultiplier);
                break;

            case DmgRegionEnum.Armored:
                healthScript.DamageArmored(damage, armoredDamageMultiplier, statusMultiplier);
                break;

            case DmgRegionEnum.Weak:
                healthScript.DamageWeak(damage, weakDamageMultiplier, statusMultiplier);
                break;
        }

    }

}
