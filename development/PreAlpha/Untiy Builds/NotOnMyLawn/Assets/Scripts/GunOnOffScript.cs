using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunOnOffScript : MonoBehaviour
{
    GunScript gunScript;
    public GameObject gun;
    public bool isAiming;
    // Start is called before the first frame update
    void Start()
    {
        gunScript = GetComponent<GunScript>();

    }

    // Update is called once per frame
    void Update()
    {
 
        if(Input.GetKey(KeyCode.Mouse1))
        {
            gun.SetActive(true);
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            gun.SetActive(true);
        }

    }
}
