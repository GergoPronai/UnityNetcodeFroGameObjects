using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetworkReplicateScript : NetworkBehaviour
{/// <summary>
 /// A toggle to test the difference between owner and server auth.
 /// </summary>
    [SerializeField] private bool _serverAuth;
    [SerializeField] private float _cheapInterpolationTime = 0.1f;

    private NetworkVariable<PlayerNetworkState> _playerState;
    public NetworkVariable<float> animSpeed;

    private void Awake()
    {
        var permission = _serverAuth ? NetworkVariableWritePermission.Server : NetworkVariableWritePermission.Owner;
        animSpeed = new NetworkVariable<float>(writePerm: permission);
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
            Rotation = transform.GetChild(0).rotation.eulerAngles,
        };
        if (IsServer || !_serverAuth)
        {
            _playerState.Value = state;
            animSpeed.Value = transform.GetComponent<PlayerMovement>().animSpeed;
        }
        else
            TransmitStateServerRpc(state);
    }

    [ServerRpc]
    private void TransmitStateServerRpc(PlayerNetworkState state)
    {
        _playerState.Value = state;
        animSpeed.Value = transform.GetComponent<PlayerMovement>().animSpeed;
    }

    #endregion

    #region Interpolate State

    private float _rotVelY;

    private void ConsumeState()
    {
        // Here you'll find the cheapest, dirtiest interpolation you'll ever come across. Please do better in your game
        transform.GetChild(0).rotation = Quaternion.Euler(
            0, Mathf.SmoothDampAngle(transform.GetChild(0).rotation.eulerAngles.y, _playerState.Value.Rotation.y, ref _rotVelY, _cheapInterpolationTime), 0);

        transform.GetComponent<PlayerMovement>().animSpeed = animSpeed.Value;
    }

    #endregion

    private struct PlayerNetworkState : INetworkSerializable
    {
        private short _rotY;

        internal Vector3 Rotation
        {
            get => new(0, _rotY, 0);
            set => _rotY = (short)value.y;
        }
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _rotY);
        }
    }
}

