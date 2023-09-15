using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// reports damage to the master script and calculates logic for
/// stumbles and limb damage (destroy head, break legs, etc)
/// </summary>
public class Zmb_Indoor : MonoBehaviour
{
    public Zmb_StatusManager masterScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// sends damage value to master script
    /// </summary>
    /// <param name="damage"></param>
    void IndoorDmg(float damage)
    {
        masterScript.damageHealth(damage);
    }
}
