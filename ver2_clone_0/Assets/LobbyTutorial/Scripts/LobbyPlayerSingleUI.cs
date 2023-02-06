using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine.UI;

public class LobbyPlayerSingleUI : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerPosTextPrefix;
    [SerializeField] private TextMeshProUGUI playerPosText;
    [SerializeField] private Image characterImage;
    [SerializeField] private Button kickPlayerButton;
    [SerializeField] private Button IncreasePlayerPosition;
    [SerializeField] private Button DecreasePlayerPosition;

    private Player player;
    private PlayerAttackInfosAndChosenAttackNumbers PlayerInfoStorage;

    private void Awake() {
        kickPlayerButton.onClick.AddListener(KickPlayer);
        IncreasePlayerPosition.onClick.AddListener(IncreasePlayerPositionFunction);
        DecreasePlayerPosition.onClick.AddListener(DecreasePlayerPositionFunction);
        PlayerInfoStorage = GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<PlayerAttackInfosAndChosenAttackNumbers>();

    }

    public void SetKickPlayerButtonVisible(bool visible) {
        kickPlayerButton.gameObject.SetActive(visible);
    }
    public void SetIncreaseAndDecreasePlayerButtonVisible(bool visible)
    {
        IncreasePlayerPosition.gameObject.SetActive(visible);
        playerPosText.gameObject.SetActive(visible);
        playerPosTextPrefix.gameObject.SetActive(visible);
        DecreasePlayerPosition.gameObject.SetActive(visible);
    }

    public void UpdatePlayer(Player player) {
        this.player = player;
        playerNameText.text = player.Data[LobbyManager.KEY_PLAYER_NAME].Value;
        CharacterChoices playerCharacter = 
            System.Enum.Parse<CharacterChoices>(player.Data[LobbyManager.KEY_PLAYER_CHARACTER].Value);
        characterImage.sprite = LobbyAssets.Instance.GetSprite(playerCharacter);
        playerPosText.text = player.Data[LobbyManager.KEY_PLAYER_Position_Number].Value;
    }
    private void IncreasePlayerPositionFunction()
    {
        int temp;
        int.TryParse(LobbyManager.Instance.playerPosition, out temp);
        if (temp <= 1)
        {
            temp = 1;
        }
        if (temp>=4)
        {
            temp = 4;
        }
        else
        {
            VotingManager.Instance.RemoveVote(PlayerInfoStorage, temp);
            temp++;
        }
        Debug.Log(temp);
        LobbyManager.Instance.playerPosition = temp.ToString();
        LobbyManager.Instance.UpdatePlayerAttackPosition(temp);
        VotingManager.Instance.CastVote(PlayerInfoStorage, temp);
    }
    private void DecreasePlayerPositionFunction()
    {
        int temp;
        int.TryParse(LobbyManager.Instance.playerPosition, out temp);
        if (temp <= 1)
        {
            temp = 1;
            VotingManager.Instance.RemoveVote(PlayerInfoStorage, temp);
        }
        else
        {
            VotingManager.Instance.RemoveVote(PlayerInfoStorage, temp);
            temp--;
        }
        VotingManager.Instance.CastVote(PlayerInfoStorage, temp);

        LobbyManager.Instance.playerPosition = temp.ToString();
        LobbyManager.Instance.UpdatePlayerAttackPosition(temp);

    }
    private void KickPlayer() {
        if (player != null) {
            LobbyManager.Instance.KickPlayer(player.Id);
        }
    }

}