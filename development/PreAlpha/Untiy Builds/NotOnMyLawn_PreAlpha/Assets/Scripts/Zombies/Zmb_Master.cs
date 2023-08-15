using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

#nullable enable

public class Zmb_Master : MonoBehaviour
{
    public Zmb_Tacview? tacviewScript;
    public Zmb_Window? windowScript;
    public Zmb_Indoor? indoorScript;

    public float health;
    public bool isCrawiling = false;
    public Vector3 targetPos;

    public void headshot(float dmgVal)
    {

    }

    public void bodyshot(float dmgVal)
    {

    }

    public void legshot(float dmgVal)
    {

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
