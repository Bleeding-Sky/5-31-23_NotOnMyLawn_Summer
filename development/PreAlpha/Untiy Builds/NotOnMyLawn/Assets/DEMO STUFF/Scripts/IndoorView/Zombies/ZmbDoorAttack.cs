using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZmbDoorAttack : MonoBehaviour
{
    //damage dealt to door per second
    public float damage = 1;
    public DoorHealth doorHealthScript;
    public TEMP_InteractableStates doorStateScript;

    private void Update()
    {
        
        if (doorHealthScript != null)
        {
            if (!doorStateScript.isActivated)
            {

                doorHealthScript.myHealth -= (damage * Time.deltaTime);
                Debug.Log("Door health = "+ doorHealthScript.myHealth);
            }
            
        }
    }

    /// <summary>
    /// when approaching door, register door 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            doorHealthScript = collision.gameObject.GetComponent<DoorHealth>();
            doorStateScript = collision.gameObject.GetComponent<TEMP_InteractableStates>();
        }
    }

    //when leaving door, deregister door
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            doorHealthScript = null;
            doorStateScript = null;
        }
    }

}
