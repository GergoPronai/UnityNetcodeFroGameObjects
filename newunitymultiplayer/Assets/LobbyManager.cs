using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Networking;

public class LobbyManager : NetworkBehaviour
{
    public GameObject lobbyCardPrefab;
    public Transform lobbyCardHolder;
    private GameObject instantiateObj;
    private GameObject[] Players;
    private int playerlistSize=0;

    private void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
    }
    private void Update()
    {
        if (playerlistSize!= Players.Length)
        {
            Players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject item in Players)
            {
                if (item.GetComponent<PlayergameObjScript>().isReady)
                {
                    SetUpChar(item);
                }
            }
            playerlistSize = Players.Length;
        }
    }
    public void SetReadyForPlayer()
    {
        GameObject[] plyrs = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject item in plyrs)
        {
            if (item.GetComponent<NetworkClient>().PlayerObject.IsLocalPlayer)
            {
                item.GetComponent<PlayergameObjScript>().isReady = true;
                break;
            }
        }
    }
    public void SetUpChar(GameObject networkClient)
    {
        for (int i = 0; i < lobbyCardHolder.transform.childCount; i++)
        {
            Destroy(lobbyCardHolder.transform.GetChild(i));
        }
        instantiateObj = Instantiate(lobbyCardPrefab, lobbyCardHolder);
        instantiateObj.GetComponent<PlayerLobbyScript>().PlayerNameText.text = networkClient.GetComponent<PlayergameObjScript>().PlayerName;
        instantiateObj.GetComponent<PlayerLobbyScript>().PlayerhealthText.text = networkClient.GetComponent<PlayergameObjScript>().playerHealth.ToString();
        instantiateObj.GetComponent<PlayerLobbyScript>().attackInfos = networkClient.GetComponent<PlayergameObjScript>().attackInfos;
        instantiateObj.GetComponent<PlayerLobbyScript>().getCharImage(networkClient.GetComponent<PlayergameObjScript>().CharChosen);
    }

}
