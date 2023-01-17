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
    [SerializeField] public GameObject instanObjHolder;
    public string PlayerName;
    public GameObject DungeonGeneratorObj;
    public ulong PlayerID;
    public string Seed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DungeonGeneratorObj.GetComponent<Dungeon>().enableDungeonGeneration();
        Seed = DungeonGeneratorObj.GetComponent<Seed>().GameSeed;
    }
    public void SetPlayerName(TMPro.TMP_InputField textField)
    {
        PlayerName = textField.text;
    }
    public void SpawnPlayer()
    {
        NetworkManager.Singleton.StartHost();
    }
}
