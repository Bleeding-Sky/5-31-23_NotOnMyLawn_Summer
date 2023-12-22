using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Zmb_Window : MonoBehaviour
{
    public Zmb_StatusManager masterScript;

    /*NOTE
     * this script needs to handle checking for and reporting damage
     * done to a zombie in the window view.
     */

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
    void WindowDmg(float damage)
    {
        masterScript.DamageHealth(damage);
    }
}
