using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject Camera1;
    public GameObject Camera2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Camera1.SetActive(true);
            Camera2.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.O))
        {
            Camera1.SetActive(false);
            Camera2.SetActive(true);
        }
    }
}
