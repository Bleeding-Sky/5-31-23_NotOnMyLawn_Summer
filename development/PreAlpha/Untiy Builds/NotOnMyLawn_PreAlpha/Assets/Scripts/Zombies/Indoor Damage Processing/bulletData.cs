using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class bulletData : MonoBehaviour
{

    public float headDmg;
    public float bodyDmg;
    public float legDmg;

    public bulletDmgValues dmgValSO;
}
