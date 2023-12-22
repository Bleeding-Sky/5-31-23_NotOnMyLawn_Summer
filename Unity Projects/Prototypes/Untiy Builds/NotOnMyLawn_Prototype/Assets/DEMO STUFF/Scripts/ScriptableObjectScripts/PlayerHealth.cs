using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Player Health")]
public class PlayerHealth : ScriptableObject
{
    public float health;
    public float addHealth;

    void increaseHealth()
    {
        health += addHealth;
    }

}
