using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// holds damage calues for TESTGUN
/// </summary>
public class testgunBulletData : bulletData
{
    
    void Start()
    {
        //fetch vals for this specific gun from SO

        //float fields below are inherited from bullet data script,
        //each bullet script gets its own SPECIFIC values from the SO
        headDmg = dmgValSO.testHeadDmg;
        bodyDmg = dmgValSO.testBodyDmg;
        legDmg = dmgValSO.testLegDmg;
    }
}
