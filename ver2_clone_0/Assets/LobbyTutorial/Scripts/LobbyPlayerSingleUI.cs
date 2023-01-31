using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine.UI;

public class LobbyPlayerSingleUI : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private Image characterImage;
    [SerializeField] private Button kickPlayerButton;

    private Player player;


    private void Awake() {
        kickPlayerButton.onClick.AddListener(KickPlayer);
        //IncreasePlayerPosition.onClick.AddListener(IncreasePlayerPositionFunction);
        //DecreasePlayerPosition.onClick.AddListener(DecreasePlayerPositionFunction);
    }

    public void SetKickPlayerButtonVisible(bool visible) {
        kickPlayerButton.gameObject.SetActive(visible);
    }

    public void UpdatePlayer(Player player) {
        this.player = player;
        playerNameText.text = player.Data[LobbyManager.KEY_PLAYER_NAME].Value;
        CharacterChoices playerCharacter = 
            System.Enum.Parse<CharacterChoices>(player.Data[LobbyManager.KEY_PLAYER_CHARACTER].Value);
        characterImage.sprite = LobbyAssets.Instance.GetSprite(playerCharacter);
        //playerPosText.text = "Attack Position " + player.Data[LobbyManager.KEY_PLAYER_Position_Number].Value;
    }
    private void IncreasePlayerPositionFunction()
    {
        int temp;
        int.TryParse(LobbyManager.Instance.playerPosition, out temp);
        if (temp>=4)
        {
            temp = 4;
        }
        else
        {
            temp++;
        }
        LobbyManager.Instance.UpdatePlayerAttackPosition(temp);

    }
    private void DecreasePlayerPositionFunction()
    {
        int temp;
        int.TryParse(LobbyManager.Instance.playerPosition, out temp);
        if (temp <= 0)
        {
            temp = 0;
        }
        else
        {
            temp--;
        }
        LobbyManager.Instance.UpdatePlayerAttackPosition(temp);

    }
    private void KickPlayer() {
        if (player != null) {
            LobbyManager.Instance.KickPlayer(player.Id);
        }
    }

}