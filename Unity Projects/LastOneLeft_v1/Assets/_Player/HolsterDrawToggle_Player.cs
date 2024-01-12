using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// checks holster input and draws/holsters gun accordingly
/// </summary>
public class HolsterDrawToggle_Player : MonoBehaviour
{
    public States_Player playerStates;


    private void HolsterGun()
    {
        playerStates.gunIsDrawn = false;
    }

    private void DrawGun()
    {
        playerStates.gunIsDrawn = true;
    }

    public void OnHolsterDraw(InputAction.CallbackContext actionContext)
    {
        if (actionContext.performed)
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

}
