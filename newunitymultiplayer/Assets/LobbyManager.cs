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
    private bool[] Players_ready = { false, false, false };
    private int playerlistSize=0;

    private void Update()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i].GetComponent<PlayergameObjScript>().isReady)
            {
                Players_ready[i] = Players[i].GetComponent<PlayergameObjScript>().isReady;
            }
        }
        if (playerlistSize!= Players_ready.Length)
        {
            foreach (GameObject item in Players)
            {
                if (item.GetComponent<PlayergameObjScript>().isReady)
                {
                    SetUpChar(item);                    
                }
            }
            playerlistSize = Players_ready.Length;
        }
    }
    public void SetReady()
    {
        NetworkManager.LocalClient.PlayerObject.GetComponent<PlayergameObjScript>().isReady = true;
    }
    public void SetUpChar(GameObject networkClient)
    {
        for (int i = 0; i < lobbyCardHolder.transform.childCount; i++)
        {
            Destroy(lobbyCardHolder.transform.GetChild(i));
        }
        instantiateObj = Instantiate(lobbyCardPrefab, lobbyCardHolder);
        instantiateObj.GetComponent<PlayerLobbyScript>().PlayerNameText.text = networkClient.GetComponent<PlayergameObjScript>().PlayerName;
        instantiateObj.GetComponent<PlayerLobbyScript>().PlayerhealthText.text = "Health: " + networkClient.GetComponent<PlayergameObjScript>().playerHealth.ToString();
        instantiateObj.GetComponent<PlayerLobbyScript>().getCharImage(networkClient.GetComponent<PlayergameObjScript>().CharChosen);
    }

}
