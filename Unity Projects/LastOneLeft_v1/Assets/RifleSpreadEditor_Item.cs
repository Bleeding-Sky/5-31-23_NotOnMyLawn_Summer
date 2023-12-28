using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Rifle_Item))]
public class RifleSpreadEditor_Item : Editor
{

    void OnSceneGUI()
    {
        Rifle_Item fow = (Rifle_Item)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.firingPoint.transform.position, Vector3.forward, Vector3.right, 360, fow.shotRadius);
        Vector3 viewAngleA = fow.DirFromAngle((-fow.shotSpread / 2) + fow.coneDirection, false);
        Vector3 viewAngleB = fow.DirFromAngle((fow.shotSpread / 2) + fow.coneDirection, false);

        Handles.DrawLine(fow.firingPoint.transform.position, fow.firingPoint.transform.position + viewAngleA * fow.shotRadius);
        Handles.DrawLine(fow.firingPoint.transform.position, fow.firingPoint.transform.position + viewAngleB * fow.shotRadius);

    }
}
