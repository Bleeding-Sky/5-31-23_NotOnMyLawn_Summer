using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class DamageRegion2D : DamageRegion_Parent
{

    private void Awake()
    {
        Collider2D myCollider = GetComponent<Collider2D>();

        //ensure collider is a trigger
        if (!myCollider.isTrigger)
        {
            myCollider.isTrigger = true;
        }
    }


}
