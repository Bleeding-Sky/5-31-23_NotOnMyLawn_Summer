using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragabbleItem : MonoBehaviour, IBeginDragHandler, 
    IDragHandler, IEndDragHandler
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public TrapPlacableScriptSO trap;
    Transform parentAfterDrag;


    public RectTransform trapSlotPosition;

    public GameObject landMine;
    void Start()
    {
       
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        screenPosition = Input.mousePosition;
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(trap.placable == false)
        {
            transform.SetParent(parentAfterDrag);

            transform.position = trapSlotPosition.position;
        }
        else if(trap.placable == true)
        {
            Instantiate(landMine, worldPosition, Quaternion.identity);
            Destroy(gameObject);
        }
      
    }

   
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }

}
