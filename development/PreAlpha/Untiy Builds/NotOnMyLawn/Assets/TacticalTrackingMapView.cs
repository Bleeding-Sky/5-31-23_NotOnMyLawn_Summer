using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalTrackingMapView : MonoBehaviour
{
    public Transform zombiePosition;
    public Transform zombieGoalPoint;
    public Transform zombieTacticalGoalPoint;

    public float x;
    public float y;
    public float z;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TranslateXposition();
        TranslateYposition();

        transform.position = new Vector3(x, y, z);
    }

    void TranslateXposition()
    {
        x = (zombieGoalPoint.transform.position.x - zombiePosition.transform.position.x ) + zombieTacticalGoalPoint.transform.position.x;
    }
    void TranslateYposition()
    {
        y = Mathf.Abs((zombiePosition.transform.position.z - zombieGoalPoint.transform.position.z)) + zombieTacticalGoalPoint.transform.position.y;
       
    }

}
