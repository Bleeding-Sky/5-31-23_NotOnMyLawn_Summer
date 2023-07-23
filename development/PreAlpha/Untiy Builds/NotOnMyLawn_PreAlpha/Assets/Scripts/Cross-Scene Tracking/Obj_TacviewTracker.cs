using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class Obj_TacviewTracker : MonoBehaviour
{
    [Header("CONFIG")]  
    //OPTIONAL: script can read position of anchor object and set anchorPos accordingly
    public GameObject anchorObject;
    //object can ALSO be configured with just an anchor position
    public Vector2 anchorPos;

    public GameObject wndwviewPrefab;

    [Header("DEBUG")]
    public float xDisplacementFromTarget;
    public float distanceFromTarget;

    private void Start()
    {
        //initialize anchorPos if an anchor object is given
        if (anchorObject != null)
        {
            anchorPos = anchorObject.transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateDisplacements();
    }

    void CalculateDisplacements()
    {
        Vector3 myPos = transform.position;
        xDisplacementFromTarget = myPos.x - anchorPos.x;
        distanceFromTarget = myPos.y - anchorPos.y;
    }

}
