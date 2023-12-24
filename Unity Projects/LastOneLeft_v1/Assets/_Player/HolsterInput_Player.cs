using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// checks holster input and draws/holsters gun accordingly
/// </summary>
public class HolsterInput_Player : MonoBehaviour
{
    public States_Player playerStates;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //holster gun if drawn
            if (playerStates.gunIsDrawn)
            {
                HolsterGun();
            }
            //draw gun if not drawn
            else if (!playerStates.gunIsDrawn)
            {
                DrawGun();
            }
        }
    }

    private void HolsterGun()
    {
        playerStates.gunIsDrawn = false;
    }

    private void DrawGun()
    {
        playerStates.gunIsDrawn = true;
    }
}
