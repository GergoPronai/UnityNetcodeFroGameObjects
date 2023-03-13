using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestScript : MonoBehaviour
{
    public GameObject[] ObjectsToSpawn;

    public void enable()
    {
        // Generate a random number of objects to spawn between 1 and 10
        int numObjectsToSpawn = Random.Range(1, 5);

        for (int i = 0; i < numObjectsToSpawn; i++)
        {
            // Choose a random object from the ObjectsToSpawn array
            int randomIndex = Random.Range(0, ObjectsToSpawn.Length);
            GameObject objectToSpawn = ObjectsToSpawn[randomIndex];

            // Instantiate the object at a random position within the transform's bounds
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}

