using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class StartingMan : NetworkBehaviour
{
    public static StartingMan Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(NetworkManager.Singleton.LocalClient.PlayerObject.transform);
    }
}
