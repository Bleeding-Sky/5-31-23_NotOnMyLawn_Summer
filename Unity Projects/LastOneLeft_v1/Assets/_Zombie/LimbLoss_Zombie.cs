using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbLoss_Zombie : MonoBehaviour
{

    [Header("CONFIG")]
    [SerializeField] float armLossChance = 20;
    [SerializeField] float bothArmsLossChance = 50;
    [SerializeField] float legBreakChance = 60;
    [SerializeField] float headBreakChance = 50;
    [SerializeField] float highHealthHeadBreakEndureChance = 70;
    [SerializeField] float lowHealthHeadBreakEndureChance = 50;

    [Header("DEBUG")]
    [SerializeField] Status_Zombie statusScript;
    [SerializeField] Health_Zombie healthScript;
    public bool headBroken = false;
    public bool oneArmLost = false;
    public bool bothArmsLost = false;
    public bool legsBroken = false;

    private void Awake()
    {
        statusScript = GetComponent<Status_Zombie>();
        healthScript = GetComponent<Health_Zombie>();
    }


    /// <summary>
    /// rolls to break the zombie's head if its head health is at 0
    /// </summary>
    /// <param name="maxHealth"></param>
    /// <param name="currentHealth"></param>
    public void AttemptHeadBreak(float maxHealth, float currentHealth)
    {
        if (RNGRolls_System.RollUnder(headBreakChance))
        {
            BreakHead(maxHealth, currentHealth);
        }

    }

    /// <summary>
    /// rolls to break a zombie's arm on torso damage
    /// </summary>
    /// <param name="bodyHealth"></param>
    public void AttemptArmLoss(float bodyHealth)
    {
        float breakChance;
        if (bodyHealth > 0)
        {
            breakChance = armLossChance;
        }
        else
        {
            breakChance = bothArmsLossChance;
        }

        if (RNGRolls_System.RollUnder(breakChance))
        {
            LoseArm();
        }
    }

    /// <summary>
    /// rolls to break zombie's legs if its legs health is at 0
    /// </summary>
    public void AttemptLegBreak()
    {
        if (RNGRolls_System.RollUnder(legBreakChance))
        {
            BreakLegs();
        }
    }


    public void BreakHead(float maxHealth, float currentHealth)
    {
        headBroken = true;
        AttemptEndureHeadBreak(maxHealth, currentHealth);
    }

    private void AttemptEndureHeadBreak(float maxHealth, float currentHealth)
    {
        bool underHalfHealth = currentHealth < (maxHealth / 2);
        float endureChance = 0;
        if (underHalfHealth)
        {
            endureChance = lowHealthHeadBreakEndureChance;
        }
        else
        {
            endureChance = highHealthHeadBreakEndureChance;
        }

        //roll for surviving the head break
        //on failure, zombie dies
        if (!RNGRolls_System.RollUnder(endureChance))
        {
            healthScript.currentHealth = 0;
        }
    }

    public void LoseArm()
    {
        if (oneArmLost)
        {
            bothArmsLost = true;
        }
        else if (!oneArmLost)
        {
            oneArmLost = true;
        }
    }

    public void BreakLegs()
    {
        legsBroken = true;
        statusScript.isCrawling = true;
    }

}
