using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Communicates damage to window view zombie to tacview counterpart
/// </summary>
public class ZmbDamageManager : MonoBehaviour
{
    [Header("CONFIG")]
    public float headshotDmg;
    public float bodyshotDmg;
    public float legshotDmg;
    public float headshotPoints;
    public float bodyshotPoints;
    public float legshotPoints;

    [Header("DEBUG")]
    public TopviewZmbHealth healthScript;
    public PointTracker points;

    private void Start()
    {
        //get health script from tacview counterpart
        //NOTE: make this not bad
        healthScript =  GetComponent<TEMP_WindowViewZombie>().tacviewCounterpart
                        .GetComponent<TopviewZmbHealth>(); 
    }

    public void Headshot()
    {
        healthScript.currentHealth -= headshotDmg;
        points.points += headshotPoints;
    }

    public void Bodyshot()
    {
        healthScript.currentHealth -= bodyshotDmg;
        points.points += bodyshotPoints;
    }

    public void Legshot()
    {
        healthScript.currentHealth -= legshotDmg;
        points.points += legshotPoints;
    }

}
