using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

//to be placed on a 2d damage region
public class Bleed2D_Bullet : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] List<GameObject> bloodDecals;

    [Header("DEBUG")]
    [SerializeField] GameObject indoorEnvironmentParentObject;

    void Start()
    {
        indoorEnvironmentParentObject = GameObject.FindGameObjectWithTag("Indoor Environment Parent Object");
    }

    /// <summary>
    /// spawns a random blood decal in the direction of the incoming bullet
    /// </summary>
    /// <param name="incomingShotDirection"></param>
    /// <param name="hitPosition"></param>
    public void SpawnBloodDecal(Vector2 incomingShotDirection, Vector2 hitPosition)
    {
        GameObject newBloodDecal = Instantiate(bloodDecals[Random.Range(0, bloodDecals.Count)], hitPosition, 
                                                Quaternion.FromToRotation(Vector3.right, incomingShotDirection));
        newBloodDecal.transform.SetParent(indoorEnvironmentParentObject.transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //same code as penetration script- spawn blood when passing thru an enemy
        if (collision.GetComponent<DamageRegion2D>() != null)
        {
            SpawnBloodDecal(GetComponent<Rigidbody2D>().velocity, transform.position);
        }
    }

}
