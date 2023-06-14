using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float speed;
    public float z;
    private float zLimit;
    public float y;
    public float x;

    public float xPosition;
    public float randomGoalPoint;
    public float goalPoint;

    public float ratio;
    public float scaleNumber;

 

    public Transform SpawnPoint;
    public Transform zombieGoalPoint;
    // Start is called before the first frame update
    void Start()
    {
      
        zLimit = z - 14;
        ratio = .0654f;
        y = SpawnPoint.transform.position.y;
        x = SpawnPoint.transform.position.x;
        Debug.Log(SpawnPoint.transform.position);
        ZombieXPosition();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (z > zLimit)
        {
            transform.position = new Vector3(x, y, z);
            ZombieMoveToGoal();
            z = z - Time.deltaTime;
        }
    }

    void ZombieXPosition()
    {
        xPosition = Random.Range(-13, 14);
        goalPoint = zombieGoalPoint.transform.position.x + (xPosition * ratio);
        speed = .07077f * Mathf.Abs(xPosition);
        

        if(xPosition > 0)
        {
            x = x + xPosition;
        }
        else if(xPosition < 0)
        {
            x = x + xPosition;
        }
        else if(xPosition == 0)
        {
            x = transform.position.x;
        }
    }

    void ZombieMoveToGoal()
    {
        if(x > goalPoint)
        {
            x = x - (speed * Time.deltaTime);
        }
        else if(x < goalPoint)
        {
            x = x + (speed * Time.deltaTime);
        }
        else if(x == goalPoint)
        {
            x = goalPoint;
        }
    }
    

    public void KillZombie()
    {
        Destroy(gameObject);
    }
}
