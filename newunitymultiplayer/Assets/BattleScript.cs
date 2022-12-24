using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Enemies")]
    public int amountOfEnemies=0;
    public GameObject[] Enemies;
    public GameObject SpawnPointHolder_Enemies;
    private int prevSpawnPoint_Enemies=-1;
    [Header("Players")]
    public int amountOfPlayers = 0;
    public GameObject[] Players;
    public GameObject SpawnPointHolder_Players;
    private int prevSpawnPoint_Players = -1;


    public void enable()
    {
        amountOfEnemies = Random.Range(1, Enemies.Length);
        Players = GameObject.FindGameObjectsWithTag("Player");
        amountOfPlayers = Players.Length;
        randomizeEnemies();
        goPlayerPoints();
    }

    // Update is called once per frame
    void randomizeEnemies()
    {
        for (int i = 0; i < amountOfEnemies; i++)
        {
            int spawnPoint = Random.Range(0, SpawnPointHolder_Enemies.transform.childCount);
            while(spawnPoint==prevSpawnPoint_Enemies)
            {
                spawnPoint = Random.Range(0, SpawnPointHolder_Enemies.transform.childCount);
            }        
            GameObject enemy = Instantiate(Enemies[Random.Range(0, Enemies.Length)], SpawnPointHolder_Enemies.transform.GetChild(spawnPoint).transform);
        }
    }
    public void goPlayerPoints()
    {
        for (int i = 0; i < amountOfPlayers; i++)
        {
            Players[i].GetComponent<PlayerMovement>().SpawnPointHolder_Players = SpawnPointHolder_Players;
            Players[i].GetComponent<PlayerMovement>().moveToPosition=true;
        }
    }
}
