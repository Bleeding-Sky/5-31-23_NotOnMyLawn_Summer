using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

#nullable enable

/// <summary>
/// handles health tracking, status effect tracking/calculations, and death
/// </summary>
public class Zmb_StatusManager : MonoBehaviour
{

    #region data values
    //used for destroying all views of the zombie when it dies
    public Zmb_Tacview? tacviewScript;
    public Zmb_Window? windowScript;
    public Zmb_Indoor? indoorScript;

    //health trackers
    public float health;
    public float headHealth;
    public float bodyHealth;
    public float legHealth;

    //incoming damage multipliers
    public float headDmgMultiplier;
    public float bodyDmgMultiplier;
    public float legDmgMultiplier;

    //statuses
    public bool isStumbling = false;
    public bool isStunned = false;
    public bool isCrawling = false;

    //data scriptable object
    public ZombieData dataSO;

    #endregion

    //limb and overall damage
    #region damage methods
    public void Headshot(float dmgVal)
    {
        Debug.Log($"headshot detected for {dmgVal} damage");

        headHealth -= dmgVal;
        DamageHealth(dmgVal * headDmgMultiplier);
    }

    public void Bodyshot(float dmgVal)
    {
        Debug.Log($"bodyshot detected for {dmgVal} damage");

        bodyHealth -= dmgVal;
        DamageHealth(dmgVal * bodyDmgMultiplier);
    }

    public void Legshot(float dmgVal)
    {
        Debug.Log($"legshot detected for {dmgVal} damage");

        legHealth -= dmgVal;
        DamageHealth(dmgVal * legDmgMultiplier);
    }

    /// <summary>
    /// damages the zombie for a specified float value
    /// </summary>
    public void DamageHealth(float dmgVal)
    {
        health -= dmgVal;
    }

    #endregion

    /// <summary>
    /// destroys all views of this zombie, along with the master object
    /// </summary>
    public void KillZmb()
    {
        if (tacviewScript != null) { Destroy(tacviewScript.gameObject); }
        if (windowScript != null) { Destroy(windowScript.gameObject); }
        if (indoorScript != null) { Destroy(indoorScript.gameObject); }
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //fetch all health values
        health = dataSO.zombieHealth;
        headHealth = dataSO.headMaxHealth;
        bodyHealth = dataSO.bodyMaxHealth;
        legHealth = dataSO.legMaxHealth;

        //fetch limb damage multiplier values
        //hello everybody my name is multiplier
        headDmgMultiplier = dataSO.headDmgMultiplier;
        bodyDmgMultiplier = dataSO.bodyDmgMultiplier;
        legDmgMultiplier = dataSO.legDmgMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            KillZmb();
            Debug.Log("zombie killed");
        }
    }

    //the following methods will eventually handle effects and anims when these statuses occur
    #region status methods
    public void DoStumble()
    {
        isStumbling = true;
    }

    public void EndStumble()
    {
        isStumbling = false;
    }

    public void DoStun()
    {
        isStunned = true;
    }

    public void EndStun()
    {
        isStunned = false;
    }

    public void DoCrawl()
    {
        isCrawling = true;
    }

    public void EndCrawl()
    {
        isCrawling= false;
    }
    #endregion

}
