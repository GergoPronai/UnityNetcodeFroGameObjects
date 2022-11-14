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
    private GameObject OWNERinstantiateObj;
    private GameObject[] Players;

    public void OnStart()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject item in Players)
        {
            SetUpChar(item);
        }
    }
    public void SetUpChar(GameObject networkClient)
    {
        for (int i = 0; i < lobbyCardHolder.transform.childCount; i++)
        {
            Destroy(lobbyCardHolder.transform.GetChild(i).gameObject);
        }
        instantiateObj = Instantiate(lobbyCardPrefab, lobbyCardHolder);
        instantiateObj.GetComponent<PlayerLobbyScript>().PlayerNameText.text = networkClient.GetComponent<PlayergameObjScript>().PlayerName;
        instantiateObj.GetComponent<PlayerLobbyScript>().getCharImage(networkClient.GetComponent<PlayergameObjScript>().CharChosen);
        if (NetworkManager.LocalClient.PlayerObject==networkClient)
        {
            OWNERinstantiateObj = instantiateObj;
        }
    }
}
