using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class IndoorZombie_PlayerDetection : MonoBehaviour
{

    public float zombieViewRadius;
    [Range(0,360)]
    public float zomViewAngle;
    
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad),0);
    }
}
