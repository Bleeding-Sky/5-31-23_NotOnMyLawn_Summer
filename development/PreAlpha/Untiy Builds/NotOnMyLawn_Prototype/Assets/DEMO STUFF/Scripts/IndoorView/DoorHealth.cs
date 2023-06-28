using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHealth : MonoBehaviour
{
    public float myHealth = 20;

    private void Update()
    {
        if (myHealth <= 0) Destroy(gameObject);
    }
}
