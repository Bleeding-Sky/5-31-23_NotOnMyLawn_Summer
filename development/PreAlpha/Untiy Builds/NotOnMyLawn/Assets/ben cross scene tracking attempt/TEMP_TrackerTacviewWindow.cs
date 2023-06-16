using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_TrackerTacviewWindow : MonoBehaviour
{

    public GameObject wndwViewCounterpart;
    public float viewWidth;
    public float viewXMin;
    public float viewXMax;

    public List<GameObject> zmbsInView;

    // Start is called before the first frame update
    void Start()
    {
        //calculate min/max x coords of window view trigger
        viewXMin = transform.position.x - .5f * viewWidth;
        viewXMax = transform.position.x + .5f * viewWidth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Zombie"))
        {
            //add zombie to inView list
            zmbsInView.Add(collision.gameObject);
            //add this window to zombie's data
            TEMP_TrackerTacticalZmb zombieTrackerScript = collision.GetComponent<TEMP_TrackerTacticalZmb>();
            zombieTrackerScript.visibleThruWindow = gameObject;

            //create zombie in window view
            wndwViewCounterpart.GetComponent<WndwZmbTracker3D>().SpawnZmb(collision.gameObject);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Zombie"))
        {
            //remove zombie from inView list
            zmbsInView.Remove(collision.gameObject);
        }
    }

}
