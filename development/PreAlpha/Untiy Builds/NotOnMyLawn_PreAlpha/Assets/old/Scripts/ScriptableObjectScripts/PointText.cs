using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointText : MonoBehaviour
{
    public PointTracker points;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        points.points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Point: " + points.points.ToString();
    }
}
