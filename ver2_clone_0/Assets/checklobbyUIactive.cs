using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checklobbyUIactive : MonoBehaviour
{
    public GameObject LobbyUI;

    // Update is called once per frame
    
    void Update()
    {
        if (LobbyUI!=null && LobbyUI.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
}
