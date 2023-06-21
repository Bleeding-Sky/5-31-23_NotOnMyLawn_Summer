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

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Headshot()
    {
        currentHealth -= headshotDmg;
        points.points += headshotPoints;
    }

    public void Bodyshot()
    {
        currentHealth -= bodyshotDmg;
        points.points += bodyshotPoints;
    }

    public void Legshot()
    {
        currentHealth -= legshotDmg;
        points.points += legshotPoints;
    }

}
