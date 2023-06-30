using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_ZombieDetector : MonoBehaviour
{
    //list to track all the zombies in the trap's range
    public List<GameObject> zombiesInTraps;

    public Vector3 trapPosition;
    public Vector3 zombiePosition;

    private void Update()
    {
        

    }
    //When a zombie enters the field of a trap it puts the zombie in the trap's range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Zombie"))
        {
            Debug.Log("Zombie in trap");
            zombieInTrapRange(collision.gameObject);
        }
    }

    //if the zombie exits the traps range it takes them out of the list
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Zombie"))
        {
            zombieOutofTrapRange(collision.gameObject);
        }
    }

    //adds the zombie in the list when in the trap's range
    public void zombieInTrapRange(GameObject zombie)
    {
        zombiesInTraps.Add(zombie);
    }

    //removes the zombies from the list when out of the trap's range
    private void zombieOutofTrapRange(GameObject zombie)
    {

        zombiesInTraps.Remove(zombie);
    }

    
}
