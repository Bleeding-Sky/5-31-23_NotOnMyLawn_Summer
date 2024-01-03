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

            //retrieve enterbuilding script from master and call the method on it
            //done by getting master obj ref from overhead tracker, then getting the enterbuilding script

            //TODO:
            //HOLY SHIT this sucks. needs to be streamlined bc getcomponent is slow
            ObjectTracker_Overhead overheadTrackerScript = collision.GetComponent<ObjectTracker_Overhead>();
            zmbMasterEnterBuildingScript = overheadTrackerScript.ZmbMasterParentObj.GetComponent<EnterBuilding_Zombie>();
            zmbMasterEnterBuildingScript.EnterBuilding(indoorWindowTransform);
        }
    }
}
