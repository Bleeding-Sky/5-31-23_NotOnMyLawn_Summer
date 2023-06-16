using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverUiDetection : MonoBehaviour
{
    public TrapPlacableScriptSO trap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        trap.placable = true;
        
    }

    private void OnMouseExit()
    {
        trap.placable = false;
        
    }
}
