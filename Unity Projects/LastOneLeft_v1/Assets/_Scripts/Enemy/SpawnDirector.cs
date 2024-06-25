using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(Spawner_Zombie))]

public class SpawnDirector : MonoBehaviour
{
    [Serializable]
    public struct EnemySpawnPrefabs
    {
        public GameObject masterPrefab;
        public GameObject overheadPrefab;
    }

    [Header("CONFIG")]
    [SerializeField] float spawnDelay = 5;
    [SerializeField] float nightDuration = 240;
    [SerializeField] float dayDuration = 150;
    [SerializeField] List<EnemySpawnPrefabs> spawnableEnemies;

    [Header("DEBUG")]
    [SerializeField] SpriteRenderer walkableGroundSprite;
    float spawnAreaXMin;
    float spawnAreaXMax;
    float spawnAreaTop;

    [SerializeField] Spawner_Zombie spawnerScript;

    [SerializeField] bool isNight = true;

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
        StartNight();
    }

    void SpawnEnemy()
    {

        EnemySpawnPrefabs enemyToSpawn = ChooseEnemyToSpawn();

        //spawns an enemy at a random x coordinate at the farthest y of the walkable area
        Vector2 spawnPosition = new Vector2(UnityEngine.Random.Range(spawnAreaXMin, spawnAreaXMax), spawnAreaTop);
        spawnerScript.SpawnZmb(spawnPosition, enemyToSpawn.masterPrefab, enemyToSpawn.overheadPrefab);

        if (isNight)
        {
            Invoke(nameof(SpawnEnemy), spawnDelay);
        }
        
    }

    /// <summary>
    /// selects an enemy to spawn from the SpawnableEnemies list.
    /// </summary>
    /// <returns></returns>
    EnemySpawnPrefabs ChooseEnemyToSpawn()
    {
        //randomly pick an enemy to spawn from all in list. equal chance of each
        EnemySpawnPrefabs enemyToSpawn = spawnableEnemies[UnityEngine.Random.Range(0, spawnableEnemies.Count)];

        return enemyToSpawn;
    }

    void StartNight()
    {
        isNight = true;
        Invoke(nameof(StartDay), nightDuration);
        SpawnEnemy();
    }

    void StartDay()
    { 
        isNight = false;
        Invoke(nameof(StartNight), dayDuration);
    }

}
