using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAtWindow : MonoBehaviour
{
    public Transform ZombieAtGoalPoint;
    public float zombiePosition;

    public GameObject indoorZombie;
    public Transform windowLocation;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ZombieAtGoalPoint.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        zombiePosition = transform.position.z;

        if(zombiePosition < -8)
        {
            Debug.Log("Zombie Reached Window");
            ShiftZombieIndoor();
        }
    }

    void ShiftZombieIndoor()
    {   
        Instantiate(indoorZombie, windowLocation.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
