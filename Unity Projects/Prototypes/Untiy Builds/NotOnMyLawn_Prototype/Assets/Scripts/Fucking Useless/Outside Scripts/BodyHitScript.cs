using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHitScript : MonoBehaviour
{
    ZombieMovement ZombieHealth;
    // Start is called before the first frame update
    void Start()
    {
        ZombieHealth = GameObject.FindGameObjectWithTag("Zombie").GetComponent<ZombieMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Debug.Log("Body Hit");
        ZombieHealth.KillZombie();
    }
}
