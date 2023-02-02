using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VotePlayerItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _PlayerNametext;
    [SerializeField] private TextMeshProUGUI _PlayerVotedtext;
    [SerializeField] private Button one;
    [SerializeField] private Button two;
    [SerializeField] private Button three;
    [SerializeField] private Button four;
    private int actorNumber;
    public int AtorNumber
    {
        get { return actorNumber; }
    }
    private VotingManager _votingManager;
    public PlayergameObjScript player;
    private int pos1=0;
    private int pos2=0;
    private int pos3=0;
    private int pos4=0;
    private bool isClicked = false;
    // Start is called before the first frame update

    // Update is called once per frame
    public void OnVotePressed(int number)
    {
        isClicked = !isClicked;
        if (isClicked)
        {
            switch (number)
            {
                case 0:
                    this.pos1++;
                    this.one.interactable = true;
                    this.two.interactable = false;
                    this.three.interactable = false;
                    this.four.interactable = false;
                    actorNumber = 0;
                    break;
                case 1:
                    this.pos2++;
                    this.one.interactable = false;
                    this.two.interactable = true;
                    this.three.interactable = false;
                    this.four.interactable = false;
                    actorNumber = 1;
                    break;
                case 2:
                    this.pos3++;
                    this.one.interactable = false;
                    this.two.interactable = false;
                    this.three.interactable = true;
                    this.four.interactable = false;
                    actorNumber = 2;
                    break;
                case 3:
                    this.pos4++;
                    this.one.interactable = false;
                    this.two.interactable = false;
                    this.three.interactable = false;
                    this.four.interactable = true;
                    actorNumber = 3;
                    break;
            }
            _votingManager.CastVote(player, actorNumber);

        }
        else
        {
            switch (number)
            {
                case 0:
                    this.pos1++;
                    this.one.interactable = true;
                    this.two.interactable = true;
                    this.three.interactable = true;
                    this.four.interactable = true;
                    actorNumber = 0;
                    break;
                case 1:
                    this.pos2++;
                    this.one.interactable = true;
                    this.two.interactable = true;
                    this.three.interactable = true;
                    this.four.interactable = true;
                    actorNumber = 1;
                    break;
                case 2:
                    this.pos3++;
                    this.one.interactable = true;
                    this.two.interactable = true;
                    this.three.interactable = true;
                    this.four.interactable = true;
                    actorNumber = 2;
                    break;
                case 3:
                    this.pos4++;
                    this.one.interactable = true;
                    this.two.interactable = true;
                    this.three.interactable = true;
                    this.four.interactable = true;
                    actorNumber = 3;
                    break;
            }
            _votingManager.RemoveVote(player, actorNumber);

        }
    }
    public void Initialise(PlayergameObjScript _player, VotingManager votingManager)
    {
        player = _player;
        _PlayerNametext.text = _player.PlayerName;
        _PlayerVotedtext.text = "Not Decided";
        _votingManager = votingManager;
    }
    public void UpdateStatus(string status)
    {
        _PlayerVotedtext.text = status;
    }
}
