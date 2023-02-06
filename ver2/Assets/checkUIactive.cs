using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkUIactive : MonoBehaviour
{
    public GameObject LobbyUIgameobject;
    public GameObject ChatWindow;
    // Update is called once per frame
    public static checkUIactive Instance;
    private void Start()
    {
        Instance = this;
    }
    void Update()
    {
        if (LobbyUIgameobject != null && LobbyUIgameobject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
    public void OnSubmit()
    {
        ChatWindow.SetActive(true);
        LobbyUI.Instance.ShowPLayerButtonVisibleForVoting = true;
        LobbyUI.Instance.UpdateLobby(LobbyUI.Instance.LobbyJoined);
    }
}
