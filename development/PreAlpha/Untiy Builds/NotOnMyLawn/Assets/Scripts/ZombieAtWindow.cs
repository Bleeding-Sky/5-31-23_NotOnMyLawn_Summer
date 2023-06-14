using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAtWindow : MonoBehaviour
{
    public Transform ZombieAtGoalPoint;
    public float zombiePosition;

    public GameObject indoorZombie;
    public Transform windowLocation;

    public float Timer;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ZombieAtGoalPoint.transform.position.z);
        Timer = 0;
        Debug.Log(Timer);
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        zombiePosition = transform.position.z;

        if(zombiePosition < -8.9)
        {
            Debug.Log("Zombie Reached Window");
            ShiftZombieIndoor();
        }
    }

    void ShiftZombieIndoor()
    {
        Debug.Log(Timer);
        Instantiate(indoorZombie, windowLocation.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
