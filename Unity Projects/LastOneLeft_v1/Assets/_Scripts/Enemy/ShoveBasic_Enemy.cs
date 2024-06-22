using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoveBasic_Enemy : Shove_Enemy
{
    public Status_Zombie statusScript;
    public Behavior_Zombie behaviorScript;
    private void Start()
    {
        statusScript = GetComponentInParent<Status_Zombie>();
        behaviorScript = GetComponent<Behavior_Zombie>();
    }
    public override void Shove()
    {
        StartCoroutine(ShovedCoolDown());
    }

    public IEnumerator ShovedCoolDown()
    {
        stunned = true;
        behaviorScript.playerStates.isGrappled = false;
        yield return new WaitForSeconds(1.5f);
        stunned = false;
        shoved = false;
    }
}
