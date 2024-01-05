using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// attached to a window in the overhead view. detects if a zombie has reached the window, and
/// calls upon it's master script to move it indoors.
/// </summary>
public class ZmbAtWindow_Overhead : MonoBehaviour
{
    [Header("CONFIG")]
    public Transform indoorWindowTransform;

    [Header("DEBUG")]
    //retrieved from overhead zombie when it reaches the window
    public EnterBuilding_Zombie zmbMasterEnterBuildingScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            //NOTE: lots of getcomponents :( info needs to be passed, so we cant do events
            //      so idk how this could be done better. but this is pretty sloppy/slow


            //retrieve ref to master obj from zombietracker script
            GameObject ZmbMasterObj = collision.GetComponent<ZombieTracker_Overhead>().ZmbMasterParentObj;

            //get EnterBuilding script on master object
            zmbMasterEnterBuildingScript = ZmbMasterObj.GetComponent<EnterBuilding_Zombie>();

            //call EnterBuilding on the script to spawn zombie indoors
            zmbMasterEnterBuildingScript.EnterBuilding(indoorWindowTransform);
        }
    }
}
