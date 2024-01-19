using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSprite")]
public class SpriteData_Zombie : ScriptableObject
{
    [Header("CONFIG")]
    public Sprite sprite;
    public bool headless;
    public bool oneArm;
    public bool armless;
    public bool legless;

}
