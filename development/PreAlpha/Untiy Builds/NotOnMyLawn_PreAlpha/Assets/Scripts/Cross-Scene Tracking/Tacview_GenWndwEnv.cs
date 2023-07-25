using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Tacview_GenWndwEnv : MonoBehaviour
{

    public List<Obj_TacviewTracker> objectsInTacview= new List<Obj_TacviewTracker>();
    public GameObject windowviewCounterpart;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if object is in tacview and not already in the list of objects
        if (collision.gameObject.layer == 31) //31 is tacview layer
        {
            //attempt to fetch tacview tracker script
            Obj_TacviewTracker colTacviewTracker = collision.GetComponent<Obj_TacviewTracker>();

            //if tacview tracker is fetched successfully and it is NOT already in the list
            if (colTacviewTracker != null &&
                !objectsInTacview.Contains(colTacviewTracker))
            {
                objectsInTacview.Add(colTacviewTracker);
                createWindowviewObject(colTacviewTracker);
            }
            
        }
    }

    /// <summary>
    /// when given a tacviewTracker script, creates an object in the window view that is synced
    /// to the tacview object's movements
    /// </summary>
    /// <param name="tacviewTrackerScript"></param>
    public void createWindowviewObject(Obj_TacviewTracker tacviewTrackerScript)
    {
        //instantiate new object in the window view and save its tracker script
        GameObject wndwObject = Instantiate(tacviewTrackerScript.wndwviewPrefab, new Vector3(0,-50,0), Quaternion.identity);
        Obj_WindowTracker wndwTrackerScript = wndwObject.GetComponent<Obj_WindowTracker>();

        //configure the tracker script with the tacview object's info
        wndwTrackerScript.tacviewTrackerScript= tacviewTrackerScript;
        wndwTrackerScript.windowviewAnchorPos = windowviewCounterpart.transform.position;


    }
}
