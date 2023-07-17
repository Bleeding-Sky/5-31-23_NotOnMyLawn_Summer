using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zmb_Tacview : MonoBehaviour
{
    public Zmb_Master masterScript;

    public Vector2 targetPos;
    public float xDisplacementFromTarget;
    public float distanceFromTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //TODO: make window read pos from tacview
        CalculateDisplacements();
    }

    void CalculateDisplacements()
    {
        Vector3 myPos = transform.position;
        xDisplacementFromTarget = myPos.x - targetPos.x;
        distanceFromTarget = myPos.y - targetPos.y;
    }

    

    /// <summary>
    /// sends damage value to master script
    /// </summary>
    /// <param name="damage"></param>
    void TacviewDmg(float damage)
    {
        masterScript.DamageZmb(damage);
    }

}
