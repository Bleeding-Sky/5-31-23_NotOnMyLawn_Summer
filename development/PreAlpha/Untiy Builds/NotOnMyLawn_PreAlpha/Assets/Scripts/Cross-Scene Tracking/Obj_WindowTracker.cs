using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obj_WindowTracker : MonoBehaviour
{

    [Header("CONFIG")]
    public Obj_TacviewTracker tacviewTrackerScript;
    //OPTIONAL: script can read position of anchor object and set anchorPos accordingly
    public GameObject windowviewAnchorObject;
    //object can ALSO be configured with just an anchor position
    public Vector3 windowviewAnchorPos;
    

    private void Start()
    {
        if (windowviewAnchorObject != null)
        {
            windowviewAnchorPos = windowviewAnchorObject.transform.position;
        }
    }

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

        float myXDisplacement = windowviewAnchorPos.x + tacviewXDisplacement;
        float myZDisplacement = windowviewAnchorPos.z + tacviewDistanceFromTarget;

        transform.position = new Vector3(myXDisplacement, 0, myZDisplacement);
    }

}
