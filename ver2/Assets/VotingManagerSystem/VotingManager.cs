using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class VotingManager : NetworkBehaviour
{
    public static VotingManager Instance;
    public Lobby joinedLobby;


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
}
