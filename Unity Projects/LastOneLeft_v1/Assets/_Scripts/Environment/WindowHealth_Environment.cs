using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowHealth_Environment : MonoBehaviour
{
    public bool windowBoarded;

    [Header("Window Statistics")]
    public float windowHealth;
    public float maxHealth;
    public float rebuildTime;
    public float destroyTime;
    public float zomMultiplier;
    public List<GameObject> ZombiesInRange;

    // Start is called before the first frame update
    void Start()
    {
        windowHealth = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (windowHealth > 0)
        {
            windowBoarded = true;
        }
        else if(windowHealth <= 0)
        {
            windowBoarded = false;
        }
    }

    public void AddHealth()
    {
        if (windowHealth < maxHealth)
        {
            windowHealth += rebuildTime * Time.deltaTime;
        }
        else if (windowHealth >= maxHealth)
        {
            windowHealth = maxHealth;
        }
        
    }

    public void SubtractHealth()
    {
        float zombiesInRange = ZombiesInRange.Count;
        if(windowHealth > 0)
        {
            windowHealth -= (destroyTime * (zomMultiplier + (zombiesInRange/5))) * Time.deltaTime; 
            Debug.Log((destroyTime * (zomMultiplier + (zombiesInRange / 5))) * Time.deltaTime);
        }
        else if(windowHealth <= 0)
        {
            windowHealth = 0;
            windowBoarded = false;
        }
    }


}
