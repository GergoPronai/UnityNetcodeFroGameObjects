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
    [SerializeField] private GameObject LoginPage;

    public static NetworkManagerUiMain instance;
    [SerializeField] private Button HostButton;
    [SerializeField] private Button ClientButton;
    [SerializeField] public GameObject instanObjHolder;
    private string IPAddress="127.0.0.1";//set to local network ny default
    public string PlayerName;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        HostButton.onClick.AddListener(() => {
            if (IPAddress != null)
            {
                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetComponent<Unity.Netcode.Transports.UNET.UNetTransport>().ConnectAddress = IPAddress;
                NetworkManager.Singleton.StartHost();
                LoginPage.SetActive(false);
            }
        });
        ClientButton.onClick.AddListener(() => {
            if (IPAddress != null)
            {
                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetComponent<Unity.Netcode.Transports.UNET.UNetTransport>().ConnectAddress = IPAddress;
                NetworkManager.Singleton.StartClient();
                LoginPage.SetActive(false);
            }
        });
    }
    public void EnterIpAddress(TMPro.TMP_InputField textfield)
    {
        IPAddress = textfield.text;
    }
    public void SetPlayerName(TMPro.TMP_InputField textField)
    {
        PlayerName = textField.text;
    }
}
