using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_WindowView : MonoBehaviour
{

    public float zmbFarScale = 1;
    public float zmbWindowScale = 5.22f;
    public float zmbFarYPos = 2.3f;
    public float zmbWindowYPos = .08f;

    public float windowRange = 7.5f;
    public float scaleDiff;
    public float posDiff;

    public GameObject windowZmbPrefab;
    public List<GameObject> zmbsInWindow;

    // Start is called before the first frame update
    void Start()
    {
        scaleDiff = zmbWindowScale - zmbFarScale;
        posDiff = zmbWindowYPos - zmbFarYPos;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject zmb in zmbsInWindow)
        {
            GameObject tacViewCounterpart = zmb.GetComponent<TEMP_WindowViewZombie>().tacviewCounterpart;
            TEMP_TrackerTacticalZmb tacviewZmbScript = tacViewCounterpart.GetComponent<TEMP_TrackerTacticalZmb>();
            
            //set x position based on displacement of tacview counterpart
            float windowXPos = transform.position.x + tacviewZmbScript.xDisplacementFromWindow;

            //calculate y position and scale based on tacview distance from house
            float distanceModifier = 1 - (tacviewZmbScript.distanceFromHouse / windowRange);

            float windowYPos = zmbFarYPos + (posDiff * distanceModifier);

            float scale = zmbFarScale + (scaleDiff * distanceModifier);

            //set position and scale
            zmb.transform.position = new Vector3(windowXPos, windowYPos, 0);
            zmb.transform.localScale = new Vector3(scale, scale, scale);
            
        }
    }

    public void SpawnZombie(GameObject tacViewZmb)
    {
        //create new window zombie in window scene
        GameObject newWindowZmb = Instantiate(windowZmbPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newWindowZmb.GetComponent<TEMP_WindowViewZombie>().tacviewCounterpart = tacViewZmb;

        //add zombie to list of zombies in view
        zmbsInWindow.Add(newWindowZmb);
    }

}
