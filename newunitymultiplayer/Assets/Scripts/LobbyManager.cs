using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Networking;
using Unity.Collections;
public class LobbyManager : NetworkBehaviour
{
    public static LobbyManager instance;
    public GameObject playerLobbyCardPrefabHolder;
    public void ShowPlayer()
    {
        NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayergameObjScript>().disable();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.GetComponent<PlayerNetwork>().playerLobbyCardPrefabHolder = playerLobbyCardPrefabHolder;
        }
        NetworkManager.LocalClient.PlayerObject.GetComponent<PlayerNetwork>().setUpLobby();
    }
}
