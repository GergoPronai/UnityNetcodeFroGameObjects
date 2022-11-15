using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public NetworkVariable<WeaponType> _charWeaponType1;
    public NetworkVariable<WeaponType> _charWeaponType2;
    public NetworkVariable<WeaponType> _charWeaponType3;
    public NetworkVariable<int> _CharHealth;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        var permission = _serverAuth ? NetworkVariableWritePermission.Server : NetworkVariableWritePermission.Owner;
        _playerState = new NetworkVariable<PlayerNetworkState>(writePerm: permission);
        animSpeed = new NetworkVariable<float>(writePerm: permission);
        _Charchosen = new NetworkVariable<int>(writePerm: permission);
        _CharName = new NetworkVariable<FixedString128Bytes>(writePerm: permission);
        _CharHealth = new NetworkVariable<int>(writePerm: permission);
        _charWeaponType1 = new NetworkVariable<WeaponType>(writePerm: permission);
        _charWeaponType2 = new NetworkVariable<WeaponType>(writePerm: permission);
        _charWeaponType3 = new NetworkVariable<WeaponType>(writePerm: permission);

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
            _charWeaponType1.Value = transform.GetComponent<PlayergameObjScript>().attackInfos[0].weaponType;
            _charWeaponType2.Value = transform.GetComponent<PlayergameObjScript>().attackInfos[1].weaponType;
            _charWeaponType3.Value = transform.GetComponent<PlayergameObjScript>().attackInfos[2].weaponType;
            SetUpChar();
        }
        else
            TransmitStateServerRpc(state);
    }

    [ServerRpc]
    private void TransmitStateServerRpc(PlayerNetworkState state)
    {
        _playerState.Value = state;
        animSpeed.Value = transform.GetComponent<PlayerMovement>().animSpeed;
        _Charchosen.Value = transform.GetComponent<PlayergameObjScript>()._Charchosen;
        _CharName.Value = transform.GetComponent<PlayergameObjScript>().PlayerName;
        _charWeaponType1.Value = transform.GetComponent<PlayergameObjScript>().attackInfos[0].weaponType;
        _charWeaponType2.Value = transform.GetComponent<PlayergameObjScript>().attackInfos[1].weaponType;
        _charWeaponType3.Value = transform.GetComponent<PlayergameObjScript>().attackInfos[2].weaponType;
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

        transform.GetComponent<PlayergameObjScript>().attackInfos[0].weaponType = _charWeaponType1.Value;
        transform.GetComponent<PlayergameObjScript>().attackInfos[1].weaponType = _charWeaponType2.Value;
        transform.GetComponent<PlayergameObjScript>().attackInfos[2].weaponType = _charWeaponType3.Value;
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

