using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Networking;
using Unity.Collections;
public class LobbyManager : NetworkBehaviour
{
    private GameObject instantiatedOBJ;
    public GameObject instanObj;
    protected virtual byte MessageType()
    {
        // As an example, we could define message type of 1 for string messages
        return 1;
    }
    public void SetUpChar()
    {
        NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayergameObjScript>().disable();
    }
    public void ShowPlayer()
    {
        transform.GetComponent<PlayerNetwork>()._clientID.Value = NetworkManagerUiMain.instance.PlayerID;
    }
}
