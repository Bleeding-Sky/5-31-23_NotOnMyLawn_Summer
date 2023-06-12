using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerHealthManager : MonoBehaviour
{
    public PlayerHealth health;
    public float initialHealth;
    // Start is called before the first frame update
    void Start()
    {
        health.health = initialHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Zombie"))
        {
            Debug.Log("Player Hit");
        }
    }
}
