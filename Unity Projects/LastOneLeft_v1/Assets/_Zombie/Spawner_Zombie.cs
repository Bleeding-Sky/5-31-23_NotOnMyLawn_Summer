using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Zombie : MonoBehaviour
{

    //public GameObject overheadParentObject;   will be used if we decide to make the zombies
    //                                          children of the overhead view parent object

    [Header("CONFIG")]
    public GameObject testZmbOverheadPrefab;
    public GameObject testZmbMasterPrefab;
    public Vector2 spawnPosition;
    public Transform overheadAnchorTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SpawnOverheadZmb();
        }
    }

    /// <summary>
    /// spawns a new zombie master object as a child of the zombies parent object.
    /// adds new child overhead zombie to that zombie master object and configures it
    /// </summary>
    public void SpawnOverheadZmb()
    {
        //instantiate master object as child of "zombies" parent object
        GameObject masterObject = Instantiate(testZmbMasterPrefab, this.transform);

        //instantiate overhead zombie as child of its master parent object
        GameObject newOverheadZmb = Instantiate(testZmbOverheadPrefab, masterObject.transform);

        //configure position and register master in overhead tracker script
        newOverheadZmb.transform.position = spawnPosition;
        ObjectTracker_Overhead zmbTrackerScript = newOverheadZmb.GetComponent<ObjectTracker_Overhead>();
        zmbTrackerScript.ZmbMasterParentObj = masterObject;

        //set target for pathfinding
        OverheadPathing_Zombie pathingScript = newOverheadZmb.GetComponent<OverheadPathing_Zombie>();
        pathingScript.target = overheadAnchorTransform;
    }


}
