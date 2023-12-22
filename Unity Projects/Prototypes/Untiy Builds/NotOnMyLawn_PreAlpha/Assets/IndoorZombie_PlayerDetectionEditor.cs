using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (IndoorZombie_PlayerDetection))]
public class IndoorZombie_PlayerDetectionEditor : Editor
{
    void OnSceneGUI()
    {
        IndoorZombie_PlayerDetection fow = (IndoorZombie_PlayerDetection)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.right, 360, fow.zombieViewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.zomViewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.zomViewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.zombieViewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.zombieViewRadius);

    }
}
