using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCreateUI : MonoBehaviour {


    public static LobbyCreateUI Instance { get; private set; }


    [SerializeField] private Button createButton;
    [SerializeField] private Button lobbyNameButton;
    [SerializeField] private Button maxPlayersButton;
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private TextMeshProUGUI maxPlayersText;


    private string lobbyName;
    private int maxPlayers;

    private void Awake() {
        Instance = this;

        createButton.onClick.AddListener(() => {
            LobbyManager.Instance.CreateLobby(
                lobbyName,
                maxPlayers,
                true
            );
            Hide();
        });

        lobbyNameButton.onClick.AddListener(() => {
            UI_InputWindow.Show_Static("Lobby Name", lobbyName, "abcdefghijklmnopqrstuvxywzABCDEFGHIJKLMNOPQRSTUVXYWZ .,-", 20,
            () => {
                // Cancel
            },
            (string lobbyName) => {
                this.lobbyName = lobbyName;
                UpdateText();
            });
        });


        maxPlayersButton.onClick.AddListener(() => {
            UI_InputWindow.Show_Static("Max Players", maxPlayers,
            () => {
                // Cancel
            },
            (int maxPlayers) => {
                this.maxPlayers = maxPlayers;
                UpdateText();
            });
        });


        Hide();
    }

    private void UpdateText() {
        lobbyNameText.text = lobbyName;
        maxPlayersText.text = maxPlayers.ToString();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);

        lobbyName = "MyLobby";
        maxPlayers = 4;

        UpdateText();
    }

}