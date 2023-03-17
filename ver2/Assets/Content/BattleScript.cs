using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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
    public GameObject PlayerActivatedBattle;
    public GameObject[] Players;
    public GameObject SpawnPointHolder_Players;
    private Camera SecondaryCamera;
    private Camera MainCam;
    private GameObject battlemanger;
    public GameObject ChestPrefab;
    public GameObject[] possibleItems;
    private void Awake()
    {
        MainCam = Camera.main;
    }
    [ServerRpc]
    public void enableServerRpc(GameObject _PlayerActivatedBattle)
    {
        PlayerActivatedBattle = _PlayerActivatedBattle;
        amountOfEnemies = Random.Range(1, Enemies.Length);
        Players = GameObject.FindGameObjectsWithTag("Player");
        randomizeEnemies();
        goPlayerPoints();        
        battlemanger = GameObject.FindGameObjectWithTag("BattleManager").transform.GetChild(0).gameObject;
        battlemanger.SetActive(true);//get the child of for 'find' reasons
        battlemanger.GetComponent<battleSystem>().battleBox = this.gameObject;

    }

    // Update is called once per frame
    void randomizeEnemies()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            int spawnPoint = Random.Range(0, SpawnPointHolder_Enemies.transform.childCount);
            while(spawnPoint==prevSpawnPoint_Enemies)
            {
                spawnPoint = Random.Range(0, SpawnPointHolder_Enemies.transform.childCount);
            }
            
            GameObject enemy = Instantiate(Enemies[Random.Range(0, Enemies.Length)], SpawnPointHolder_Enemies.transform.GetChild(spawnPoint).transform);
           
        }
        GameObject.FindGameObjectWithTag("EnemyHealthManager").transform.GetChild(0).gameObject.SetActive(true);
    }
    public void goPlayerPoints()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].GetComponent<PlayerMovement>().SpawnPointHolder_Players = SpawnPointHolder_Players;
            Vector3 targetPosition = Players[i].GetComponent<PlayerMovement>().SpawnPointHolder_Players.transform.GetChild(Players[i].GetComponent<PlayerNetwork>().playerPositionInBattle.Value).position;
            Players[i].transform.position = targetPosition;
            Players[i].transform.GetChild(0).rotation = Quaternion.LookRotation(PlayerActivatedBattle.transform.forward);
            Players[i].GetComponent<PlayerMovement>().allowedMove = false;
            Players[i].GetComponent<PlayerMovement>().animator.SetFloat("Speed", 0f);
            Players[i].GetComponent<PlayergameObjScript>().battleCamCanvas.SetActive(true);
        }
    }
    

}
