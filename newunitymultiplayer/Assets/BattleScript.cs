using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int amountOfEnemies=0;
    public GameObject[] Enemies;
    public GameObject SpawnPointHolder;
    private int prevSpawnPoint=-1;


    public void enable()
    {
        amountOfEnemies = Random.Range(1, Enemies.Length);
        randomizeEnemies();
    }

    // Update is called once per frame
    void randomizeEnemies()
    {
        for (int i = 0; i < amountOfEnemies; i++)
        {
            int spawnPoint = Random.Range(0, SpawnPointHolder.transform.childCount);
            while(spawnPoint==prevSpawnPoint)
            {
                spawnPoint = Random.Range(0, SpawnPointHolder.transform.childCount);
            }        
            GameObject enemy = Instantiate(Enemies[Random.Range(0, Enemies.Length)], SpawnPointHolder.transform.GetChild(spawnPoint).transform);
        }
    }
}
