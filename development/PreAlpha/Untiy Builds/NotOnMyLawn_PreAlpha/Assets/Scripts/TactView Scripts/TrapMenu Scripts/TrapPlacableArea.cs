using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlacableArea : MonoBehaviour
{
    public TrapPlacable_SO PlacableArea;

    private void OnMouseEnter()
    {
        PlacableArea.placable = true;
    }

    private void OnMouseExit()
    {
        PlacableArea.placable = false;
    }
}
