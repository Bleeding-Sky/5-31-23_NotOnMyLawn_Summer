using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Zmb_Window : MonoBehaviour
{
    public Zmb_Master masterScript;
    public Zmb_Tacview tacviewScript;

    public Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MatchTacviewPos();
    }

    void MatchTacviewPos()
    {
        float tacviewXDisplacement = tacviewScript.xDisplacementFromTarget;
        float tacviewDistanceFromTarget = tacviewScript.distanceFromTarget;

        float myXDisplacement = targetPos.x + tacviewXDisplacement;
        float myZDisplacement = targetPos.z + tacviewDistanceFromTarget;

        transform.position = new Vector3(myXDisplacement, targetPos.y, myZDisplacement);
    }

    /// <summary>
    /// sends damage value to master script
    /// </summary>
    /// <param name="damage"></param>
    void WindowDmg(float damage)
    {
        masterScript.DamageZmb(damage);
    }
}
