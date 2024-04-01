using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterWindowHelper_Zombie : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// disables positionsync script on the zombie and moves it so it
    /// appears inside the window in window view
    /// </summary>
    public void EnterWindow()
    {
        //disable position sync script so zombie doesnt move
        GetComponent<PositionSync_Window>().enabled = false;

        //move zombie inside in window view
        transform.position += new Vector3(0, 0, -2);

    }

}
