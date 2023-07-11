using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm_Rotation : MonoBehaviour
{
    public float itemRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        itemRotation = rotZ;
        transform.localRotation = Quaternion.Euler(0, 0, rotZ);
        


    }
}
