using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shove_Enemy : MonoBehaviour
{
    public bool shoved;
    public bool stunned;
    public virtual void Shove()
    {
        Debug.Log("Shoving " + gameObject.name);
    }
}
