using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Zombie : MonoBehaviour
{
    [Header("DEBUG")]
    //used for retrieving current health value from health script
    public Health_Zombie healthScript;

    // Start is called before the first frame update
    void Start()
    {
        //fetch health script for health monitoring
        healthScript = GetComponent<Health_Zombie>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (healthScript.currentHealth <= 0)
        {
            KillZmb();
        }
    }


    /// <summary>
    /// destroys master object, and therefore all it's children (different views of same zombie)
    /// </summary>
    public void KillZmb()
    {  
        Destroy(gameObject);
    }
}
