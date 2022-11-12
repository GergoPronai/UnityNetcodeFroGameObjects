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
    public NetworkList<FixedString32Bytes> _playerInfos_names;
    public NetworkList<FixedString32Bytes> _playerInfos_description;
    public NetworkVariable<FixedString32Bytes> _playerInfos_PositionItCanBeUsedIn;
    public NetworkList<FixedString32Bytes> _playerInfos_weaponType;
    public NetworkList<int> _playerInfos_numberOfTargetsIfApplicable;
    public NetworkList<int> _playerInfos_Damage;
    public NetworkList<int> _playerInfos_Accuracy;
    public NetworkList<int> _playerInfos_CritChance;
    public NetworkList<FixedString32Bytes> _playerInfos_AffectedStats;
    public NetworkList<FixedString32Bytes> _playerInfos_StatRemovedIfApplicable;
    public NetworkList<int> _playerInfos_AffectStatAmountIfApplicable;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        var permission = _serverAuth ? NetworkVariableWritePermission.Server : NetworkVariableWritePermission.Owner;
        _playerState = new NetworkVariable<PlayerNetworkState>(writePerm: permission);
        animSpeed = new NetworkVariable<float>(writePerm: permission);
        _Charchosen = new NetworkVariable<int>(writePerm: permission);

        _playerInfos_names = new NetworkList<FixedString32Bytes>();
        _playerInfos_description = new NetworkList<FixedString32Bytes>();
        _playerInfos_weaponType = new NetworkList<FixedString32Bytes>();
        _playerInfos_PositionItCanBeUsedIn = new NetworkVariable<FixedString32Bytes>();
        _playerInfos_numberOfTargetsIfApplicable = new NetworkList<int>();
        _playerInfos_Damage = new NetworkList<int>();
        _playerInfos_Accuracy = new NetworkList<int>();
        _playerInfos_CritChance = new NetworkList<int>();
        _playerInfos_AffectStatAmountIfApplicable = new NetworkList<int>();
        _playerInfos_AffectedStats = new NetworkList<FixedString32Bytes>();
        _playerInfos_StatRemovedIfApplicable = new NetworkList<FixedString32Bytes>();
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
            foreach (AttackInfo item in transform.GetComponent<PlayergameObjScript>().attackInfos)
            {
                _playerInfos_names.Add(item.Name);
                _playerInfos_description.Add(item.Description);
                _playerInfos_weaponType.Add(item.weaponType.ToString());
                _playerInfos_numberOfTargetsIfApplicable.Add(item.numberOfTargetsIfApplicable);
                _playerInfos_Damage.Add(item.Damage);
                _playerInfos_Accuracy.Add(item.Accuracy);
                _playerInfos_CritChance.Add(item.CritChance);
                _playerInfos_AffectStatAmountIfApplicable.Add(item.AffectStatAmountIfApplicable);
                _playerInfos_AffectedStats.Add(item.AffectedStats.ToString());
                _playerInfos_StatRemovedIfApplicable.Add(item.StatRemovedIfApplicable.ToString());
            }
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
        foreach (AttackInfo item in transform.GetComponent<PlayergameObjScript>().attackInfos)
        {
            _playerInfos_names.Add(item.Name);
            _playerInfos_description.Add(item.Description);
            _playerInfos_weaponType.Add(item.weaponType.ToString());
            _playerInfos_numberOfTargetsIfApplicable.Add(item.numberOfTargetsIfApplicable);
            _playerInfos_Damage.Add(item.Damage);
            _playerInfos_Accuracy.Add(item.Accuracy);
            _playerInfos_CritChance.Add(item.CritChance);
            _playerInfos_AffectStatAmountIfApplicable.Add(item.AffectStatAmountIfApplicable);
            _playerInfos_AffectedStats.Add(item.AffectedStats.ToString());
            _playerInfos_StatRemovedIfApplicable.Add(item.StatRemovedIfApplicable.ToString());
        }
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
        SetUpChar();
        for (int i = 0; i < transform.GetComponent<PlayergameObjScript>().attackInfos.Length; i++)
        {
            transform.GetComponent<PlayergameObjScript>().attackInfos[i].Name =_playerInfos_names[i];
            transform.GetComponent<PlayergameObjScript>().attackInfos[i].Description = _playerInfos_description[i];
            transform.GetComponent<PlayergameObjScript>().attackInfos[i].weaponType = Enum.Parse<WeaponType>(_playerInfos_weaponType[i].ToString());
            for (int j = 0; j < _playerInfos_AffectedStats.Count; j++)
            {
                transform.GetComponent<PlayergameObjScript>().attackInfos[i].AffectedStats[j] = Enum.Parse<StatInflicted>(_playerInfos_AffectedStats[j].ToString());
            }
            for (int k = 0; k < _playerInfos_StatRemovedIfApplicable.Count; k++)
            {
                transform.GetComponent<PlayergameObjScript>().attackInfos[i].StatRemovedIfApplicable[k] = Enum.Parse<StatInflicted>(_playerInfos_AffectedStats[k].ToString());
            }
            transform.GetComponent<PlayergameObjScript>().attackInfos[i].numberOfTargetsIfApplicable = _playerInfos_numberOfTargetsIfApplicable[i];
            transform.GetComponent<PlayergameObjScript>().attackInfos[i].Damage = _playerInfos_Damage[i];
            transform.GetComponent<PlayergameObjScript>().attackInfos[i].Accuracy = _playerInfos_Accuracy[i];
            transform.GetComponent<PlayergameObjScript>().attackInfos[i].CritChance = _playerInfos_CritChance[i];
            transform.GetComponent<PlayergameObjScript>().attackInfos[i].AffectStatAmountIfApplicable = _playerInfos_AffectStatAmountIfApplicable[i];
            
        }
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

