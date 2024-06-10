using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paperdoll : MonoBehaviour
{
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
