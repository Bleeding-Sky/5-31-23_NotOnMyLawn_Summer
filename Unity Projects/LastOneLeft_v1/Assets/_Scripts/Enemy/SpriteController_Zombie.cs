using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// placed on zombie master object, changes zombie sprites in all children
/// </summary>
public class SpriteController_Zombie : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] SpriteList_Zombie spriteListSO;

    [Header("DEBUG")]
    [SerializeField] List<SpriteData_Zombie> sprites;
    [SerializeField] List<SpriteChanger_Zombie> childrenSpriteChangers;

    //local tracking of recent sprite bools
    public bool local_isHeadless = false;
    public bool local_isOneArm = false;
    public bool local_isArmless = false;
    public bool local_isLegless = false;

    private void Awake()
    {
        sprites = spriteListSO.zombieSpriteList;
    }

    private void Start()
    {
        Refresh();
    }

    /// <summary>
    /// refreshes the sprite renderer list
    /// </summary>
    public void FetchSpriteChangers()
    {
        GetComponentsInChildren(childrenSpriteChangers);
    }

    /// <summary>
    /// tells all children sprite changers to change their sprites.
    /// also updates local bools to reflect most recent sprite info.
    /// </summary>
    /// <param name="isHeadless"></param>
    /// <param name="isOneArm"></param>
    /// <param name="isArmless"></param>
    /// <param name="isLegless"></param>
    public void ActivateSpriteChangers(bool isHeadless, bool isOneArm, bool isArmless, bool isLegless)
    {

        UpdateLocalBools(isHeadless, isOneArm, isArmless, isLegless);

        foreach (SpriteChanger_Zombie spriteChanger in childrenSpriteChangers)
        {
            if (spriteChanger != null)
            {
                spriteChanger.ChangeSprite(sprites, isHeadless, isOneArm, isArmless, isLegless);
            }
            
        }
    }

    /// <summary>
    /// activates all sprite changers to change their sprites to match the locally saved sprite info
    /// </summary>
    private void UpdateSprites()
    {
        ActivateSpriteChangers(local_isHeadless, local_isOneArm, local_isArmless, local_isLegless);
    }

    /// <summary>
    /// updates locally saved bools for sprite info
    /// </summary>
    /// <param name="isHeadless"></param>
    /// <param name="isOneArm"></param>
    /// <param name="isArmless"></param>
    /// <param name="isLegless"></param>
    private void UpdateLocalBools(bool isHeadless, bool isOneArm, bool isArmless, bool isLegless)
    {
        local_isHeadless = isHeadless;
        local_isOneArm = isOneArm;
        local_isArmless = isArmless;
        local_isLegless = isLegless;
    }

    /// <summary>
    /// fetches all sprite renderers in children and updates them with most recent limb status
    /// </summary>
    public void Refresh()
    {
        FetchSpriteChangers();
        UpdateSprites();
    }
    
}
