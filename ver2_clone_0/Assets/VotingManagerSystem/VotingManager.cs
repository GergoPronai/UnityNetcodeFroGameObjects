using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Lobbies.Models;

public class VotingManager : MonoBehaviour
{
    public static VotingManager Instance;
    private Dictionary<string, int> PlayernamesWithVotesPerPosition= new Dictionary<string, int>();

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void CastVote(PlayerAttackInfosAndChosenAttackNumbers player, int actorNumber)
    {
        player.playerpositionVotes=actorNumber;
        PlayernamesWithVotesPerPosition[player.PlayerName] = player.playerpositionVotes;
    }
    public void RemoveVote(PlayerAttackInfosAndChosenAttackNumbers player, int actorNumber)
    {
        player.playerpositionVotes = actorNumber;
        PlayernamesWithVotesPerPosition[player.PlayerName] = player.playerpositionVotes;
    }
}
