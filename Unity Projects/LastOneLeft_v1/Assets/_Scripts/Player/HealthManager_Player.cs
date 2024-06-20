using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager_Player : MonoBehaviour
{
    [SerializeField]private int health;
    
    public void AddHealth(int amount)
    {
        health += amount;
    }

    public void DecreaseHealth(int amount)
    {
        health -= amount;
    }    
}
