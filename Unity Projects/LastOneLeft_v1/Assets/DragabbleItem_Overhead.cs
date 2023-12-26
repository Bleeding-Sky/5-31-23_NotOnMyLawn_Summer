using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragabbleItem_Overhead : MonoBehaviour, IBeginDragHandler,
    IDragHandler, IEndDragHandler
{
    [Header("CONFIG")]
    public TrapItem_Overhead TrapInfo;
    public TrapPlacability_Overhead trapPlacability;
    private GameObject Trap;
    public RectTransform trapSlotPosition;
    public Text trapCostText;
    [Header("DEBUG")]
    public int trapCost;
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        Trap = TrapInfo.Trap;
        trapCost = TrapInfo.TrapCost;
        trapCostText.text = trapCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Event system that detects when the item is beggining to be dragged
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        //This sets the objects parent to a variable to reset it later 
        parent = transform.parent;
        transform.SetParent(transform.root);
        //Puts the trap as the last object in the heir archy
        //in order to make it visible not matter where on the screen it is
        transform.SetAsLastSibling();
    }

    /// <summary>
    /// Event system that detects whether the object is being dragged
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        //takes the mouse's position on screen and sets the
        //position of the trap to that of the mouse
        screenPosition = Input.mousePosition;
        transform.position = screenPosition;

        //sets the world position to the in game position your mouse is over
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;
    }

    /// <summary>
    /// Event system that detects when the player lets go of the trap
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (trapPlacability.placable)
        {
            //Sets the traps parent back to the Trap Slot
            //and makes the traps posiiton the Trap Slot's
            transform.SetParent(parent);
            transform.position = trapSlotPosition.position;
            //creates the trap on the players mouse position
            Instantiate(Trap, worldPosition, Quaternion.identity);
        }
        else if (!trapPlacability.placable)
        {
            //if the trap isn't placable it doesnt let the player create a trap
            transform.SetParent(parent);
            transform.position = trapSlotPosition.position;
        }
    }
}
