using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(
    typeof(Status_Zombie),
    typeof(LimbLoss_Zombie)
    )]

public class Health_Zombie : MonoBehaviour
{
    [Header("CONFIG")]

    //health values
    public float maxHealth = 30;
    public float currentHealth;
    [SerializeField] float headHealth = 6;
    public float bodyHealth = 12;
    [SerializeField] float legHealth = 3;


    //incoming damage multipliers
    [SerializeField] float headDmgMultiplier = 1.5f;
    [SerializeField] float bodyDmgMultiplier = 1;
    [SerializeField] float legDmgMultiplier = 0.5f;

    [Header("DEBUG")]
    [SerializeField] Status_Zombie statusScript;
    [SerializeField] LimbLoss_Zombie limbLossScript;


    private void Awake()
    {
        statusScript = GetComponent<Status_Zombie>();
        limbLossScript = GetComponent <LimbLoss_Zombie>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            KillZmb();
        }
    }


    //limb and overall damage
    //  damages limbs with full incoming damage, then damages overall zombie health with
    //  damage region multipliers
    #region damage methods
    public void Headshot(float dmgVal)
    {
        headHealth -= dmgVal;

        //attempt to stun if head has health
        //attempt to break head if its health is 0
        if (headHealth > 0)
        {
            statusScript.ProcessHeadshotStatus();
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
        statusScript.ProcessBodyshotStatus();
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
            statusScript.ProcessLegshotStatus();
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

    /// <summary>
    /// destroys master object, and therefore all it's children (different views of same zombie)
    /// </summary>
    public void KillZmb()
    {
        Destroy(gameObject);
    }

}
