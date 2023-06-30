using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Exploded : MonoBehaviour
{
    public Vector3 trapPosition;
    public Vector3 zombiePosition;
    public Vector3 distanceExplodedPositive;
    public Vector3 distanceExplodedNegative;

    public bool exploded;
    public List<GameObject> traps;
    // Start is called before the first frame update
    void Start()
    {
        exploded = false;
    }

    // Update is called once per frame
    void Update()
    {
        //tracks zombie position and detects when the zombie is exploded
        zombiePosition = transform.position;
        if (exploded)
        {
            Exploded();
        }
    }

    //if the zombie is exploded this function is used
    public void Exploded()
    {
        //if the zombie moves past any of the distance in the x and y direction the zombie stops moving
        Rigidbody2D zombie = gameObject.GetComponent<Rigidbody2D>();
        if(distanceExplodedPositive.x > zombiePosition.x && distanceExplodedNegative.x < zombiePosition.x 
            && distanceExplodedPositive.y > zombiePosition.y && distanceExplodedNegative.y<zombiePosition.y)
        {

        }
        else
        {
            zombie.velocity = Vector3.zero;
            exploded = false;
        }
    }
}
