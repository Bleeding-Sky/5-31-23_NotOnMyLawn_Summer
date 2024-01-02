using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBuilding_Zombie : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterBuilding()
    {
        //delete all children (other views of the zombie)
        Transform[] outdoorViews = GetComponentsInChildren<Transform>();
        foreach (Transform view in outdoorViews)
        {
            Destroy(view);
        }

        //spawn indoor zombie
        //link indoor zombie to master
    }
}
