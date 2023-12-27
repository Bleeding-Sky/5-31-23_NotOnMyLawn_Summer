using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieData")]
public class Data_Zombie : ScriptableObject
{
    [Header("CONFIG")]
    public float zombieMaxHealth;

    public float headMaxHealth;
    public float bodyMaxHealth;
    public float legMaxHealth;

    public float headDmgMultiplier;
    public float bodyDmgMultiplier;
    public float legDmgMultiplier;

}
