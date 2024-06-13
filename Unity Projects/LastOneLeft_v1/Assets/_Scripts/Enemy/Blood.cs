using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]

public class Blood : MonoBehaviour
{

    Rigidbody myRigidBody;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("3D Environment Geometry"))
        {
            myRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

}
