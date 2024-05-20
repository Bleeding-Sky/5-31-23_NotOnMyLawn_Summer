using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Activates/deactivates sets of hitboxes on the zombie. managed by spritechanger
/// </summary>
public class HitboxChanger_Zombie : MonoBehaviour
{
    [Header("CONFIG")]
    public GameObject standingHitboxObject;
    public GameObject standingHeadHitboxObject;
    public GameObject crawlingHitboxObject;
    public GameObject crawlingHeadHitboxObject;

    /// <summary>
    /// changes an object's hitbox to the specified type
    /// </summary>
    /// <param name="hitboxType"></param>
    public void ChangeHitbox(bool isHeadless, bool isLegless)
    {
        GameObject currentHeadObject = null;

        //set body hitbox and save correct head hitbox
        if (isLegless)
        {
            ActivateCrawlingHitbox();
            currentHeadObject = crawlingHeadHitboxObject;
        }
        else if (!isLegless)
        {
            ActivateStandingHitbox();
            currentHeadObject = standingHeadHitboxObject;
        }

        //set head hitbox to be active/inactive based on bool
        UpdateHeadHitbox(isHeadless, currentHeadObject);

    }

    private static void UpdateHeadHitbox(bool isHeadless, GameObject currentHeadObject)
    {
        if (isHeadless)
        {
            currentHeadObject.SetActive(false);
        }
        else if (!isHeadless)
        {
            currentHeadObject.SetActive(true);
        }
    }

    /// <summary>
    /// activates standing hitbox, deactivates crawling hitbox
    /// </summary>
    void ActivateStandingHitbox()
    {
        standingHitboxObject.SetActive(true);
        crawlingHitboxObject.SetActive(false);
    }

    /// <summary>
    /// activates crawling hitbox, deactivates standing hitbox
    /// </summary>
    void ActivateCrawlingHitbox()
    {
        crawlingHitboxObject.SetActive(true);
        standingHitboxObject.SetActive(false);
    }

}
