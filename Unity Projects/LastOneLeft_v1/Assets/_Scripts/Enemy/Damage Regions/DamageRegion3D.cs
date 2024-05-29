using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class DamageRegion3D : DamageRegion_Parent
{

    private void Awake()
    {
        Collider myCollider = GetComponent<Collider>();

        //ensure collider is a trigger
        if (!myCollider.isTrigger)
        {
            myCollider.isTrigger = true;
        }
    }

}


