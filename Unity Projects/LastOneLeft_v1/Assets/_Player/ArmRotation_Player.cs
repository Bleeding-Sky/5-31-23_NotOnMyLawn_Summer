using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation_Player : MonoBehaviour
{
    [Header("DEBUG")]
    public float itemRotation;

    // Update is called once per frame
    void Update()
    {
        //Determines the direction the arm will follow the mouse 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        itemRotation = rotZ;
        transform.localRotation = Quaternion.Euler(0, 0, rotZ);
    }
}
