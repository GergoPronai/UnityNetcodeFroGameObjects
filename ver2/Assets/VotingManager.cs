using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VotingManager : MonoBehaviour
{
    public static VotingManager Instance;
    [SerializeField] private VotePlayerItem playerItemPrefab;
    [SerializeField] private Transform playerItemContainer;
    private List<VotePlayerItem> votePlayerList= new List<VotePlayerItem>();
    private GameObject[] Players;
    private Dictionary<string, List<int>> PlayernamesWithVotesPerPosition= new Dictionary<string, List<int>>();
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void PopulatePlayerList()
    {
        for (int i = 0; i < votePlayerList.Count; i++)
        {
            Destroy(votePlayerList[i].gameObject);
        }
        PlayernamesWithVotesPerPosition.Clear();
        votePlayerList.Clear();
        Players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject item in Players)
        {
            VotePlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemContainer);
            votePlayerList.Add(newPlayerItem);
            newPlayerItem.Initialise(item.GetComponent<PlayergameObjScript>(),this);
            PlayernamesWithVotesPerPosition[newPlayerItem.player.PlayerName] = newPlayerItem.player.playerpositionVotes;
        }
    }

    public void CastVote(PlayergameObjScript player, int actorNumber)
    {
        player.playerpositionVotes[actorNumber]++;
        PlayernamesWithVotesPerPosition[player.PlayerName] = player.playerpositionVotes;
    }
    public void RemoveVote(PlayergameObjScript player, int actorNumber)
    {
        player.playerpositionVotes[actorNumber]--;
        PlayernamesWithVotesPerPosition[player.PlayerName] = player.playerpositionVotes;
    }
}
