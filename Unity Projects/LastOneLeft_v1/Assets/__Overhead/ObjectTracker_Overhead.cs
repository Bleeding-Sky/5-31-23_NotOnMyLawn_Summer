using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// calculates an overhead view object's position with respect to an overhead view anchor
/// </summary>
public class ObjectTracker_Overhead : MonoBehaviour
{

    [Header("CONFIG")]
    //window view prefab of this object for use by window view environment generation script
    public GameObject windowViewPrefab;
    

    [Header("DEBUG")]
    public Transform overheadAnchorTransform; //can be set manually for debug purposes
    public float xDisplacementFromAnchor;
    public float yDisplacementFromAnchor;

    // Start is called before the first frame update
    void Start()
    {
        //fetch overhead anchor transform
        if (overheadAnchorTransform == null)
        {
            overheadAnchorTransform = GameObject.FindWithTag("Overhead Anchor").transform;
        }
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
        yDisplacementFromAnchor = myPos.y - overheadAnchorTransform.position.y;
    }
}
