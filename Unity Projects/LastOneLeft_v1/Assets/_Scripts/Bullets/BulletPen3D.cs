using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
[RequireComponent (typeof(Rigidbody))]

public class BulletPen3D : BulletPenetration_Parent
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            //only process penetration info if the collider is part of a damage region
            if (other.GetComponent<DamageRegion3D>() != null)
            {
                ProcessPenetration(other.GetComponent<DamageRegion3D>().Region);
            }
        }
    }

}
