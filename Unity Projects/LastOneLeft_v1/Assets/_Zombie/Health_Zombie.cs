using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Zombie : MonoBehaviour
{
    [Header("CONFIG")]
    //data scriptable object
    public Data_Zombie dataSO;

    [Header("DEBUG")]
    //health trackers
    public float health;
    public float headHealth;
    public float bodyHealth;
    public float legHealth;

    //incoming damage multipliers
    public float headDmgMultiplier;
    public float bodyDmgMultiplier;
    public float legDmgMultiplier;

    

    // Start is called before the first frame update
    void Start()
    {
        FetchHealthValues();
    }

    /// <summary>
    /// Fetches overall health value AND limb health values from a Data_Zombie scriptable object.
    /// </summary>
    private void FetchHealthValues()
    {
        //fetch all health values
        health = dataSO.zombieMaxHealth;
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
        DamageHealth(dmgVal * headDmgMultiplier);
    }

    public void Bodyshot(float dmgVal)
    {
        bodyHealth -= dmgVal;
        DamageHealth(dmgVal * bodyDmgMultiplier);
    }

    public void Legshot(float dmgVal)
    {
        legHealth -= dmgVal;
        DamageHealth(dmgVal * legDmgMultiplier);
    }

    /// <summary>
    /// damages the zombie for a specified float value
    /// </summary>
    public void DamageHealth(float dmgVal)
    {
        health -= dmgVal;
        Debug.Log($"zombie damaged for {dmgVal} damage");
    }

    #endregion

}
