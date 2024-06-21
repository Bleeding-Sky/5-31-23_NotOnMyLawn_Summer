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

    [Header("DEBUG")]
    [SerializeField] Status_Zombie parentStatusScript;

    private void Start()
    {
        parentStatusScript = GetComponentInParent<Status_Zombie>();
    }

    /// <summary>
    /// changes an object's hitbox to the specified type
    /// </summary>
    /// <param name="hitboxType"></param>
    public void ChangeHitbox(LimbCondition headCondition, LimbCondition legCondition)
    {
        GameObject currentHeadObject = null;

        //set body hitbox and save correct head hitbox
        if (legCondition == LimbCondition.Broken)
        {
            ActivateCrawlingHitbox();
            currentHeadObject = crawlingHeadHitboxObject;
        }
        else if (legCondition == LimbCondition.Intact)
        {
            ActivateStandingHitbox();
            currentHeadObject = standingHeadHitboxObject;
        }

        //set head hitbox to be active/inactive based on bool
        UpdateHeadHitbox(headCondition, currentHeadObject);

    }

    private static void UpdateHeadHitbox(LimbCondition headCondition, GameObject currentHeadObject)
    {
        if (headCondition == LimbCondition.Broken)
        {
            currentHeadObject.SetActive(false);
        }
        else if (headCondition == LimbCondition.Intact)
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
