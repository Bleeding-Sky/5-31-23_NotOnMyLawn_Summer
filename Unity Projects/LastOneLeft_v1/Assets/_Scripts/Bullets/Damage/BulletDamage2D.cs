using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class BulletDamage2D : BulletDamage_Parent
{

    private void Start()
    {
        Collider2D myCollider = GetComponent<Collider2D>();

        //ensure collider is a trigger
        if (!myCollider.isTrigger)
        {
            myCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            //only deal damage if the collider is part of a damage region
            if (collision.GetComponent<DamageRegion_Parent>() != null)
            {
                DealDamage(collision.GetComponent<DamageRegion_Parent>());
            }
        }
    }
}
