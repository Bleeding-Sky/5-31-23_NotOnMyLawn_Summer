using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTracker_Overhead : ObjectTracker_Overhead
{

    [Header("DEBUG (Zombie only)")]
    //used to instantiate window zombie as child of the master script
    public GameObject ZmbMasterParentObj;

    private void Awake()
    {
        ZmbMasterParentObj = GetComponentInParent<Health_Zombie>().gameObject;
    }

}
