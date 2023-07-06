using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Player_State")]
public class Player_States : ScriptableObject
{
    //This script is used to keep tracks of the player's many states while in motion or combat
    public bool isWalking;
    public int faceDirection;
    public bool gunIsDrawn;
}
