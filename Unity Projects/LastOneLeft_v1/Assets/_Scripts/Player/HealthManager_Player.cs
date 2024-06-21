using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager_Player : MonoBehaviour
{
    [SerializeField]private int health;

    #region Edit Health
    /// <summary>
    /// Add health to the players total health bar
    /// </summary>
    /// <param name="amount"></param>
    public void AddHealth(int amount)
    {
        health += amount;
    }

    /// <summary>
    /// Decrease health to the players total health bar
    /// </summary>
    /// <param name="amount"></param>
    public void DecreaseHealth(int amount)
    {
        health -= amount;
        CheckDeath();
    }
    #endregion
    #region Health Checkers
    /// <summary>
    /// check if the player has lost enough health to die
    /// </summary>
    public void CheckDeath()
    {
        if(health <= 0)
        {
            Debug.Log("Player is Dead");
        }
        else
        {
            Debug.Log("The current health is "+ health);
        }
    }
    #endregion
}
