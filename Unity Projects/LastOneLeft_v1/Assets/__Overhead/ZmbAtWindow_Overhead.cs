using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZmbAtWindow_Overhead : MonoBehaviour
{
    [Header("CONFIG")]
    public Transform indoorWindowTransform;
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
            zmbMasterEnterBuildingScript = overheadTrackerScript.GetComponent<EnterBuilding_Zombie>();
            zmbMasterEnterBuildingScript.EnterBuilding(indoorWindowTransform);
        }
    }
}
