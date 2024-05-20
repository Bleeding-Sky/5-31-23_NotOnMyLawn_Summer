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
    //initialized by EnterBuilding on master object when zombie moves inside
    public Health_Zombie zmbHealthScript;


    /// <summary>
    /// calls damage region methods on the zombie's health script
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="damageRegion"></param>
    public void TakeDamage(float damage, DmgRegionEnum damageRegion)
    {
        switch (damageRegion)
        {
            case DmgRegionEnum.Head:
                zmbHealthScript.Headshot(damage); break;
            case DmgRegionEnum.Body:
                zmbHealthScript.Bodyshot(damage); break;
            case DmgRegionEnum.Legs:
                zmbHealthScript.Legshot(damage); break;

        }
    }


}
