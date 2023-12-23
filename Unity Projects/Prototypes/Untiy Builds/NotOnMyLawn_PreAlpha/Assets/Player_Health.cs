using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    /*
     * Players health script that will be used to damage,
     * heal, and kill the player depending on the health
     * and outside sources
     */
    [Header("CONFIG")]
    public int health;

    /// <summary>
    /// Hurts the player's health depending on how much strength the enemy or trap has
    /// </summary>
    /// <param name="damage"></param>
    public void DamagePlayer(int damage)
    {
        health -= damage;
    }
}
