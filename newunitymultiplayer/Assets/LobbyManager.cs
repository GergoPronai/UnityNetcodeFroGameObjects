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

    private void Start()
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject item in Players)
        {
            SetUpChar(item.GetComponent<NetworkObject>().NetworkManager.LocalClient);
        }
    }
    public void SetUpChar(NetworkClient networkClient)
    {
        for (int i = 0; i < lobbyCardHolder.transform.childCount; i++)
        {
            Destroy(lobbyCardHolder.transform.GetChild(i));
        }
        instantiateObj = Instantiate(lobbyCardPrefab, lobbyCardHolder);
        instantiateObj.GetComponent<PlayerLobbyScript>().PlayerNameText.text = networkClient.PlayerObject.GetComponent<PlayergameObjScript>().PlayerName;
        instantiateObj.GetComponent<PlayerLobbyScript>().PlayerhealthText.text = networkClient.PlayerObject.GetComponent<PlayergameObjScript>().playerHealth.ToString();
        instantiateObj.GetComponent<PlayerLobbyScript>().attackInfos = networkClient.PlayerObject.GetComponent<PlayergameObjScript>().attackInfos;
        instantiateObj.GetComponent<PlayerLobbyScript>().getCharImage(networkClient.PlayerObject.GetComponent<PlayergameObjScript>().CharChosen);
    }

}
