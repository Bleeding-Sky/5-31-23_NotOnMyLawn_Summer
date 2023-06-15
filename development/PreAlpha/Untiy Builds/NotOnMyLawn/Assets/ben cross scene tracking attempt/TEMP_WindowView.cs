using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_WindowView : MonoBehaviour
{

    public float zmbFarScale = 1;
    public float zmbWindowScale = 5.22f;
    public float zmbFarYPos = 2.3f;
    public float zmbWindowYPos = .08f;

    public float windowRange;
    public float scaleDiff;
    public float posDiff;

    // Start is called before the first frame update
    void Start()
    {
        scaleDiff = zmbFarScale - zmbWindowScale;
        posDiff = zmbFarYPos - zmbWindowYPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
