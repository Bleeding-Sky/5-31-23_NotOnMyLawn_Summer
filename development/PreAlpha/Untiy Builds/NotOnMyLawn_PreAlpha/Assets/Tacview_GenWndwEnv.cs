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
        if (!GetComponent<BoxCollider2D>().isTrigger)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if object is in tacview and not already in the list of objects
        if (collision.gameObject.layer == 7 && //7 is tacview layer
            !objectsInTacview.Contains(collision.GetComponent<Obj_TacviewTracker>())) 
        {
            objectsInTacview.Add(collision.GetComponent<Obj_TacviewTracker>());
            createWindowviewObject(collision.GetComponent<Obj_TacviewTracker>());
            
        }
    }

    public void createWindowviewObject(Obj_TacviewTracker tacviewTrackerScript)
    {
        GameObject wndwObject = Instantiate(tacviewTrackerScript.wndwviewPrefab, new Vector3(0,-50,0), Quaternion.identity);
        wndwObject.GetComponent<Obj_WindowTracker>().tacviewTrackerScript= tacviewTrackerScript;
        wndwObject.GetComponent<Obj_WindowTracker>().windowviewAnchorPos = windowviewCounterpart.transform.position;


    }
}
