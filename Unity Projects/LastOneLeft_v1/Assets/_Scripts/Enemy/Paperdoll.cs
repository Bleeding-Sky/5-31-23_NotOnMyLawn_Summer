using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the opacity of a sprite that only appears in the idoor view.
/// The sprite gradually becomes more opaque as the zombie approaches the window.
/// </summary>
public class Paperdoll : MonoBehaviour
{
    //NOTE: all sprite object on the window zombie should be tagged as WINDOW
    //(except for this one, this should be tagged as Paperdoll Enemies)
    public Transform overheadZombieTransform;
    public Transform overheadTargetTransform;

    [SerializeField] float currentDistance;
    [SerializeField] float beginAppearDistance;
    [SerializeField] float fullyVisibleDistance;

    public float alphaPercent;
    public float alphaVal;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject overheadObject = GetComponentInParent<PositionSync_Window>().overheadTrackerScript.gameObject;
        overheadZombieTransform = overheadObject.transform;
        overheadTargetTransform = overheadObject.GetComponent<OverheadPathing_Zombie>().target;
    }

    private void Update()
    {
        //increases the sprites alpha as the overhead zombie approaches the window
        currentDistance = Vector3.Distance(overheadZombieTransform.position, overheadTargetTransform.position);
        if (currentDistance < beginAppearDistance)
        {
            alphaPercent = (beginAppearDistance - currentDistance) / (beginAppearDistance - fullyVisibleDistance);

            alphaVal = alphaPercent * 255;

            Color spriteColor = spriteRenderer.color;
            spriteColor.a = alphaPercent;
            spriteRenderer.color = spriteColor;

        }
    }
}
