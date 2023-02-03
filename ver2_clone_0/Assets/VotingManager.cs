using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Lobbies.Models;

public class VotingManager : MonoBehaviour
{
    public static VotingManager Instance;
    private Dictionary<string, List<int>> PlayernamesWithVotesPerPosition= new Dictionary<string, List<int>>();

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void CastVote(PlayerAttackInfosAndChosenAttackNumbers player, int actorNumber)
    {
        player.playerpositionVotes[actorNumber]++;
        PlayernamesWithVotesPerPosition[player.PlayerName] = player.playerpositionVotes;
        Debug.Log(PlayernamesWithVotesPerPosition[player.PlayerName]);

    }
    public void RemoveVote(PlayerAttackInfosAndChosenAttackNumbers player, int actorNumber)
    {
        player.playerpositionVotes[actorNumber]--;
        PlayernamesWithVotesPerPosition[player.PlayerName] = player.playerpositionVotes;
        Debug.Log(PlayernamesWithVotesPerPosition[player.PlayerName]);
    }
}
