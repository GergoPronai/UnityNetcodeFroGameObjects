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

    public LobbySeeAttacks Attack1_info;
    public LobbySeeAttacks Attack2_info;
    public LobbySeeAttacks Attack3_info;

    private void Awake() {
        kickPlayerButton.onClick.AddListener(KickPlayer);

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
        playerNameText.text = player.Data[LobbyManager.KEY_PLAYER_NAME].Value;

        Attack1_info.attackNumber = player.Data[LobbyManager.KEY_PLAYER_Attack_Number1].Value;
        Attack2_info.attackNumber = player.Data[LobbyManager.KEY_PLAYER_Attack_Number2].Value;
        Attack3_info.attackNumber = player.Data[LobbyManager.KEY_PLAYER_Attack_Number3].Value;

    }

    private void KickPlayer() {
        if (player != null) {
            LobbyManager.Instance.KickPlayer(player.Id);
        }
    }

}