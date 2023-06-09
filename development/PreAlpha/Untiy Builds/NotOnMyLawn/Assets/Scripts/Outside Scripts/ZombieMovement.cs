using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public Rigidbody2D zombRigidBody;
    public float speed;
    public float z;
    private float zLimit;
    public float y;
    public float x;

    public float xPosition;

    public Transform SpawnPoint;
    public Transform zombieGoalPoint;
    // Start is called before the first frame update
    void Start()
    {
        zLimit = z - 14;
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
        Debug.Log(x);
    }

    void ZombieMoveToGoal()
    {
        if(x > zombieGoalPoint.transform.position.x)
        {
            x = x - (speed * Time.deltaTime);
            Debug.Log("Right");
        }
        else if(x < zombieGoalPoint.transform.position.x)
        {
            x = x + (speed * Time.deltaTime);
            Debug.Log("Left");
        }
        else if(x == zombieGoalPoint.transform.position.x)
        {
            x = zombieGoalPoint.transform.position.x;
            Debug.Log("Middle");
        }
    }

    public void KillZombie()
    {
        Destroy(gameObject);
    }
}
