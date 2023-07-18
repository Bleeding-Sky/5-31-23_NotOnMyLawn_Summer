using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_TacviewTracker : MonoBehaviour
{
    //typically a window, an anchor point present in both tacview and window scenes
    public Vector2 anchorPos;

    public float xDisplacementFromTarget;
    public float distanceFromTarget;

    // Update is called once per frame
    void Update()
    {
        CalculateDisplacements();
    }

    void CalculateDisplacements()
    {
        Vector3 myPos = transform.position;
        xDisplacementFromTarget = myPos.x - anchorPos.x;
        distanceFromTarget = myPos.y - anchorPos.y;
    }

}
