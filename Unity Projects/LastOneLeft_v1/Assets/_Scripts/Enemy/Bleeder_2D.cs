using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleeder_2D : MonoBehaviour
{
    [SerializeField] GameObject bloodParticlePrefab;
    [SerializeField] int bloodCount = 10;
    [SerializeField] float bloodSpreadMax = .2f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpurtBlood(new Vector2(1, 0).normalized, transform.position, 10);
        }
    }

    public void SpurtBlood(Vector2 normalizedSpurtDirection, Vector3 bloodSource, float bloodSpurtStrength)
    {

        for (int i = 0; i < bloodCount; i++)
        {
            Vector3 randomizedSpurtDirection = new Vector3( normalizedSpurtDirection.x + UnityEngine.Random.Range(-bloodSpreadMax, bloodSpreadMax),
                                                            normalizedSpurtDirection.y + UnityEngine.Random.Range(-bloodSpreadMax, 2*bloodSpreadMax),
                                                            UnityEngine.Random.Range(0f, .8f));
            randomizedSpurtDirection *= bloodSpurtStrength;
            Debug.Log($"Spurt Direction: X = {randomizedSpurtDirection.x}, Y = {randomizedSpurtDirection.y}, Z = {randomizedSpurtDirection.z}");

            GameObject bloodObject = Instantiate(bloodParticlePrefab, bloodSource, Quaternion.identity);
            bloodObject.GetComponent<Rigidbody>().velocity = randomizedSpurtDirection;
        }
        
    }

}
