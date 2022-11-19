using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Collections;
using Unity.Netcode;
using System;

public class PlayerNetwork : NetworkBehaviour
{/// <summary>
 /// A toggle to test the difference between owner and server auth.
 /// </summary>
    [SerializeField] private bool _serverAuth;
    [SerializeField] private float _cheapInterpolationTime = 0.1f;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject[] barbarian;
    [SerializeField] private GameObject[] knight;
    [SerializeField] private GameObject[] mage;
    [SerializeField] private GameObject[] rogue;

    private NetworkVariable<PlayerNetworkState> _playerState;
    public NetworkVariable<float> animSpeed;
    public NetworkVariable<int> _Charchosen;
    
    public NetworkVariable<FixedString128Bytes> _CharName;
    public NetworkVariable<int> _CharHealth;
    public NetworkVariable<ulong> _clientID;
    public NetworkVariable<int> playersJoined;
    public NetworkVariable<CharacterChoices> charChosen;
    public NetworkVariable<int> attackIDs_1;
    public NetworkVariable<int> attackIDs_2;
    public NetworkVariable<int> attackIDs_3;
    private Rigidbody _rb;

    public GameObject instanObj;
    private GameObject instantiatedOBJ;
    public static PlayerNetwork instance;
    public void setUpLobby()
    {
        OnJoinServerRpc();
        GameObject border = GameObject.FindGameObjectWithTag("border");
        border.SetActive(false);

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _rb = GetComponent<Rigidbody>();
        var permission = _serverAuth ? NetworkVariableWritePermission.Server : NetworkVariableWritePermission.Owner;
        _playerState = new NetworkVariable<PlayerNetworkState>(writePerm: permission);
        animSpeed = new NetworkVariable<float>(writePerm: permission);
        _Charchosen = new NetworkVariable<int>(writePerm: permission);
        _CharName = new NetworkVariable<FixedString128Bytes>(writePerm: permission);
        _CharHealth = new NetworkVariable<int>(writePerm: permission);
        _clientID = new NetworkVariable<ulong>(writePerm: permission);
        playersJoined = new NetworkVariable<int>(writePerm: permission);

        charChosen = new NetworkVariable<CharacterChoices>(writePerm: permission);
        attackIDs_1 = new NetworkVariable<int>(writePerm: permission);
        attackIDs_2 = new NetworkVariable<int>(writePerm: permission);
        attackIDs_3 = new NetworkVariable<int>(writePerm: permission);

    }
    public void SetUpChar()
    {

        switch (_Charchosen.Value)
        {
            case 0:
                foreach (GameObject item in barbarian)
                {
                    item.SetActive(true);
                }
                foreach (GameObject item in knight)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in mage)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in rogue)
                {
                    item.SetActive(false);
                }
                break;
            case 1:
                foreach (GameObject item in barbarian)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in knight)
                {
                    item.SetActive(true);
                }
                foreach (GameObject item in mage)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in rogue)
                {
                    item.SetActive(false);
                }
                break;
            case 2:
                foreach (GameObject item in barbarian)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in knight)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in mage)
                {
                    item.SetActive(true);
                }
                foreach (GameObject item in rogue)
                {
                    item.SetActive(false);
                }
                break;
            case 3:
                foreach (GameObject item in barbarian)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in knight)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in mage)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in rogue)
                {
                    item.SetActive(true);
                }
                break;
        }
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsClient && IsOwner)
        {
            cam.gameObject.SetActive(true);
        }
        
    }
    private void Update()
    {
        if (IsOwner) TransmitState();
        else ConsumeState();
    }

    #region Transmit State

    private void TransmitState()
    {
        var state = new PlayerNetworkState
        {
            Position = _rb.position,
            Rotation = transform.GetChild(0).rotation.eulerAngles,
        };
        if (IsServer || !_serverAuth)
        {
            _playerState.Value = state;
            animSpeed.Value = transform.GetComponent<PlayerMovement>().animSpeed;
            _Charchosen.Value = transform.GetComponent<PlayergameObjScript>()._Charchosen;
            _CharName.Value = transform.GetComponent<PlayergameObjScript>().PlayerName;
            _CharHealth.Value = transform.GetComponent<PlayergameObjScript>().playerHealth;
            _clientID.Value = transform.GetComponent<PlayergameObjScript>().clientID;
            playersJoined.Value = transform.GetComponent<PlayergameObjScript>().playersJoined;
            charChosen.Value = transform.GetComponent<PlayergameObjScript>().CharChosen;

            attackIDs_1.Value = transform.GetComponent<PlayergameObjScript>().CharChosen_ChosenAttacks_1;
            attackIDs_2.Value = transform.GetComponent<PlayergameObjScript>().CharChosen_ChosenAttacks_2;
            attackIDs_3.Value = transform.GetComponent<PlayergameObjScript>().CharChosen_ChosenAttacks_3;
            SetUpChar();
        }
        else
            TransmitStateServerRpc(state);
    }

   

    [ServerRpc(RequireOwnership = false)]
    private void OnJoinServerRpc()
    {
        for (int i = 0; i < NetworkManagerUiMain.instance.instanObjHolder.transform.childCount; i++)
        {
            Destroy(NetworkManagerUiMain.instance.instanObjHolder.transform.GetChild(i));
        }
        foreach (var client in NetworkManager.ConnectedClientsList)
        {
            if (client.PlayerObject != NetworkManager.Singleton.LocalClient.PlayerObject)
            {
                instantiatedOBJ = Instantiate(instanObj, NetworkManagerUiMain.instance.instanObjHolder.transform);
                instantiatedOBJ.GetComponent<TMPro.TextMeshProUGUI>().text = $"{client.PlayerObject.GetComponent<PlayergameObjScript>().PlayerName} > Has Joined";
                Destroy(instantiatedOBJ.gameObject, 60);
            }

        }

    }
    [ServerRpc] 
    private void TransmitStateServerRpc(PlayerNetworkState state)
    {
        _playerState.Value = state;
        animSpeed.Value = transform.GetComponent<PlayerMovement>().animSpeed;
        _Charchosen.Value = transform.GetComponent<PlayergameObjScript>()._Charchosen;
        _CharName.Value = transform.GetComponent<PlayergameObjScript>().PlayerName;
        _CharHealth.Value = transform.GetComponent<PlayergameObjScript>().playerHealth;
        playersJoined.Value = transform.GetComponent<PlayergameObjScript>().playersJoined;
        charChosen.Value = transform.GetComponent<PlayergameObjScript>().CharChosen;

        attackIDs_1.Value = transform.GetComponent<PlayergameObjScript>().CharChosen_ChosenAttacks_1;
        attackIDs_2.Value = transform.GetComponent<PlayergameObjScript>().CharChosen_ChosenAttacks_2;
        attackIDs_3.Value = transform.GetComponent<PlayergameObjScript>().CharChosen_ChosenAttacks_3;

        SetUpChar();
    }

    #endregion

    #region Interpolate State

    private Vector3 _posVel;
    private float _rotVelY;

    private void ConsumeState()
    {
        // Here you'll find the cheapest, dirtiest interpolation you'll ever come across. Please do better in your game
        _rb.MovePosition(Vector3.SmoothDamp(_rb.position, _playerState.Value.Position, ref _posVel, _cheapInterpolationTime));

        transform.GetChild(0).rotation = Quaternion.Euler(
            0, Mathf.SmoothDampAngle(transform.GetChild(0).rotation.eulerAngles.y, _playerState.Value.Rotation.y, ref _rotVelY, _cheapInterpolationTime), 0);

        transform.GetComponent<PlayerMovement>().animSpeed = animSpeed.Value;
        transform.GetComponent<PlayergameObjScript>()._Charchosen = _Charchosen.Value;
        transform.GetComponent<PlayergameObjScript>().PlayerName = _CharName.Value.ToString();
        transform.GetComponent<PlayergameObjScript>().playerHealth = _CharHealth.Value;
        transform.GetComponent<PlayergameObjScript>().playersJoined = playersJoined.Value;

        transform.GetComponent<PlayergameObjScript>().CharChosen_ChosenAttacks_1 = attackIDs_1.Value;
        transform.GetComponent<PlayergameObjScript>().CharChosen_ChosenAttacks_2 = attackIDs_2.Value;
        transform.GetComponent<PlayergameObjScript>().CharChosen_ChosenAttacks_3 = attackIDs_3.Value;

        SetUpChar();
    }

    #endregion

    private struct PlayerNetworkState : INetworkSerializable
    {
        private float _posX, _posZ;
        private short _rotY;

        internal Vector3 Position
        {
            get => new Vector3(_posX, 0, _posZ);
            set
            {
                _posX = value.x;
                _posZ = value.z;
            }
        }

        internal Vector3 Rotation
        {
            get => new Vector3 (0, _rotY, 0);
            set => _rotY = (short)value.y;
        }
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _posX);
            serializer.SerializeValue(ref _posZ);

            serializer.SerializeValue(ref _rotY);
        }
    }
}

