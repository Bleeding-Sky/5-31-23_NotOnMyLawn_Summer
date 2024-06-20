using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScreenPosition_Camera : MonoBehaviour
{
    public Vector3 worldPos;
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitLocation;
        if (Physics.Raycast(ray, out hitLocation, 1000))
        {
            worldPos = hitLocation.point;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Debug.Log(worldPos);
        }
    }
}
