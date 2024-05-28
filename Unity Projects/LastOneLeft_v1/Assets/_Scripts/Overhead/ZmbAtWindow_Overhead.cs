using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// attached to a window in the overhead view. detects if a zombie has reached the window, and
/// calls upon it's master script to move it indoors.
/// </summary>
public class ZmbAtWindow_Overhead : MonoBehaviour
{
    [Header("CONFIG")]
    public Transform indoorWindowTransform;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            //call EnterBuilding on master script to spawn zombie indoors
            collision.gameObject.GetComponentInParent<EnterBuilding_Zombie>().StartEnterBuilding(indoorWindowTransform);
        }
    }
}
