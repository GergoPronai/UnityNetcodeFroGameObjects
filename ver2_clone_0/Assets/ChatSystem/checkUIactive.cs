using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkUIactive : MonoBehaviour
{
    public GameObject LobbyUIgameobject;
    public GameObject LoadingScreen;
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
            LoadingScreen.SetActive(false);
        }
    }
    public void OnSubmit()
    {
        ChatWindow.SetActive(true);
    }
}
