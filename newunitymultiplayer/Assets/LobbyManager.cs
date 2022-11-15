using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Networking;

public class LobbyManager : NetworkBehaviour
{
    public void SetUpChar()
    {
        NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayergameObjScript>().disable();
    }
}
