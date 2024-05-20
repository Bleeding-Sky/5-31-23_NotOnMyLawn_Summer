using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// syncs a window view object's position with its overhead view counterpart
/// </summary>
public class PositionSync_Window : MonoBehaviour
{

    [Header("DEBUG")] //can be configured, but is typically set
                      //automatically on creation of window object
    public ObjectTracker_Overhead overheadTrackerScript;
    public GameObject windowAnchorObject;


    // Start is called before the first frame update
    void Start()
    {
        //get window view anchor if needed
        if (windowAnchorObject == null)
        {
            windowAnchorObject = GameObject.FindWithTag("Window Anchor");
        }
    }

    // Update is called once per frame
    void Update()
    {
        MatchOverheadPos();
    }

    /// <summary>
    /// translates x and y displacement from overhead to x and z displacement,
    /// then sets the window zombie's position vector accordingly
    /// </summary>
    void MatchOverheadPos()
    {
        float overheadXDisplacement = overheadTrackerScript.xDisplacementFromAnchor;
        float overheadDistanceFromAnchor = overheadTrackerScript.yDisplacementFromAnchor;

        float myXDisplacement = windowAnchorObject.transform.position.x + overheadXDisplacement;
        float myZDisplacement = windowAnchorObject.transform.position.z + overheadDistanceFromAnchor;

        transform.position = new Vector3(myXDisplacement, transform.position.y, myZDisplacement);
    }
}
