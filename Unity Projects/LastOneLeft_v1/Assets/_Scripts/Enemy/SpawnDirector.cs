using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(Spawner_Zombie))]

public class SpawnDirector : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] float spawnDelay = 5;

    [Header("DEBUG")]
    [SerializeField] SpriteRenderer walkableGroundSprite;
    float spawnAreaXMin;
    float spawnAreaXMax;
    float spawnAreaTop;

    [SerializeField] Spawner_Zombie spawnerScript;

    private void Awake()
    {
        spawnerScript = GetComponent<Spawner_Zombie>();
    }

    private void Start()
    {
        //calculates extents of the walkable area for zombie spawning
        walkableGroundSprite = GameObject.FindGameObjectWithTag("Overhead Walkable Ground").GetComponent<SpriteRenderer>();

        spawnAreaXMin = walkableGroundSprite.transform.position.x - walkableGroundSprite.bounds.extents.x;
        spawnAreaXMax = walkableGroundSprite.transform.position.x + walkableGroundSprite.bounds.extents.x;
        spawnAreaTop = walkableGroundSprite.transform.position.y + walkableGroundSprite.bounds.extents.y;

        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        //spawns an enemy at a random x coordinate at the farthest y of the walkable area
        Vector2 spawnPosition = new Vector2(Random.Range(spawnAreaXMin, spawnAreaXMax), spawnAreaTop);
        spawnerScript.SpawnZmb(spawnPosition);

        Invoke(nameof(SpawnEnemy), spawnDelay);
    }
}
