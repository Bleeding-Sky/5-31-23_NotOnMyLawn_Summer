using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopviewZmbHealth : MonoBehaviour
{

    public float maxHealth;
    public float currentHealth;
    public TEMP_TrackerTacticalZmb myTrackerScript;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            /* 
             * holy shit this needs to be fixed as soon as we're out of pre alpha,
             * there are WAY too many calls here and too many scripts doing
             * too many different things. pls compartmentalize, future person
             * who is reading this
             */

            //get tracker script
            myTrackerScript = GetComponent<TEMP_TrackerTacticalZmb>();
            //get tacview window object from tracker script
            GameObject myWindow = myTrackerScript.visibleThruWindow;
            //call function on window's tracker script
            myWindow.GetComponent<TEMP_TrackerTacviewWindow>().KillZombie(gameObject);
        }
    }
}
