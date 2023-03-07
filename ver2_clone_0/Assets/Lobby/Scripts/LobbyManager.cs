using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;

/// <summary>
/// RelayHostData represents the necessary informations
/// for a Host to host a game on a Relay
/// </summary>
public struct RelayHostData
{
    public string JoinCode;
    public string IPv4Address;
    public ushort Port;
    public Guid AllocationID;
    public byte[] AllocationIDBytes;
    public byte[] ConnectionData;
    public byte[] Key;
}

/// <summary>
/// RelayHostData represents the necessary informations
/// for a Host to host a game on a Relay
/// </summary>
public struct RelayJoinData
{
    public string JoinCode;
    public string IPv4Address;
    public ushort Port;
    public Guid AllocationID;
    public byte[] AllocationIDBytes;
    public byte[] ConnectionData;
    public byte[] HostConnectionData;
    public byte[] Key;
}

public class LobbyManager : MonoBehaviour {


    public static LobbyManager Instance { get; private set; }
    public string KEY_START_GAME = "1";

    public const string KEY_PLAYER_NAME = "PlayerName";
    public const string KEY_PLAYER_CHARACTER = "Character";
    public const string KEY_PLAYER_Position_Number = "0";
    public const string KEY_PLAYER_Attack_Number1 = "2";
    public const string KEY_PLAYER_Attack_Number2 = "3";
    public const string KEY_PLAYER_Attack_Number3 = "4";

    public TestRelay TestRelayScript;

    private RelayHostData _hostData;
    private RelayJoinData _joinData;

    public event EventHandler OnLeftLobby;

    public UnityAction<string> UpdateState;


    public event EventHandler<LobbyEventArgs> OnJoinedLobby;
    public event EventHandler<LobbyEventArgs> OnJoinedLobbyUpdate;
    public event EventHandler<LobbyEventArgs> OnKickedFromLobby;



    public class LobbyEventArgs : EventArgs {
        public Lobby lobby;
    }

    public event EventHandler<OnLobbyListChangedEventArgs> OnLobbyListChanged;
    public class OnLobbyListChangedEventArgs : EventArgs {
        public List<Lobby> lobbyList;
    }


    private float heartbeatTimer;
    private float lobbyPollTimer;
    private float refreshLobbyListTimer = 5f;
    public Lobby joinedLobby;
    private bool GameStarted=false;
    public string playerName;
    public string playerCharacterName="0";
    public string playerPosition="1";
    public string playerAttack1="0";
    public string playerAttack2= "0";
    public string playerAttack3= "0";
    public UnityAction MatchFound;
    private int _maxPlayers;
    private GameObject DungeonGeneratorObj;
    public bool collided=false;
    private Player _Player;

    private const int MaxNumberOfMessagesInList = 20;
    private List<ChatMessage> _messages;
    private PlayerAttackInfosAndChosenAttackNumbers localinfos;
    private const float MinIntervalBetweenChatMessages = 1f;
    private float _clientSendTimer;

    public GameObject message;


    IEnumerator HeartbeatLobbyCoroutine(string lobbyId, float waitTimeSeconds)
    {
        var delay = new WaitForSecondsRealtime(waitTimeSeconds);
        while (true)
        {
            Lobbies.Instance.SendHeartbeatPingAsync(lobbyId);
            Debug.Log("Lobby Heartbit");
            yield return delay;
        }
    }
    private void Awake() {
        Instance = this;
        _messages = new List<ChatMessage>();
        localinfos = GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<PlayerAttackInfosAndChosenAttackNumbers>();
    }
    private void Update() {
        //HandleRefreshLobbyList(); // Disabled Auto Refresh for testing with multiple builds
        HandleLobbyHeartbeat();
        HandleLobbyPolling();
        HandleDungeonGenOnStartUp();
    }

    public async void Authenticate(string playerName) {
        this.playerName = playerName;
        InitializationOptions initializationOptions = new InitializationOptions();
        initializationOptions.SetProfile(playerName);

        await UnityServices.InitializeAsync(initializationOptions);

        AuthenticationService.Instance.SignedIn += () => {
            // do nothing
            Debug.Log("Signed in! " + AuthenticationService.Instance.PlayerId);

            RefreshLobbyList();
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void HandleRefreshLobbyList() {
        if (UnityServices.State == ServicesInitializationState.Initialized && AuthenticationService.Instance.IsSignedIn) {
            refreshLobbyListTimer -= Time.deltaTime;
            if (refreshLobbyListTimer < 0f) {
                float refreshLobbyListTimerMax = 5f;
                refreshLobbyListTimer = refreshLobbyListTimerMax;

                RefreshLobbyList();
            }
        }
    }

    private async void HandleLobbyHeartbeat() {
        if (IsLobbyHost()) {
            heartbeatTimer -= Time.deltaTime;
            if (heartbeatTimer < 0f) {
                float heartbeatTimerMax = 300f;
                heartbeatTimer = heartbeatTimerMax;

                Debug.Log("Heartbeat");
                await LobbyService.Instance.SendHeartbeatPingAsync(joinedLobby.Id);
            }
        }
    }

    private void HandleDungeonGenOnStartUp()
    {
        if (GameStarted)
        {
            DungeonGeneratorObj = GameObject.FindGameObjectWithTag("Generator_Dungeon");
            DungeonGeneratorObj.GetComponent<Dungeon>().enableDungeonGeneration();
            GameStarted = false;
        }
        
    }
    private async void HandleLobbyPolling() {
        if (joinedLobby != null) {
            lobbyPollTimer -= Time.deltaTime;
            if (lobbyPollTimer < 0f) {
                float lobbyPollTimerMax = 1.1f;
                lobbyPollTimer = lobbyPollTimerMax;

                joinedLobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);

                OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });

                if (!IsPlayerInLobby()) {
                    // Player was kicked out of this lobby
                    Debug.Log("Kicked from Lobby!");

                    OnKickedFromLobby?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });

                    joinedLobby = null;
                }
                if (joinedLobby.Data[KEY_START_GAME].Value!="1")
                {
                    if (!IsLobbyHost())
                    {
                        TestRelayScript.JoinRelay(joinedLobby.Data[KEY_START_GAME].Value);
                    }
                    joinedLobby = null;
                    GameStarted = true;
                }
            }
        }
    }
   
    public Lobby GetJoinedLobby() {
        return joinedLobby;
    }

    public bool IsLobbyHost() {
        return joinedLobby != null && joinedLobby.HostId == AuthenticationService.Instance.PlayerId;
    }

    private bool IsPlayerInLobby() {
        if (joinedLobby != null && joinedLobby.Players != null) {
            foreach (Player player in joinedLobby.Players) {
                if (player.Id == AuthenticationService.Instance.PlayerId) {
                    // This player is in this lobby
                    return true;
                }
            }
        }
        return false;
    }

    private Player GetPlayer() {
        return new Player(AuthenticationService.Instance.PlayerId, null, new Dictionary<string, PlayerDataObject> {
            { KEY_PLAYER_NAME, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, playerName) },
            { KEY_PLAYER_CHARACTER, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, playerCharacterName) },
            { KEY_PLAYER_Position_Number, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, playerPosition) },
            { KEY_PLAYER_Attack_Number1, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, playerAttack1) },
            { KEY_PLAYER_Attack_Number2, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, playerAttack2) },
            { KEY_PLAYER_Attack_Number3, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, playerAttack3) }
        });
    }

    
    public async void CreateLobby(string lobbyName, int maxPlayers, bool isPrivate) {
        _maxPlayers = maxPlayers;
        Player player = GetPlayer();
        _Player = player;
        CreateLobbyOptions options = new CreateLobbyOptions
        {
            Player = player,
            Data = new Dictionary<string, DataObject>
            {
                {KEY_START_GAME, new DataObject(DataObject.VisibilityOptions.Member,"1")}
            }
        };

        Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);

        joinedLobby = lobby;

        OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });
    }


    //joinCodeWorks
    public async void JoinLobbyByCode(string lobbyCode)
    {

        try
        {
            Player player = GetPlayer();

            Lobby lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(lobbyCode, new JoinLobbyByCodeOptions
            {
                Player = player
            });
            joinedLobby = lobby;

            OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });
        }
        catch (LobbyServiceException e)
        {            
            if (e.Reason == LobbyExceptionReason.ValidationError)
            {
                this.message.SetActive(true);
                Debug.LogError($"Failed to join lobby with code {lobbyCode}: {e.Message}");
                StartCoroutine(setMSGBackToFalse());
            }
        }
    }
    IEnumerator setMSGBackToFalse()
    {
        yield return new WaitForSeconds(1.5f);
        this.message.SetActive(false);
    }

    public async void RefreshLobbyList() {
        try {
            QueryLobbiesOptions options = new QueryLobbiesOptions();
            options.Count = 25;

            // Filter for open lobbies only
            options.Filters = new List<QueryFilter> {
                new QueryFilter(
                    field: QueryFilter.FieldOptions.AvailableSlots,
                    op: QueryFilter.OpOptions.GT,
                    value: "0")
            };

            // Order by newest lobbies first
            options.Order = new List<QueryOrder> {
                new QueryOrder(
                    asc: false,
                    field: QueryOrder.FieldOptions.Created)
            };

            QueryResponse lobbyListQueryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            OnLobbyListChanged?.Invoke(this, new OnLobbyListChangedEventArgs { lobbyList = lobbyListQueryResponse.Results });
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }
    public async void UpdatePlayerName(string playerName) {
        this.playerName = playerName;

        if (joinedLobby != null) {
            try {
                UpdatePlayerOptions options = new UpdatePlayerOptions();

                options.Data = new Dictionary<string, PlayerDataObject>() {
                    {
                        KEY_PLAYER_NAME, new PlayerDataObject(
                            visibility: PlayerDataObject.VisibilityOptions.Public,
                            value: playerName)
                    }

                };

                string playerId = AuthenticationService.Instance.PlayerId;

                Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, options);
                joinedLobby = lobby;

                OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
            } catch (LobbyServiceException e) {
                Debug.Log(e);
            }
        }
    }
    public async void UpdatePlayerAttack_1(string playerAttack1)
    {
        this.playerAttack1 = playerAttack1;

        if (joinedLobby != null)
        {
            try
            {
                UpdatePlayerOptions options = new UpdatePlayerOptions();

                options.Data = new Dictionary<string, PlayerDataObject>() {
                    {
                        KEY_PLAYER_Attack_Number1, new PlayerDataObject(
                            visibility: PlayerDataObject.VisibilityOptions.Public,
                            value: playerAttack1)
                    }

                };

                string playerId = AuthenticationService.Instance.PlayerId;

                Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, options);
                joinedLobby = lobby;

                OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
    }
    public async void UpdatePlayerAttack_2(string playerAttack2)
    {
        this.playerAttack2 = playerAttack2;

        if (joinedLobby != null)
        {
            try
            {
                UpdatePlayerOptions options = new UpdatePlayerOptions();

                options.Data = new Dictionary<string, PlayerDataObject>() {
                    {
                        KEY_PLAYER_Attack_Number2, new PlayerDataObject(
                            visibility: PlayerDataObject.VisibilityOptions.Public,
                            value: playerAttack2)
                    }

                };

                string playerId = AuthenticationService.Instance.PlayerId;

                Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, options);
                joinedLobby = lobby;

                OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
    }
    public async void UpdatePlayerAttack_3(string playerAttack3)
    {
        this.playerAttack3 = playerAttack3;

        if (joinedLobby != null)
        {
            try
            {
                UpdatePlayerOptions options = new UpdatePlayerOptions();

                options.Data = new Dictionary<string, PlayerDataObject>() {
                    {
                        KEY_PLAYER_Attack_Number3, new PlayerDataObject(
                            visibility: PlayerDataObject.VisibilityOptions.Public,
                            value: playerAttack3)
                    }

                };

                string playerId = AuthenticationService.Instance.PlayerId;

                Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, options);
                joinedLobby = lobby;

                OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
    }
    public async void UpdatePlayerpos(string playerpos)
    {
        this.playerPosition = playerpos;

        if (joinedLobby != null)
        {
            try
            {
                UpdatePlayerOptions options = new UpdatePlayerOptions();

                options.Data = new Dictionary<string, PlayerDataObject>() {
                    {
                        KEY_PLAYER_Position_Number, new PlayerDataObject(
                            visibility: PlayerDataObject.VisibilityOptions.Public,
                            value: playerpos)
                    }

                };

                string playerId = AuthenticationService.Instance.PlayerId;

                Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, options);
                joinedLobby = lobby;

                OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
    }

    public async void UpdatePlayerCharacter(CharacterChoices playerCharacter) {
        if (joinedLobby != null) {
            try {
                UpdatePlayerOptions options = new UpdatePlayerOptions();

                options.Data = new Dictionary<string, PlayerDataObject>() {
                    {
                        KEY_PLAYER_CHARACTER, new PlayerDataObject(
                            visibility: PlayerDataObject.VisibilityOptions.Public,
                            value: playerCharacter.ToString())
                    }
                };

                string playerId = AuthenticationService.Instance.PlayerId;

                Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, options);
                joinedLobby = lobby;

                OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
            } catch (LobbyServiceException e) {
                Debug.Log(e);
            }
        }
    }

    
    public async void QuickJoinLobby() {
        try {
            QuickJoinLobbyOptions options = new QuickJoinLobbyOptions();

            Lobby lobby = await LobbyService.Instance.QuickJoinLobbyAsync(options);
            joinedLobby = lobby;

            OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });
        } catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }

    public async void LeaveLobby() {
        if (joinedLobby != null) {
            try {

                await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);

                joinedLobby = null;
                OnLeftLobby?.Invoke(this, EventArgs.Empty);

            }
            catch (LobbyServiceException e) {
                Debug.Log(e);
            }
        }
    }

    public async void KickPlayer(string playerId) {
        if (IsLobbyHost()) {
            try {
                await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, playerId);
            } catch (LobbyServiceException e) {
                Debug.Log(e);
            }
        }
    }
    
    public async void StartGame()
    {
        GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<AttackListHolder>().enabled=false;

        if (IsLobbyHost())
        {
            try
            {
                var relayCode = await TestRelayScript.CreateRelay(_maxPlayers);
                Lobby lobby = await Lobbies.Instance.UpdateLobbyAsync(joinedLobby.Id, new UpdateLobbyOptions
                {
                    Data = new Dictionary<string, DataObject>
                    {
                        {KEY_START_GAME, new DataObject(DataObject.VisibilityOptions.Member,relayCode) }
                    }
                });
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
        }
        
    }

}