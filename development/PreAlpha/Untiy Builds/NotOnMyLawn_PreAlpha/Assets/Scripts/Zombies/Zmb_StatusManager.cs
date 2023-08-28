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

    public float health;

    //statuses
    public bool isCrawiling = false;

    public void headshot(float dmgVal)
    {
        Debug.Log($"headshot detected for {dmgVal} damage");
    }

    public void bodyshot(float dmgVal)
    {
        Debug.Log($"bodyshot detected for {dmgVal} damage");
    }

    public void legshot(float dmgVal)
    {
        Debug.Log($"legshot detected for {dmgVal} damage");
    }

    /// <summary>
    /// damages the zombie for a specified float value (OUTDATED)
    /// </summary>
    /// <param name="damage"></param>
    public void DamageZmb(float damage)
    {
        health -= damage;
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
