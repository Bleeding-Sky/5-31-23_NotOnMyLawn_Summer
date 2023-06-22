using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_IndoorZombieHealth : MonoBehaviour
{

    [Header("CONFIG")]
    public float currentHealth;
    public float maxHealth = 5;
    public float headshotDmg;
    public float bodyshotDmg;
    public float legshotDmg;
    public float headshotPoints;
    public float bodyshotPoints;
    public float legshotPoints;

    [Header("DEBUG")]
    public PointTracker points;
    public Vector3 bloodTransform;
    public GameObject blood;

    public float directionFacing;
    public int bloodDirection;
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        directionFacing = gameObject.transform.localScale.x;
        if(directionFacing > 0)
        {
            bloodDirection = -90;
        }   
        else if(directionFacing < 0)
        {
            bloodDirection = 90;
        }
        bloodTransform = transform.position;
    }

    public void Headshot()
    {
        currentHealth -= headshotDmg;
        points.points += headshotPoints;
        Instantiate(blood, bloodTransform, Quaternion.Euler(0,bloodDirection,0));
    }

    public void Bodyshot()
    {
        currentHealth -= bodyshotDmg;
        points.points += bodyshotPoints;
        Instantiate(blood, bloodTransform, Quaternion.Euler(0, bloodDirection, 0));
    }

    public void Legshot()
    {
        currentHealth -= legshotDmg;
        points.points += legshotPoints;
        Instantiate(blood, bloodTransform, Quaternion.Euler(0, bloodDirection, 0));
    }

}
