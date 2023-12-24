using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Zombie : MonoBehaviour
{

    //health trackers
    public float health;
    public float headHealth;
    public float bodyHealth;
    public float legHealth;

    //incoming damage multipliers
    public float headDmgMultiplier;
    public float bodyDmgMultiplier;
    public float legDmgMultiplier;

    //data scriptable object
    public Data_Zombie dataSO;

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


}
