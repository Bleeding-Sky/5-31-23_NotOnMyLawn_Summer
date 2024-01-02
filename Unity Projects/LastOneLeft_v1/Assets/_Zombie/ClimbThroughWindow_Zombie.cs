using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbThroughWindow_Zombie : MonoBehaviour
{
    public Spawner_Zombie spawnerScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Overhead Window"))
        {
            spawnerScript.SpawnIndoorZombie();
        }
    }
}
