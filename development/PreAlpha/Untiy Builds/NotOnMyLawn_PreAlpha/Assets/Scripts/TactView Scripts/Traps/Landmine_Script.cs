using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine_Script : MonoBehaviour
{
    [Header("CONFIG")]
    public float explosionStrength;
    public bool detonateBomb;

    [Header("DEBUG")]
    public List<GameObject> zombies;
    public Trap_ZombieDetector zombieDetector;
    public int zombieListLength;
    public Vector3 landminePosition;
    // Start is called before the first frame update
    void Start()
    {
        detonateBomb = false;
    }

    // Update is called once per frame
    void Update()
    {
        //sets the landmine position to where its placed and updates the zombie list from the Trap_ZombieDetector Script
        landminePosition = transform.position;
        zombieDetector = GetComponent<Trap_ZombieDetector>();
        zombies = zombieDetector.zombiesInTraps;

        //Determines how many zombies are in the List and detonates the bomb
        zombieListLength = zombies.Count;
        if(detonateBomb)
        {
            Detonate();
        }
    }


    //function that detonates the landmine
    public void Detonate()
    {
        //For each of the zombies in the trap's range the explosion affects the zombies each individually
        zombieListLength = Mathf.Abs(zombies.Count);
        for (int i = 0; i < zombieListLength; i++)
        {
            GameObject zombie = zombies[i];
            Rigidbody2D zombieRb = zombie.GetComponent<Rigidbody2D>();
            Debug.Log(i);
            Explode(zombieRb,zombie.transform.position);
        }
        Destroy(gameObject);
    }

    //Explosion function that uses the zombie's velocity and position to create the explosion effect
    public void Explode(Rigidbody2D zombie, Vector3 zombiePosition)
    {
        //Vectors that determine the what direction the zombie will be launched
        Vector3 positionFromTrap = zombiePosition - landminePosition;
        Vector3 launchDirection = positionFromTrap.normalized;

        //Sets the distance the zombie will be knocked to
        Zombie_Exploded zmbPosition = zombie.GetComponent<Zombie_Exploded>();

        zmbPosition.trapPosition = landminePosition;
        zmbPosition.distanceExplodedPositive = zombiePosition + new Vector3(3,3,0);
        zmbPosition.distanceExplodedNegative = zombiePosition + new Vector3(-3, -3, 0);
        zmbPosition.exploded = true;
        
        //sets the velocity and direction the zombies will go
        zombie.velocity = new Vector3(launchDirection.x * explosionStrength, launchDirection.y * explosionStrength, 0);
    }
}
