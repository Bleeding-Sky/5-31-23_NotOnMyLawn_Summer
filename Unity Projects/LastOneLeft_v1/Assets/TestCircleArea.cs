using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCircleArea : MonoBehaviour
{
    public float anglePhi;
    public float angleTheta;
    public GameObject circle;
    public Vector3 worldPos;
    public Transform startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        double zDirection = Math.Cos((Math.PI / 180) * anglePhi) * Math.Sin((Math.PI / 180) * angleTheta);
        double yDirection = Math.Sin((Math.PI / 180) * anglePhi);
        double xDirection = Math.Cos((Math.PI / 180) * angleTheta);

        Vector3 position = new Vector3((float)xDirection, (float)yDirection, (float)zDirection);
        circle.transform.localPosition = position.normalized / 2;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MousePosition();
            Vector3 direction = worldPos - startingPosition.position;

            Debug.Log(direction.normalized);
            float angle1 = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;
            float angle2 = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            double z = Math.Cos((Math.PI / 180) * angle1) * Math.Sin((Math.PI / 180) * angle2);
            double y = Math.Sin((Math.PI / 180) * angle1);
            double x = Math.Cos((Math.PI / 180) * angle2);

            Vector3 pos = new Vector3((float)x, (float)y, (float)z);

            Debug.Log($"angle y/z: {angle1} angle2 z/x: {angle2} \n position: {pos}");
        }
    }

    public void MousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitLocation;
        if (Physics.Raycast(ray, out hitLocation, 1000))
        {
            worldPos = hitLocation.point;
        }
    }
}
