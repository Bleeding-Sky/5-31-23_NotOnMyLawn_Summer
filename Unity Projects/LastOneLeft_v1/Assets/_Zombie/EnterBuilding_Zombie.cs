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

    [Header("DEBUG")]
    //passed to indoor zombie for behavior purposes
    public Status_Zombie statusScript;
    public Health_Zombie healthScript;

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

    /// <summary>
    /// destroys all outside views of a zombie and spawns an indoor view of it at
    /// a given transform, specifying the indoor position of the window.
    /// </summary>
    /// <param name="indoorWindowTransform"></param>
    public void EnterBuilding(Transform indoorWindowTransform)
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

        //configure indoor zombie components
        newIndoorZombie.GetComponent<Behavior_Zombie>().zombieStates = statusScript;

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
        spriteControllerScript.fetchSpriteRenderers();

    }
}
