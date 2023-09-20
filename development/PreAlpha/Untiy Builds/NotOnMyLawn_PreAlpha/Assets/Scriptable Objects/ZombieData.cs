using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Zombie_Data")]

/// <summary>
/// holds all default zombie values. includes health and damage
/// </summary>
public class ZombieData : ScriptableObject
{

    public float zombieHealth;

    public float headMaxHealth;
    public float bodyMaxHealth;
    public float legMaxHealth;

    public float headDmgMultiplier;
    public float bodyDmgMultiplier;
    public float legDmgMultiplier;

}
