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
    [SerializeField] float critRegionHealth = 6;
    public float armoredRegionMaxHealth = 12;
    [SerializeField] float weakRegionHealth = 3;

    [Header("DEBUG")]
    [SerializeField] Status_Zombie statusScript;
    [SerializeField] LimbLoss_Zombie limbLossScript;

    public float currentHealth;
    public float armoredRegionCurrentHealth;


    private void Awake()
    {
        statusScript = GetComponent<Status_Zombie>();
        limbLossScript = GetComponent <LimbLoss_Zombie>();
        currentHealth = maxHealth;
        armoredRegionCurrentHealth = armoredRegionMaxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }


    //limb and overall damage
    //  damages limbs with full incoming damage, then damages overall zombie health with
    //  damage region multipliers
    #region damage methods
    public void DamageCrit(float damage, float critDamageMultiplier, float statusMultiplier)
    {
        critRegionHealth -= damage;

        //attempt to stun
        statusScript.ProcessCritHit(statusMultiplier);

        //break head if its health is 0
        //also attempt to stun again as a bonus for limb breaking
        if (critRegionHealth <= 0)
        {
            limbLossScript.BreakCritRegion(maxHealth, currentHealth);
            statusScript.ProcessCritHit(statusMultiplier);
        }

        DamageHealth(damage * critDamageMultiplier);
    }

    public void DamageArmored(float damage, float armoredDamageMultiplier, float statusMultiplier)
    {
        armoredRegionCurrentHealth -= damage;
        statusScript.ProcessArmoredHit(statusMultiplier);
        limbLossScript.AttemptArmLoss(armoredRegionMaxHealth, armoredRegionCurrentHealth);

        //bonus stun chance if body is broken
        if (armoredRegionCurrentHealth <= 0)
        {
            statusScript.ProcessArmoredHit(statusMultiplier);
        }

        DamageHealth(damage * armoredDamageMultiplier);

    }

    public void DamageWeak(float damage, float weakDamageMultiplier, float statusMultiplier)
    {
        weakRegionHealth -= damage;

        //attempt stumble
        statusScript.ProcessWeakHit(statusMultiplier);

        //break legs if legs have no health
        if (weakRegionHealth <= 0)
        {
            limbLossScript.BreakLegs();
        }

        DamageHealth(damage * weakDamageMultiplier);
    }

    /// <summary>
    /// damages the zombie for a specified float value
    /// </summary>
    public void DamageHealth(float dmgVal)
    {
        Debug.Log($"Health damaged for {dmgVal} damage");
        currentHealth -= dmgVal;
    }

    #endregion

    /// <summary>
    /// destroys master object, and therefore all it's children (different views of same zombie)
    /// </summary>
    public void Die()
    {
        Destroy(gameObject);
    }

}
