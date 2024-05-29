using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]

public class BulletPen2D : BulletPenetration_Parent
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            //only process penetration info if the collider is part of a damage region
            if(collision.GetComponent<DamageRegion2D>() != null)
            {
                ProcessPenetration(collision.GetComponent<DamageRegion2D>().Region);
            }
        }
    }
}
