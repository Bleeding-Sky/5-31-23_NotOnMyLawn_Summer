using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Aiming : MonoBehaviour
{
    public GameObject gunFiringArea;
    public Vector3 gunScale;
    public GameObject Test;

    public float randomX;
    public float randomY;
    public float yMaxLimit;

    public GameObject circleObject;
    public Player_States PlayerState;

    public float maxCircleSize;
    public float minCircleSize;
    public float circleSize;
    public float circleGrowthRate;

    public Vector3 firingPos;
    // Start is called before the first frame update
    void Start()
    {
        maxCircleSize = 6f;
        minCircleSize = .5f;
        circleSize = minCircleSize;
    }

    // Update is called once per frame
    void Update()
    {
        gunScale = gunFiringArea.transform.localScale;

        followMouse();
        DetermineCircleSize();
        calculateShootingPosition();
    }

    private void calculateShootingPosition()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            float r = .5f;
            randomX = Random.Range(-.5f, .5f);
            yMaxLimit = Mathf.Sqrt((r * r) - (randomX * randomX));
            randomY = Random.Range(-yMaxLimit, yMaxLimit);

            transform.localPosition = new Vector2(randomX, randomY);
            firingPos = transform.position;
        }
    }

    private void followMouse()
    {
        Vector3 circlePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        circlePos.z = 0;
        circleObject.transform.position = circlePos;
        
    }

    private void DetermineCircleSize()
    {
        if(PlayerState.isWalking && circleSize <= maxCircleSize)
        {
            circleSize += circleGrowthRate * Time.deltaTime;
        }
        else if(!PlayerState.isWalking && circleSize >= minCircleSize)
        {
            circleSize -= circleGrowthRate * Time.deltaTime;
        }

        gunFiringArea.transform.localScale = new Vector3(circleSize, circleSize, 0);
    }
}
