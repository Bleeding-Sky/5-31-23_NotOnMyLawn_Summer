using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RNGRolls_System 
{

    public static bool RollUnder(float successCutoff)
    {
        bool isSuccess = false;
        float rngValue = Random.Range(0, 100);
        if (rngValue < successCutoff) { isSuccess = true; }
        return isSuccess;
    }
}
