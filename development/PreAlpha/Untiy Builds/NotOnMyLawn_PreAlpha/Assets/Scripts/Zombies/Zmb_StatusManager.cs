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
    public float headDmgMultiplier = 1.5f;
    public float bodyDmgMultiplier = 1;
    public float legDmgMultiplier = .5f;

    //statuses
    public bool isStumbling = false;
    public bool isStunned = false;
    public bool isCrawling = false;

    public void headshot(float dmgVal)
    {
        Debug.Log($"headshot detected for {dmgVal} damage");

        headHealth -= dmgVal;
        damageHealth(dmgVal * headDmgMultiplier);
    }

    public void bodyshot(float dmgVal)
    {
        Debug.Log($"bodyshot detected for {dmgVal} damage");

        bodyHealth -= dmgVal;
        damageHealth(dmgVal * bodyDmgMultiplier);
    }

    public void legshot(float dmgVal)
    {
        Debug.Log($"legshot detected for {dmgVal} damage");

        legHealth -= dmgVal;
        damageHealth(dmgVal * legDmgMultiplier);
    }

    /// <summary>
    /// damages the zombie for a specified float value
    /// </summary>
    public void damageHealth(float dmgVal)
    {
        health -= dmgVal;
    }

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
        //TODO: get max health value from scriptable object
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            KillZmb();
        }
    }
}
