using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_WindowTracker : MonoBehaviour
{
    public Obj_TacviewTracker tacviewTrackerScript;

    //typically a window, an anchor point present in both tacview and window scenes
    public Vector3 anchorPos; 

    // Update is called once per frame
    void Update()
    {
        MatchTacviewPos();
    }

    /// <summary>
    /// translates x and y displacement from tacview to x and z displacement,
    /// then sets the window zombie's position vector accordingly
    /// </summary>
    void MatchTacviewPos()
    {
        float tacviewXDisplacement = tacviewTrackerScript.xDisplacementFromTarget;
        float tacviewDistanceFromTarget = tacviewTrackerScript.distanceFromTarget;

        float myXDisplacement = anchorPos.x + tacviewXDisplacement;
        float myZDisplacement = anchorPos.z + tacviewDistanceFromTarget;

        transform.position = new Vector3(myXDisplacement, anchorPos.y, myZDisplacement);
    }

}
