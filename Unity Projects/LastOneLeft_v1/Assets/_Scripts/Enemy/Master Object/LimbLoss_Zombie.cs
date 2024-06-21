using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(Status_Zombie), typeof(Health_Zombie) )]

public class LimbLoss_Zombie : MonoBehaviour
{

    [Header("CONFIG")]
    [SerializeField] float highHealthHeadBreakEndureChance = 70;
    [SerializeField] float lowHealthHeadBreakEndureChance = 50;

    [SerializeField] bool printDebugMessages = false;

    [Header("DEBUG")]
    [SerializeField] Status_Zombie statusScript;
    [SerializeField] Health_Zombie healthScript;

    private void Awake()
    {
        statusScript = GetComponent<Status_Zombie>();
        healthScript = GetComponent<Health_Zombie>();
    }


    /// <summary>
    /// breaks an arm at half health and at zero health
    /// </summary>
    /// <param name="bodyMaxHealth"></param>
    /// <param name="bodyCurrentHealth"></param>
    public void AttemptArmLoss(float bodyMaxHealth, float bodyCurrentHealth)
    {
        //if body is broken, lose an arm.
        if (bodyCurrentHealth <= 0)
        {
            BreakArm();
        }
        //if body is below half health and both arms are intact, lose an arm
        else if (   bodyCurrentHealth < (bodyMaxHealth / 2) &&
                    statusScript.LArmCondition == LimbCondition.Intact &&
                    statusScript.RArmCondition == LimbCondition.Intact
                )
        {
            BreakArm();
        }
    }

    #region Break Part Methods

    /// <summary>
    /// breaks a limb by changing limb's condition to broken on the status script
    /// </summary>
    /// <param name="limbToBreak"></param>
    void BreakLimb(FodderLimb limbToBreak)
    {
        statusScript.ChangeLimbCondition(limbToBreak, LimbCondition.Broken);
    }

    /// <summary>
    /// breaks the crit region, rolls a chance for the zombie to endure or die
    /// </summary>
    /// <param name="maxHealth"></param>
    /// <param name="currentHealth"></param>
    public void BreakHead(float maxHealth, float currentHealth)
    {
        BreakLimb(FodderLimb.Head);
        AttemptEndureHeadBreak(maxHealth, currentHealth);

    }

    /// <summary>
    /// rolls a chance for the enemy to survive the crit region breaking.
    /// kills the enemy if the roll fails
    /// </summary>
    /// <param name="maxHealth"></param>
    /// <param name="currentHealth"></param>
    private void AttemptEndureHeadBreak(float maxHealth, float currentHealth)
    {
        bool isLowHealth = currentHealth < (maxHealth / 3);
        float endureChance;
        if (isLowHealth)
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
            healthScript.Die();

            if (printDebugMessages) { Debug.Log("Endure Failure"); }
        }
        else
        {
            if (printDebugMessages) { Debug.Log("Endure Success"); }
        }
    }

    /// <summary>
    /// breaks a random arm if no arms are broken. breaks remaining arm if one arm is broken.
    /// </summary>
    public void BreakArm()
    {
        //bools for readability
        bool bothArmsIntact = statusScript.LArmCondition == LimbCondition.Intact && statusScript.RArmCondition == LimbCondition.Intact;
        bool bothArmsBroken = statusScript.LArmCondition == LimbCondition.Broken && statusScript.RArmCondition == LimbCondition.Broken;
        //left arm or right arm broken, but not both
        bool oneArmBroken = statusScript.LArmCondition == LimbCondition.Broken || statusScript.RArmCondition == LimbCondition.Broken
                            && (!bothArmsBroken);

        //break one random arm if no arms broken
        if (bothArmsIntact)
        {
            if (Random.value<0.5f)
            {
                BreakLimb(FodderLimb.LArm);
            }
            else
            {
                BreakLimb(FodderLimb.RArm);
            }
        }
        //break remaining arm if one arm broken
        else if (oneArmBroken)
        {
            if (statusScript.LArmCondition == LimbCondition.Intact)
            {
                BreakLimb(FodderLimb.LArm);
            }
            else if (statusScript.RArmCondition == LimbCondition.Intact)
            {
                BreakLimb(FodderLimb.RArm);
            }
        }

    }

    /// <summary>
    /// breaks legs and applies the crawling status
    /// </summary>
    public void BreakLegs()
    {
        BreakLimb(FodderLimb.Legs);
        statusScript.BreakLegs();
    }
    #endregion


}
