using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checklobbyUIactive : MonoBehaviour
{
    public GameObject LobbyUI;

    // Update is called once per frame
    private void Start()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (LobbyUI!=null && LobbyUI.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
}
