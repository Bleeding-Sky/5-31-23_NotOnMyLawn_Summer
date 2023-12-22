using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideBulletScript : MonoBehaviour
{
    public Rigidbody2D bulletRb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        bulletRb.velocity = new Vector3(0, 0, 30).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
