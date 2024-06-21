using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Status_Zombie), typeof(Health_Zombie))]

/// <summary>
/// placed on zombie master object. destroys outdoor views of the zombie when it reaches a window
/// and spawns an indoor variant indoors at the corresponding window
/// </summary>
public class EnterBuilding_Zombie : MonoBehaviour
{
    [Header("CONFIG")]
    public GameObject indoorZmbPrefab;
    public float enterBuildingDuration = 3;

    [Header("DEBUG")]
    //passed to indoor zombie for behavior purposes
    //indoor window transform where zombie will spawn
    public Transform localcopy_indoorWindowTranform;
    public bool isEnteringBuilding = false;
    public float enterBuildingTimeRemaining;

    public CameraManagement cameraManagerScript; //set w/ persistent scriptable object


    private void Update()
    {
        if (isEnteringBuilding)
        {
            AttemptMoveInside();
        }
    }

    /// <summary>
    /// saves local copy of the window's indoor transform and initiates timed process
    /// for entering building. update method will move the zombie inside when the 
    /// process completes.
    /// </summary>
    /// <param name="indoorWindowTransform"></param>
    public void StartEnterBuilding(Transform indoorWindowTransform)
    {
        //begin timer for entering building
        isEnteringBuilding = true;
        enterBuildingTimeRemaining = enterBuildingDuration;

        //save indoor window transform for moving inside later
        localcopy_indoorWindowTranform = indoorWindowTransform;


    }

    /// <summary>
    /// decrements the move inside timer and then tries to move the zombie inside. if the player is
    /// at the window, it will climb inside in the window view. if the player is not at the window,
    /// it will spawn inside in the indoor view
    /// </summary>
    private void AttemptMoveInside()
    {
        enterBuildingTimeRemaining -= Time.deltaTime;
        if (enterBuildingTimeRemaining <= 0)
        {
            //climb inside in window view if player is at window
           if (cameraManagerScript.currentEnum == CameraManagement.Cameras.Window)
            {
                ZmbMoveInsideWindow();
            }
           //spawn inside if player is not at window
           else
            {
                SpawnZmbInside(localcopy_indoorWindowTranform);
            }
        }
    }

    /// <summary>
    /// moves the zombie inside the window in window view
    /// </summary>
    private void ZmbMoveInsideWindow()
    {
        //zombie will move inside window in window view and begin damaging the player.
        //it will not spawn in the indoor view until the player exits the window.

        //disables position tracking on the window zombie and moves it inside
        GetComponentInChildren<EnterWindowHelper_Zombie>().EnterWindow();
        //destroys the overhead view zombie
        Destroy(GetComponentInChildren<ZombieTracker_Overhead>().gameObject);

    }

    /// <summary>
    /// deletes all outside views of the zombie and spawns + configures its indoor variant at the
    /// window's position inside. basically moves the zombie inside the building
    /// </summary>
    /// <param name="indoorWindowTransform"></param>
    private void SpawnZmbInside(Transform indoorWindowTransform)
    {
        //delete all children (other views of the zombie)
        Transform[] outdoorViews = GetComponentsInChildren<Transform>();

        foreach (Transform view in outdoorViews)
        {
            //this makes the object NOT delete itself, since
            //getcomponentsinchildren ALSO gets components in self
            if (view != transform)
            {
                Destroy(view.gameObject);
            }
        }

        //spawn indoor zombie as child of master object
        GameObject newIndoorZombie = Instantiate(indoorZmbPrefab, this.transform);

        //set zmb pos to the pos of the window inside, like it crawled thru
        newIndoorZombie.transform.position = new Vector3(indoorWindowTransform.position.x, 0, 0);


        isEnteringBuilding = false;
    }


}
