using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WndwObjGenerator_Overhead : MonoBehaviour
{
    [Header("CONFIG")]
    public GameObject windowAnchorObject;

    [Header("DEBUG")]
    public List<ObjectTracker_Overhead> overheadObjectsList = new List<ObjectTracker_Overhead>();
    

    // Start is called before the first frame update
    void Start()
    {
        //set collider to be a trigger if not already set
        BoxCollider2D myBoxCollider = GetComponent<BoxCollider2D>();
        if (!myBoxCollider.isTrigger)
        {
            myBoxCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if object is in overhead and not already in the list of objects
        if (collision.gameObject.layer == 31) //31 is overhead layer
        {
            //attempt to fetch overhead object tracker script
            ObjectTracker_Overhead colObjectTracker = collision.GetComponent<ObjectTracker_Overhead>();

            //if overhead tracker is fetched successfully and it is NOT already in the list
            if (colObjectTracker != null &&
                !overheadObjectsList.Contains(colObjectTracker))
            {
                //add to list and create corresponding window object
                overheadObjectsList.Add(colObjectTracker);
                createWindowviewObject(colObjectTracker);

                //also register this anchor as the object's overhead anchor if not already registered
                if (colObjectTracker.overheadAnchorObject != gameObject)
                {
                    colObjectTracker.overheadAnchorObject = gameObject;
                }

            }

        }
    }

    /// <summary>
    /// when given an ObjectTracker_Overhead script, creates an object in the window view 
    /// that is synced to the tacview object's movements
    /// </summary>
    /// <param name="overheadTrackerScript"></param>
    public void createWindowviewObject(ObjectTracker_Overhead overheadTrackerScript)
    {

        //instantiate new object in the window view and save its tracker script
        GameObject wndwObject = Instantiate(overheadTrackerScript.windowViewPrefab, new Vector3(0, -50, 0), Quaternion.identity);
        PositionSync_Window wndwTrackerScript = wndwObject.GetComponent<PositionSync_Window>();

        wndwObject.transform.position = new Vector3(0, CalculateYSpawnOffset(wndwTrackerScript), 0);

        //configure the tracker script with the tacview object's info
        wndwTrackerScript.overheadTrackerScript = overheadTrackerScript;


    }

    /// <summary>
    /// finds the correct y value to spawn a window object at so that its bottom edge is at y=0
    /// </summary>
    /// <param name="wndwObject"></param>
    /// <param name="wndwTrackerScript"></param>
    private float CalculateYSpawnOffset(PositionSync_Window wndwTrackerScript)
    {
        //find correct y value to spawn object at
        Bounds spriteBounds = wndwTrackerScript.spriteChildObject.GetComponent<BoxCollider2D>().bounds;
        float spawnY = (spriteBounds.center.y + spriteBounds.extents.y) - spriteBounds.center.y;
        return spawnY;
    }

}
