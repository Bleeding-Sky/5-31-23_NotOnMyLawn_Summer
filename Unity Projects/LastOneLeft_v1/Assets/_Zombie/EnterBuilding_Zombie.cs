using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Status_Zombie))]
[RequireComponent(typeof(Health_Zombie))]

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
    public Status_Zombie statusScript;
    public Health_Zombie healthScript;
    //indoor window transform where zombie will spawn
    public Transform localcopy_indoorWindowTranform;
    public bool isEnteringBuilding = false;
    public float enterBuildingTimeRemaining;

    public bool isReadyToSpawnInside = false;
    

    [SerializeField] SpriteController_Zombie spriteControllerScript;

    private void Awake()
    {
        spriteControllerScript = GetComponent<SpriteController_Zombie>();
    }

    private void Start()
    {
        //fetch master scripts
        statusScript = GetComponent<Status_Zombie>();
        healthScript = GetComponent<Health_Zombie>();
    }

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

    private void AttemptMoveInside()
    {
        enterBuildingTimeRemaining -= Time.deltaTime;
        if (enterBuildingTimeRemaining <= 0)
        {
            /* commented out until current view tracking is implemented
           if (currentview == window)
            {
                zmbMoveInsideWindow();
            }
           else
            {
                SpawnZmbInside(localcopy_indoorWindowTranform);
            }
            */
            SpawnZmbInside(localcopy_indoorWindowTranform);
        }
    }

    /// <summary>
    /// moves the zombie inside the window in window view
    /// </summary>
    private void zmbMoveInsideWindow()
    {
        //zombie will move inside window in window view and begin damaging the player.
        //it will not spawn in the indoor view until the player exits the window.

        GetComponentInChildren<EnterWindowHelper_Zombie>().EnterWindow();
        Destroy(GetComponentInChildren<ZombieTracker_Overhead>().gameObject);
        isReadyToSpawnInside = true;

    }

    /// <summary>
    /// deletes all outside views of the zombie and spawns/configures it's indoor variant at the
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
        newIndoorZombie.transform.position = indoorWindowTransform.position;
        ConfigureIndoorComponents(newIndoorZombie);

        isEnteringBuilding = false;
    }

    /// <summary>
    /// sets up all components necessary for indoor zombie functioning
    /// </summary>
    /// <param name="newIndoorZombie"></param>
    private void ConfigureIndoorComponents(GameObject newIndoorZombie)
    {
        //configure indoor zombie components
        newIndoorZombie.GetComponent<Behavior_Zombie>().zombieStates = statusScript;
        SetUpBehaviorStatusMonitoring(newIndoorZombie);

        //save damage reporter script for dmg region config, and give it the health script
        DmgReporter_Zombie indoorDmgReporterScript = newIndoorZombie.GetComponent<DmgReporter_Zombie>();
        indoorDmgReporterScript.zmbHealthScript = healthScript;

        //link damage regions to indoor damage reporter
        DamageRegion_Zombie[] damageRegions = GetComponentsInChildren<DamageRegion_Zombie>();
        foreach (DamageRegion_Zombie damageRegionScript in damageRegions)
        {
            damageRegionScript.damageReporterScript = indoorDmgReporterScript;
        }

        //refresh renderers when spawning indoors
        spriteControllerScript.Refresh();
    }

    /// <summary>
    /// gives a ref (to the status script) to the behavior script so it can change move speed
    /// based on the zombie's state
    /// </summary>
    /// <param name="newIndoorZombie"></param>
    private void SetUpBehaviorStatusMonitoring(GameObject newIndoorZombie)
    {
        Behavior_Zombie indoorBhvrScript = newIndoorZombie.GetComponent<Behavior_Zombie>();
        indoorBhvrScript.statusScript = GetComponent<Status_Zombie>();
    }
}
