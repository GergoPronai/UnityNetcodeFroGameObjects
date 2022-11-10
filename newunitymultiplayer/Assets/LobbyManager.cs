using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Networking;

public class LobbyManager : NetworkBehaviour
{
    public GameObject lobbyCardPrefab;
    public Transform lobbyCardHolder;
    private GameObject instantiateObj;
    int ConnectedPlayers;
    public int ConnectedPlayersAll
    {
        get
        {
            return ConnectedPlayers;
        }
        set
        {
            ConnectedPlayers = value;
            SetUpChar(NetworkManager.Singleton.ConnectedClientsList);
        }
    }
    private void Update()
    {
        ConnectedPlayersAll = NetworkManager.Singleton.ConnectedClientsList.Count;
    }
    // Start is called before the first frame update

    public void SetUpChar(IReadOnlyList<NetworkClient> networkClients)
    {
        foreach (var item in networkClients)
        {
            instantiateObj = Instantiate(lobbyCardPrefab, lobbyCardHolder);
            instantiateObj.GetComponent<PlayerLobbyScript>().PlayerNameText.text = item.PlayerObject.GetComponent<PlayergameObjScript>().PlayerName;
            instantiateObj.GetComponent<PlayerLobbyScript>().PlayerhealthText.text = item.PlayerObject.GetComponent<PlayergameObjScript>().playerHealth.ToString();
            instantiateObj.GetComponent<PlayerLobbyScript>().attackInfos = item.PlayerObject.GetComponent<PlayergameObjScript>().attackInfos;
            instantiateObj.GetComponent<PlayerLobbyScript>().getCharImage(item.PlayerObject.GetComponent<PlayergameObjScript>().CharChosen);
        }
    }
}
