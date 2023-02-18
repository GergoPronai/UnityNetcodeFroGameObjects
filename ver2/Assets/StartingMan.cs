using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class StartingMan : NetworkBehaviour
{
    public static StartingMan Instance;
    private GameObject dialogueBox;
    public Transform shopkeeper;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        dialogueBox=GameObject.FindGameObjectWithTag("DialogueBox");
    }

    // Update is called once per frame
    void Update()
    {
        shopkeeper.LookAt(NetworkManager.Singleton.LocalClient.PlayerObject.transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        dialogueBox.transform.GetChild(0).gameObject.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        dialogueBox.transform.GetChild(0).gameObject.SetActive(false);
        dialogueBox.GetComponent<Dialogue>().textMeshComponent.text = string.Empty;        
        dialogueBox.GetComponent<Dialogue>().StartDialogue();
    }
}
