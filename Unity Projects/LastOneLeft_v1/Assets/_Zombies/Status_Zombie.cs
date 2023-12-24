using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Zombie : MonoBehaviour
{

    //statuses
    //DO NOT EDIT THESE DIRECTLY- PLEASE USE METHODS BELOW
    public bool isStumbling = false;
    public bool isStunned = false;
    public bool isCrawling = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region status methods
    /// <summary>
    /// applies the stumble status
    /// </summary>
    public void DoStumble()
    {
        isStumbling = true;
    }

    /// <summary>
    /// removes the stumble status
    /// </summary>
    public void StopStumble()
    {
        isStumbling = false;
    }

    /// <summary>
    /// applies the stun status
    /// </summary>
    public void DoStun()
    {
        isStunned = true;
    }

    /// <summary>
    /// removes the stun status
    /// </summary>
    public void StopStun()
    {
        isStunned = false;
    }

    /// <summary>
    /// applies the crawling status
    /// </summary>
    public void DoCrawl()
    {
        isCrawling = true;
    }

    /// <summary>
    /// removes the crawling status
    /// </summary>
    public void StopCrawl()
    {
        isCrawling = false;
    }
    #endregion

}
