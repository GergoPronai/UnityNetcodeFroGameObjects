using Netcode.Transports.Facepunch;
using Steamworks;
using Steamworks.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine;
public enum NetworkTransportType
{
    UnityTransport,
    UNET,
    facepunch,
    steamworks_net
}
public class NetworkManagerUiMain : MonoBehaviour
{
    [SerializeField] private Canvas LobbyCanvas;


    [SerializeField] private Button HostButton;
    [SerializeField] private Button ClientButton;
    private string IPAddress=null;
    private void Awake()
    {
        HostButton.onClick.AddListener(() => {
            if (IPAddress != null)
            {
                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetComponent<Unity.Netcode.Transports.UNET.UNetTransport>().ConnectAddress = IPAddress;
                NetworkManager.Singleton.StartHost();
                LobbyCanvas.gameObject.SetActive(false);
            }
        });
        ClientButton.onClick.AddListener(() => {
            if (IPAddress != null)
            {
                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetComponent<Unity.Netcode.Transports.UNET.UNetTransport>().ConnectAddress = IPAddress;
                NetworkManager.Singleton.StartClient();
                LobbyCanvas.gameObject.SetActive(false);
            }
        });
    }
    public void EnterIpAddress(TMPro.TMP_InputField textfield)
    {
        IPAddress = textfield.text;
    }
}
