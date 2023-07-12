using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zmb_Tacview : MonoBehaviour
{
    public Zmb_Master masterScript;

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
    void TacviewDmg(float damage)
    {
        masterScript.DamageZmb(damage);
    }

}
