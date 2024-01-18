using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Zombie : MonoBehaviour
{
    [Header("CONFIG")]
    //data scriptable object
    public Data_Zombie dataSO;

    [Header("DEBUG")]
    [SerializeField] Status_Zombie statusScript;
    [SerializeField] LimbLoss_Zombie limbLossScript;

    //health trackers
    public float maxHealth;
    public float currentHealth;
    public float headHealth;
    public float bodyHealth;
    public float legHealth;

    //incoming damage multipliers
    public float headDmgMultiplier;
    public float bodyDmgMultiplier;
    public float legDmgMultiplier;

    private void Awake()
    {
        statusScript = GetComponent<Status_Zombie>();
        FetchHealthValues();
    }

    /// <summary>
    /// Fetches overall health value AND limb health values from a Data_Zombie scriptable object.
    /// </summary>
    private void FetchHealthValues()
    {
        //fetch all health values
        maxHealth = dataSO.zombieMaxHealth;
        currentHealth = maxHealth;
        headHealth = dataSO.headMaxHealth;
        bodyHealth = dataSO.bodyMaxHealth;
        legHealth = dataSO.legMaxHealth;

        //fetch limb damage multiplier values
        //hello everybody my name is multiplier
        headDmgMultiplier = dataSO.headDmgMultiplier;
        bodyDmgMultiplier = dataSO.bodyDmgMultiplier;
        legDmgMultiplier = dataSO.legDmgMultiplier;
    }

    //limb and overall damage
    #region damage methods
    public void Headshot(float dmgVal)
    {
        headHealth -= dmgVal;

        //attempt to stun if head has health
        //attempt to break head if its health is 0
        if (headHealth > 0)
        {
            statusScript.AttemptStun(DmgRegionEnum.Head);
        }
        else
        {
            limbLossScript.AttemptHeadBreak(maxHealth, currentHealth);
        }

        DamageHealth(dmgVal * headDmgMultiplier);
    }

    public void Bodyshot(float dmgVal)
    {
        //attempt stun and arm loss regardless of body health
        bodyHealth -= dmgVal;
        statusScript.AttemptStun(DmgRegionEnum.Body);
        limbLossScript.AttemptArmLoss(bodyHealth);
        DamageHealth(dmgVal * bodyDmgMultiplier);

    }

    public void Legshot(float dmgVal)
    {
        legHealth -= dmgVal;

        //attempt stumble if legs have health remaining
        //attempt to break legs if legs have no health
        if(legHealth > 0)
        {
            statusScript.AttemptStumble();
        }
        else
        {
            limbLossScript.AttemptLegBreak();
        }

        DamageHealth(dmgVal * legDmgMultiplier);
    }

    /// <summary>
    /// damages the zombie for a specified float value
    /// </summary>
    public void DamageHealth(float dmgVal)
    {
        currentHealth -= dmgVal;
    }

    #endregion

}
