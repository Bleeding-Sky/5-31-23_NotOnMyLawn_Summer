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
    public WindowHealth_Environment windowHealth;

    private void Start()
    {
        windowHealth = GetComponent<WindowHealth_Environment>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            windowHealth.ZombiesInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            
            //call EnterBuilding on master script to spawn zombie indoors
            if (windowHealth.windowHealth > 0)
            {
                windowHealth.SubtractHealth();
            }
            else
            {
                EnterBuilding_Zombie entering = collision.gameObject.GetComponentInParent<EnterBuilding_Zombie>();
                if (!entering.isEnteringBuilding)
                {
                    entering.StartEnterBuilding(indoorWindowTransform);
                    for (int i = 0; i < windowHealth.ZombiesInRange.Count; i++)
                    {
                        if (collision.gameObject == windowHealth.ZombiesInRange[i])
                        {
                            windowHealth.ZombiesInRange.Remove(collision.gameObject);
                        }
                    }

                }
            }

        }
    }
}
