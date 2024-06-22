using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(
    typeof(Status_Zombie),
    typeof(LimbLoss_Zombie),
    typeof(ItemDropper)
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
    [SerializeField] ItemDropper itemDropperScript;

    public float currentHealth;
    public float armoredRegionCurrentHealth;


    private void Awake()
    {
        statusScript = GetComponent<Status_Zombie>();
        limbLossScript = GetComponent <LimbLoss_Zombie>();
        currentHealth = maxHealth;
        armoredRegionCurrentHealth = armoredRegionMaxHealth;

        itemDropperScript = GetComponent<ItemDropper>();
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
        critRegionHealth -= (damage * critDamageMultiplier);

        //attempt to stun
        statusScript.ProcessCritHit(statusMultiplier);

        //break head if its health is 0
        //also attempt to stun again as a bonus for limb breaking
        if (critRegionHealth <= 0)
        {
            limbLossScript.BreakHead(maxHealth, currentHealth);
            statusScript.ProcessCritHit(statusMultiplier);
        }

        DamageHealth(damage * critDamageMultiplier);
    }

    public void DamageArmored(float damage, float armoredDamageMultiplier, float statusMultiplier)
    {
        armoredRegionCurrentHealth -= (damage * armoredDamageMultiplier);
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
        weakRegionHealth -= (damage * weakDamageMultiplier);

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
        //Debug.Log($"Health damaged for {dmgVal} damage");
        currentHealth -= dmgVal;
    }

    #endregion

    /// <summary>
    /// destroys master object, and therefore all it's children (different views of same zombie)
    /// </summary>
    public void Die()
    {
        itemDropperScript.AttemptDrop();
        Destroy(gameObject);
    }

}
