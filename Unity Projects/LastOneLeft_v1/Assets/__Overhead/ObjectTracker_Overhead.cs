using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// calculates an overhead view object's position with respect to an overhead view anchor
/// </summary>
public class ObjectTracker_Overhead : MonoBehaviour
{

    [Header("CONFIG")]
    public GameObject overheadAnchorObject;

    //window view prefab of this object for use by window view environment generation script
    public GameObject windowViewPrefab;
    

    [Header("DEBUG")]
    public Transform overheadAnchorTransform;
    public float xDisplacementFromAnchor;
    public float distanceFromAnchor;

    // Start is called before the first frame update
    void Start()
    {
        if (overheadAnchorObject == null)
        {
            overheadAnchorObject = GameObject.FindWithTag("Overhead Anchor");
        }

        //fetch transform from anchor object for constant position tracking
        overheadAnchorTransform = overheadAnchorObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDisplacements();
    }

    /// <summary>
    /// calculates the displacement of an object's x and y position with respect to an anchor
    /// </summary>
    void CalculateDisplacements()
    {
        Vector3 myPos = transform.position;
        xDisplacementFromAnchor = myPos.x - overheadAnchorTransform.position.x;
        distanceFromAnchor = myPos.y - overheadAnchorTransform.position.y;
    }
}
