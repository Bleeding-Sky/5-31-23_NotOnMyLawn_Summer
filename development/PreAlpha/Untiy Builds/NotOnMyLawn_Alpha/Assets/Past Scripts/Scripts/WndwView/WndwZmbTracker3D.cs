using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WndwZmbTracker3D : MonoBehaviour
{

    public GameObject windowZmbPrefab;
    public List<GameObject> zmbsInWindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        

        foreach (GameObject zmb in zmbsInWindow)
        {
            GameObject tacviewCounterpart = zmb.GetComponent<TEMP_WindowViewZombie>().tacviewCounterpart;
            float distanceFromHouse = tacviewCounterpart.GetComponent<TEMP_TrackerTacticalZmb>().distanceFromHouse;
            float xDisplacementFromWndw = tacviewCounterpart.GetComponent<TEMP_TrackerTacticalZmb>().xDisplacementFromWindow;
            //tacview x -> wndwview x
            //wndw.z + distancefromhouse -> wndwview z
            zmb.transform.position = new Vector3(transform.position.x + xDisplacementFromWndw,
                                                0,
                                                transform.position.z + distanceFromHouse);
        }

        
    }

    public void SpawnZmb(GameObject tacViewZmb)
    {
        //create new window zombie in window scene
        GameObject newWindowZmb = Instantiate(windowZmbPrefab, Vector3.zero, Quaternion.identity);
        //set tacview counterpart of new window zombie to tacViewZmb
        newWindowZmb.GetComponent<TEMP_WindowViewZombie>().tacviewCounterpart = tacViewZmb;

        //add zombie to list of zombies in view
        zmbsInWindow.Add(newWindowZmb);
    }

    public void KillWndwZmb(GameObject tacViewZmb)
    {

        //find zmb in list that matches tacview zmb
        foreach (GameObject zmb in zmbsInWindow)
        {
            if (zmb.GetComponent<TEMP_WindowViewZombie>().tacviewCounterpart == tacViewZmb)
            {
                //for some reason the wndwview zmb list starts erroring
                //if you dont remove the zombie from the list before
                //destroying it???? but this doesnt happen in the tacview
                //list so idk what this is but it works lmao fml
                zmbsInWindow.Remove(zmb);
                Destroy(zmb);
            }
        }
    }
}
