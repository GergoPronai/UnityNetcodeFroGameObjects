using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkUIactive : MonoBehaviour
{
    public GameObject LobbyUI;
    public GameObject ChatWindow;
    // Update is called once per frame

    void Update()
    {
        if (LobbyUI != null && LobbyUI.activeInHierarchy)
        {
            gameObject.SetActive(false);
            ChatWindow.SetActive(true);
        }
    }
}
