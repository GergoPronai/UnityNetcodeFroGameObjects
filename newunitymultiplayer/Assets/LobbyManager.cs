using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class LobbyManager : NetworkBehaviour
{
    public TMPro.TextMeshProUGUI LobbyTitle;
    // Start is called before the first frame update
    void Start()
    {
        LobbyTitle.text = "Welcome to the lobby";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
