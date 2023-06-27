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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Zombie"))
        {

            ProcessZmbInWindow(collision);

        }
    }

    /// <summary>
    /// adds zombie to list of zombies in view + spawns it in the windowview
    /// </summary>
    /// <param name="collision"></param>
    private void ProcessZmbInWindow(Collider2D collision)
    {
        //add zombie to inView list
        zmbsInView.Add(collision.gameObject);
        //add this window to zombie's data
        TEMP_TrackerTacticalZmb zombieTrackerScript = collision.GetComponent<TEMP_TrackerTacticalZmb>();
        zombieTrackerScript.visibleThruWindow = gameObject;

        //create zombie in window view
        wndwViewCounterpart.GetComponent<WndwZmbTracker3D>().SpawnZmb(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Zombie"))
        {
            //remove zombie from inView list
            zmbsInView.Remove(collision.gameObject);
            KillZombie(collision.gameObject);
        }
    }

    public void KillZombie(GameObject tacviewZmb)
    {
        Debug.Log("Tacview window KillZmb script activated");
        //remove this zmb from the 2d window list
        wndwViewCounterpart.GetComponent<WndwZmbTracker3D>().KillWndwZmb(tacviewZmb);

        //delete the tacview zombie
        Destroy(tacviewZmb);
    }

}
