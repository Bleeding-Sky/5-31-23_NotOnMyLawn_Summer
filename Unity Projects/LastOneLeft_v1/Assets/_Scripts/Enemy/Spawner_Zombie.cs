using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Zombie : MonoBehaviour
{


    /// <summary>
    /// spawns a new zombie master object as a child of the zombies parent object.
    /// adds new child overhead zombie to that zombie master object at a preset position
    /// </summary>
    public void SpawnZmb(Vector2 overheadSpawnPosition, 
                            GameObject masterPrefab,
                            GameObject overheadPrefab)
    {

        //instantiate master object as child of "zombies" parent object
        GameObject masterObject = Instantiate(masterPrefab, this.transform);

        //instantiate overhead zombie as child of its master parent object
        GameObject newOverheadZmb = Instantiate(overheadPrefab, masterObject.transform);

        //configure position
        newOverheadZmb.transform.position = overheadSpawnPosition;


    }

}
