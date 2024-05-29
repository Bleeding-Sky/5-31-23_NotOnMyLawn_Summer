using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(  typeof(Status_Zombie), 
                    typeof(Health_Zombie), 
                    typeof(SpriteController_Zombie))]

public class LimbLoss_Zombie : MonoBehaviour
{

    [Header("CONFIG")]
    //chance out of 100 that the limb will break when it takes damage at 0 limb health
    //[SerializeField] float armLossChance = 20;
    //[SerializeField] float bothArmsLossChance = 50;
    //[SerializeField] float legBreakChance = 60;
    //[SerializeField] float headBreakChance = 50;
    [SerializeField] float highHealthHeadBreakEndureChance = 70;
    [SerializeField] float lowHealthHeadBreakEndureChance = 50;

    [SerializeField] bool printDebugMessages = false;

    [Header("DEBUG")]
    [SerializeField] Status_Zombie statusScript;
    [SerializeField] Health_Zombie healthScript;
    [SerializeField] SpriteController_Zombie spriteController;
    [SerializeField] bool headless = false;
    public bool oneArmBroken = false;
    public bool armless = false;
    public bool legless = false;

    private void Awake()
    {
        statusScript = GetComponent<Status_Zombie>();
        healthScript = GetComponent<Health_Zombie>();
        spriteController = GetComponent<SpriteController_Zombie>();
    }

    private void Start()
    {
        ChangeSprite();
    }


    #region Attempt Part Break Methods (Deprecated)
    //no longer necessary as limbs break when at 0 health all the time now
    /*
    /// <summary>
    /// rolls to break the zombie's head if its head health is at 0
    /// </summary>
    /// <param name="maxHealth"></param>
    /// <param name="currentHealth"></param>
    public void AttemptCritBreak(float maxHealth, float currentHealth)
    {

        if (RNGRolls_System.RollUnder(headBreakChance))
        {
            BreakCritRegion(maxHealth, currentHealth);

            if (printDebugMessages) { Debug.Log("Head Break Success"); }
        }
        else
        {
            if (printDebugMessages) { Debug.Log("Head Break Failure"); }
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

            if (printDebugMessages) { Debug.Log("Break Arm Success"); }
        }
        else
        {
            if (printDebugMessages) { Debug.Log("Break Arm Failure"); }
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

            if (printDebugMessages) { Debug.Log("Break Legs Success"); }
        }
        else
        {
            if (printDebugMessages) { Debug.Log("Break Legs Failure"); }
        }
    }
    */

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
            LoseArm();
        }

        //if body is below half health and both arms are intact, lose an arm
        else if (   bodyCurrentHealth < (bodyMaxHealth / 2) &&
                    !armless && !oneArmBroken
                )
        {
            LoseArm();
        }
    }
    #endregion

    #region Break Part Methods

    /// <summary>
    /// breaks the crit region, rolls a chance for the zombie to endure or die,
    /// and refreshes sprites in child objects
    /// </summary>
    /// <param name="maxHealth"></param>
    /// <param name="currentHealth"></param>
    public void BreakCritRegion(float maxHealth, float currentHealth)
    {
        headless = true;
        AttemptEndureHeadBreak(maxHealth, currentHealth);

        ChangeSprite();
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
        float endureChance = 0;
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
    /// breaks an arm if no arms are broken. breaks other arm if one arm is broken.
    /// also refreshes sprites of child objects
    /// </summary>
    public void LoseArm()
    {
        //break one arm if no arms broken, break other arm if one arm broken
        if (!armless)
        {
            if (oneArmBroken)
            {
                armless = true;
                oneArmBroken = false;
            }
            else if (!oneArmBroken)
            {
                oneArmBroken = true;
            }
        }

        ChangeSprite();
    }

    public void BreakLegs()
    {
        legless = true;
        statusScript.DoCrawl();

        ChangeSprite();
    }
    #endregion

    /// <summary>
    /// calls method on a spritecontroller to change sprite according to limb status booleans
    /// </summary>
    public void ChangeSprite()
    {
        spriteController.ActivateSpriteChangers(headless, oneArmBroken, armless, legless);
    }

}
