using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Authentication;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using System.Threading.Tasks;
using Unity.Networking.Transport.Relay;

public class TestRelay : MonoBehaviour
{
// Start is called before the first frame update
// Update is called once per frame
    public async Task<string> CreateRelay(int maxConnections)
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
            var joincode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
                );

            NetworkManager.Singleton.StartHost();
            return joincode;
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
            return null;
        }
    }
    public async void JoinRelay(string joincode)
    {
        try
        {
            JoinAllocation join_allocation = await RelayService.Instance.JoinAllocationAsync(joincode);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(
                join_allocation.RelayServer.IpV4,
                (ushort)join_allocation.RelayServer.Port,
                join_allocation.AllocationIdBytes,
                join_allocation.Key,
                join_allocation.ConnectionData,
                join_allocation.HostConnectionData
                );
            NetworkManager.Singleton.StartClient();

        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
}
