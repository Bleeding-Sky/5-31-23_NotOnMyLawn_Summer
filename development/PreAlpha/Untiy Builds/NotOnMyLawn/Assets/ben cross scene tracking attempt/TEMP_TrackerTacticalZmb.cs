using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_TrackerTacticalZmb : MonoBehaviour
{

    public GameObject visibleThruWindow;
    public float xDisplacementFromWindow;
    public float distanceFromHouse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if seen through a window, calculate position values relative to window
        if (visibleThruWindow != null)
        {
            xDisplacementFromWindow = transform.position.x - visibleThruWindow.transform.position.x;
            distanceFromHouse = transform.position.y - visibleThruWindow.transform.position.y;
        }
    }

    

}
