using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTacViewSpawner : MonoBehaviour
{

    public GameObject window1;
    public GameObject window2;

    public WindowBoardsDamageScript windowBoardHealth1;
    public WindowBoardsDamageScript windowBoardHealth2;

    public GameObject indoorWindow1;
    public GameObject indoorWindow2;

    public int windowPicker;
    private GameObject window;

    public GameObject tactViewZombie;
    public bool SpawnZombie;
    public float ZombieSpawnTimer;

    public float zombieXDisplacement;
    public float zombieYFromWindow;

    public Vector2 ZombieSpawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        SpawnZombie = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(SpawnZombie == true)
        {
            windowPicker = Random.Range(1, 3);
            chooseWindow();
            spawnZombie();
            StartCoroutine(ZombieSpawnDelay());
        }
    }

    public void chooseWindow()
    {
        TopviewZmbPathing zombieWindow = tactViewZombie.GetComponent<TopviewZmbPathing>();
        TopviewCheckBoardHealth boardHealth = tactViewZombie.GetComponent<TopviewCheckBoardHealth>();
        if (windowPicker == 1)
        {
            window = window1;
            boardHealth.windowBoardHealth = windowBoardHealth1;
            boardHealth.indoorWindow = indoorWindow1.transform.position;
        }
        else if(windowPicker == 2)
        {
            window = window2;
            boardHealth.windowBoardHealth = windowBoardHealth2;
            boardHealth.indoorWindow = indoorWindow2.transform.position;
        }
        
        zombieWindow.windowObject = window;
    }

    public void spawnZombie()
    {
        ZombieSpawnLocation = DetermineSpawnLocation();
        Instantiate(tactViewZombie, ZombieSpawnLocation, Quaternion.identity);
    }
    public Vector2 DetermineSpawnLocation()
    {
        float y = window.transform.position.y + zombieYFromWindow;
        float x = RandomXSpawn();
        Debug.Log(x);
        Vector2 zombieSpawn = new Vector2(x, y);
        return zombieSpawn;
    }

    public float RandomXSpawn()
    {
        float x = window.transform.position.x + Random.Range(-9, 10);
        return x;
    }

    IEnumerator ZombieSpawnDelay()
    {
        ZombieSpawnTimer = Random.Range(1, 3);
        Debug.Log(ZombieSpawnTimer);
        SpawnZombie = false;
        yield return new WaitForSeconds(ZombieSpawnTimer);
        SpawnZombie = true;
    }
}
