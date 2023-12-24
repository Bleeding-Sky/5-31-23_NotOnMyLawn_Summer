using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Zombie : MonoBehaviour
{
    /*
    //used for destroying all views of the zombie when it dies
    public Zmb_Tacview? tacviewScript;
    public Zmb_Window? windowScript;
    public Zmb_Indoor? indoorScript;
    */

    //used for retrieving current health value from health script
    public Health_Zombie healthScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healthScript.health <= 0)
        {
            KillZmb();
        }
    }


    /// <summary>
    /// destroys all views of this zombie, along with the master object
    /// </summary>
    public void KillZmb()
    {
        /*
        if (tacviewScript != null) { Destroy(tacviewScript.gameObject); }
        if (windowScript != null) { Destroy(windowScript.gameObject); }
        if (indoorScript != null) { Destroy(indoorScript.gameObject); }
        */
        Destroy(gameObject);
    }
}
