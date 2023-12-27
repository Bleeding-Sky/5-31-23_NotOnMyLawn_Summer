using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Shotgun_Item))]
public class ShotgunSpread_Item : Editor
{
    void OnSceneGUI()
    {
        Shotgun_Item fow = (Shotgun_Item)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.right, 360, fow.shotRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.shotSpread / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.shotSpread / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.shotRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.shotRadius);

    }
}
