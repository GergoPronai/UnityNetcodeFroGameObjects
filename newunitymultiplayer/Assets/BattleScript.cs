using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int amountOfEnemies=0;
    public GameObject[] Enemies;
    public GameObject SpawnPointHolder;
    private List<GameObject> spawnPoints;
    private int prevSpawnPoint=-1;


    public void enable()
    {
        for (int i = 0; i < SpawnPointHolder.transform.childCount; i++)
        {
            spawnPoints.Add(SpawnPointHolder.transform.GetChild(i).gameObject);
        }
        amountOfEnemies = Random.RandomRange(1, Enemies.Length);
        randomizeEnemies();
    }

    // Update is called once per frame
    void randomizeEnemies()
    {
        int spawnPoint = Random.RandomRange(0, spawnPoints.Count);
        while(spawnPoint==prevSpawnPoint)
        {
            spawnPoint = Random.RandomRange(0, spawnPoints.Count);
        }
        for (int i = 0; i < amountOfEnemies; i++)
        {
            GameObject enemy = Instantiate(Enemies[Random.RandomRange(0, Enemies.Length)], spawnPoints[spawnPoint].transform);
        }
    }
}
