using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class PropShake : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] float hardShakeSpeedRequirement = 1.3f;
    [SerializeField] float softShakeTime = .4f;
    [SerializeField] float hardShakeTime = .6f;

    [Header("DEBUG")]
    [SerializeField] Collider2D shakeTrigger;
    [SerializeField] bool isShaking = false;
    [SerializeField] ShakeStrengthEnum shakeStrength;
    [SerializeField] int propShakerLayer;
    [SerializeField] float shakeTimeRemaining;

    enum ShakeStrengthEnum { Soft, Hard };


    private void Awake()
    {
        shakeTrigger = GetComponent<Collider2D>();
        shakeTrigger.isTrigger = true;
    }

    private void Start()
    {
        propShakerLayer = LayerMask.NameToLayer("Prop Shaker");
    }

    private void Update()
    {
        if (isShaking)
        {
            shakeTimeRemaining -= Time.deltaTime;
            if (shakeTimeRemaining <= 0) { isShaking = false; }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int collisionLayer = collision.gameObject.layer;
        if (collisionLayer == propShakerLayer)
        {
            //start shaking if the prop is undisturbed
            if (!isShaking)
            {
                isShaking = true;
                float collisionVelocity = Mathf.Abs( collision.GetComponentInParent<Rigidbody2D>().velocity.x );
                
                //shake softly
                if (collisionVelocity < hardShakeSpeedRequirement)
                {
                    shakeStrength = ShakeStrengthEnum.Soft;
                    shakeTimeRemaining = softShakeTime;
                }
                //shake hard
                else
                {
                    shakeStrength = ShakeStrengthEnum.Hard;
                    shakeTimeRemaining = hardShakeTime;
                }
            }
            //if prop is shaking softly, escalate to a hard shake
            else if (isShaking && shakeStrength == ShakeStrengthEnum.Hard)
            {
                shakeStrength = ShakeStrengthEnum.Hard;
                shakeTimeRemaining = hardShakeTime;
            }
        }
    }



}
