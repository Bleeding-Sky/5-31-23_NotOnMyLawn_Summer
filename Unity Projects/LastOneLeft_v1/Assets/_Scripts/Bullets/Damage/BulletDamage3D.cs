using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
//for some reason the collision doesnt register unless the bullet has a rigidbody
[RequireComponent (typeof(Rigidbody))]

public class BulletDamage3D : BulletDamage_Parent
{

    private void Start()
    {
        Collider myCollider = GetComponent<Collider>();

        //ensure collider is a trigger
        if (!myCollider.isTrigger)
        {
            myCollider.isTrigger = true;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            //only deal damage if the collider is part of a damage region
            if (other.GetComponent<DamageRegion_Parent>() != null)
            {
                //Debug.Log("DAMAGING ZOMBIE");
                DealDamage(other.GetComponent<DamageRegion_Parent>());
            }
        }
    }
}
