using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Window : MonoBehaviour
{
    public Rigidbody bulletRB;
    public Vector3 bulletDirection;
    public Vector3 statringPosition;

    // Start is called before the first frame update
    void Start()
    {
        
        bulletDirection = new Vector3(0, 0, 1);
        bulletRB.velocity = bulletDirection;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
