using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public Rigidbody2D zombRigidBody;
    public float speed;
    public float z;
    public float zLimit;
    public float y;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (z > zLimit)
        {
            transform.position = new Vector3(0, y, z);
            z = z - Time.deltaTime;
        }
    }
}
