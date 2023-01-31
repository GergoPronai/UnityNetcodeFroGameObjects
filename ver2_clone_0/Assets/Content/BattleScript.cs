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
    public GameObject battleCamParent;
    public GameObject[] Players;
    public GameObject SpawnPointHolder_Players;
    private GameObject playerCam;

    [ServerRpc]
    public void enableServerRpc(GameObject PlayerCam)
    {
        playerCam = PlayerCam;
        PlayerActivatedBattle = playerCam.transform.parent.gameObject;
        amountOfEnemies = Random.Range(1, Enemies.Length);
        Players = GameObject.FindGameObjectsWithTag("Player");
        randomizeEnemies();
        goPlayerPoints();
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
    }
    public void goPlayerPoints()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].GetComponent<PlayerMovement>().SpawnPointHolder_Players = SpawnPointHolder_Players;
            Players[i].transform.position = Players[i].GetComponent<PlayerMovement>().SpawnPointHolder_Players.transform.GetChild(Players[i].GetComponent<PlayerNetwork>().playerPositionInBattle.Value).position;
            Players[i].GetComponent<PlayerMovement>().allowedMove = false;
            Players[i].GetComponent<PlayerMovement>().animator.SetFloat("Speed", 0f);
            Players[i].GetComponent<PlayergameObjScript>().battleCamCanvas.SetActive(true);

            
            if (battleCamParent != null)
            {
                battleCamParent.SetActive(true);
                playerCam.transform.SetParent(battleCamParent.transform);
                playerCam.transform.position = battleCamParent.transform.position;
                playerCam.transform.rotation = battleCamParent.transform.rotation;
                battleCamParent.transform.rotation.SetAxisAngle(Vector3.up, SpawnPointHolder_Enemies.transform.rotation.eulerAngles.y + 180f);
            }
            else
            {
                Debug.Log("BattleCam is null value");
            }
        }
    }
}
