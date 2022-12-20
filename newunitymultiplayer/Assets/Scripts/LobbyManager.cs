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
        NetworkManager.LocalClient.PlayerObject.GetComponent<PlayerNetwork>().playerLobbyCardPrefabHolder = playerLobbyCardPrefabHolder;
        NetworkManager.LocalClient.PlayerObject.GetComponent<PlayerNetwork>().setUpLobby();
    }
}
