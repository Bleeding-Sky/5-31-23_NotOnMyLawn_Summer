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
    [Header("CONFIG (Zombie Only)")]
    public bool isZombie;
    public GameObject ZmbMasterParentObj;

    [Header("DEBUG")]
    public Vector2 overheadAnchorPos;
    public float xDisplacementFromAnchor;
    public float distanceFromAnchor;

    // Start is called before the first frame update
    void Start()
    {
        if (overheadAnchorObject == null)
        {
            overheadAnchorObject = GameObject.FindWithTag("Overhead Anchor");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //update local tracking of anchor's position every frame
        overheadAnchorPos = overheadAnchorObject.transform.position;

        CalculateDisplacements();
    }

    /// <summary>
    /// calculates the displacement of an object's x and y position with respect to an anchor
    /// </summary>
    void CalculateDisplacements()
    {
        Vector3 myPos = transform.position;
        xDisplacementFromAnchor = myPos.x - overheadAnchorPos.x;
        distanceFromAnchor = myPos.y - overheadAnchorPos.y;
    }
}
