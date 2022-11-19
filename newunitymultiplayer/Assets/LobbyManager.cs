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

    public GameObject border;
    public void SetUpChar()
    {
        NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayergameObjScript>().disable();
    }
    public void ShowPlayer()
    {
        NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayergameObjScript>().playersJoined++;
        NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerNetwork>().setUpLobby();
    }
}
