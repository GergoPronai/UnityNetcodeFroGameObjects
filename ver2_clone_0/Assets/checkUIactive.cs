using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkUIactive : MonoBehaviour
{
    public GameObject LobbyUIgameobject;
    public GameObject ChatWindow;
    public GameObject Cover;
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
        Cover.SetActive(false);
    }
}
