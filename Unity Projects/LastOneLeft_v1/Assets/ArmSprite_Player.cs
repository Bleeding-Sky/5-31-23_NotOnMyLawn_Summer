using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSprite_Player : MonoBehaviour
{
    public List<Vector3> ArmPositions;
    public ArmRotation_Player Arm;
    public int armIndex;
    public Vector3 currentArmPosition;

    public GameObject sprite;
    public GameObject bone1;
    public GameObject bone2;
    public GameObject bone3;
    private void Start()
    {
        currentArmPosition = ArmPositions[0];
    }
    void Update()
    {
        DecideCurrentIndex();
        float direction = sprite.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        if(direction > 0)
        {
            sprite.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            sprite.transform.localScale = new Vector3(1, 1, 1);
        }

        if(ArmPositions[armIndex] != currentArmPosition)
        {
            currentArmPosition = ArmPositions[armIndex];

            bone1.transform.localRotation = Quaternion.Euler(0, 0, ArmPositions[armIndex].x);
            bone2.transform.localRotation = Quaternion.Euler(0, 0, ArmPositions[armIndex].y);
            bone3.transform.localRotation = Quaternion.Euler(0, 0, ArmPositions[armIndex].z);
        }    
    }

    public void DecideCurrentIndex()
    {
        float armAngle = Arm.itemRotation + 180;
        int angleIndex = (int)(armAngle / 36);
        armIndex = angleIndex;

        Debug.Log(armIndex);
    }
}
