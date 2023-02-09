using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour {


    public static LobbyUI Instance;
    public Lobby LobbyJoined { get; private set; }

    [SerializeField] private Transform playerSingleTemplate;
    [SerializeField] private Transform container;
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private TextMeshProUGUI lobbyCodeText;
    [SerializeField] private TextMeshProUGUI playerCountText;
    [SerializeField] private Button leaveLobbyButton;
    [SerializeField] private Button changeGameModeButton;
    private void Awake() {
        Instance = this;

        playerSingleTemplate.gameObject.SetActive(false);

    }

    private void Start() {
        LobbyManager.Instance.OnJoinedLobby += UpdateLobby_Event;
        LobbyManager.Instance.OnJoinedLobbyUpdate += UpdateLobby_Event;
        LobbyManager.Instance.OnLeftLobby += LobbyManager_OnLeftLobby;
        LobbyManager.Instance.OnKickedFromLobby += LobbyManager_OnLeftLobby;

        leaveLobbyButton.onClick.AddListener(() => {
            LobbyManager.Instance.LeaveLobby();
        });
        Hide();
    }
    public void SetCharacterChoice()
    {
        LobbyManager.Instance.UpdatePlayerCharacter(GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<PlayerAttackInfosAndChosenAttackNumbers>().character);
        LobbyManager.Instance.UpdatePlayerAttack_1(GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<PlayerAttackInfosAndChosenAttackNumbers>().CharChosen_ChosenAttacks_1.ToString());
        LobbyManager.Instance.UpdatePlayerAttack_2(GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<PlayerAttackInfosAndChosenAttackNumbers>().CharChosen_ChosenAttacks_2.ToString());
        LobbyManager.Instance.UpdatePlayerAttack_3(GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<PlayerAttackInfosAndChosenAttackNumbers>().CharChosen_ChosenAttacks_3.ToString());
    }
    private void LobbyManager_OnLeftLobby(object sender, System.EventArgs e) {
        ClearLobby();
        Hide();
    }

    private void UpdateLobby_Event(object sender, LobbyManager.LobbyEventArgs e) {
        UpdateLobby();
    }

    private void UpdateLobby() {
        LobbyJoined = LobbyManager.Instance.GetJoinedLobby();        
        UpdateLobby(LobbyManager.Instance.GetJoinedLobby());
    }


    public void UpdateLobby(Lobby lobby)
    {
        ClearLobby();

        foreach (Player player in lobby.Players)
        {
            Transform playerSingleTransform = Instantiate(playerSingleTemplate, container);
            playerSingleTransform.gameObject.SetActive(true);
            LobbyPlayerSingleUI lobbyPlayerSingleUI = playerSingleTransform.GetComponent<LobbyPlayerSingleUI>();

            lobbyPlayerSingleUI.SetKickPlayerButtonVisible(
                LobbyManager.Instance.IsLobbyHost() &&
                player.Id != AuthenticationService.Instance.PlayerId // Don't allow kick self
            );

            lobbyPlayerSingleUI.UpdatePlayer(player);
        }

        changeGameModeButton.gameObject.SetActive(LobbyManager.Instance.IsLobbyHost());

        lobbyNameText.text = lobby.Name;
        playerCountText.text = lobby.Players.Count + "/" + lobby.MaxPlayers;
        lobbyCodeText.text = lobby.LobbyCode;

        Show();
    }


    private void ClearLobby() {
        foreach (Transform child in container) {
            if (child == playerSingleTemplate) continue;
            Destroy(child.gameObject);
            //An error will appear here saying to use 'destroy immediate' instead. it causes the lobby template to still exist 
        }
    }

    private void Hide() {
        if (gameObject!=null)
        {
            gameObject.SetActive(false);

        }
    }

    private void Show() {
        if (gameObject != null)
        {
            gameObject.SetActive(true);

        }
    }

    public void ToggleActiveUIComponent(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}