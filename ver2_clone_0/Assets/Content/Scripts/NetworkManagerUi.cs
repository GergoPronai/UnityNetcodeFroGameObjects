using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.Netcode;

public class NetworkManagerUi : MonoBehaviour
{
    [SerializeField] private Button ServerButton;
    [SerializeField] private Button HostButton;
    [SerializeField] private Button ClientButton;

    // Start is called before the first frame update
    void Awake()
    {
        ServerButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartServer();
        });
        HostButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
        });
        ClientButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
        });
    }
}
