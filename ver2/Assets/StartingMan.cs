using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class StartingMan : NetworkBehaviour
{
    public static StartingMan Instance;
    private GameObject dialogueBox;
    private Transform shopkeeper;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        dialogueBox=GameObject.FindGameObjectWithTag("DialogueBox");
        shopkeeper = transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.shopkeeper != null && NetworkManager.Singleton.LocalClient != null && NetworkManager.Singleton.LocalClient.PlayerObject != null)
        {
            shopkeeper.LookAt(NetworkManager.Singleton.LocalClient.PlayerObject.transform);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (this.shopkeeper != null && other.gameObject == NetworkManager.Singleton.LocalClient.PlayerObject.gameObject)
        {
            this.dialogueBox.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (this.shopkeeper != null)
        {
            this.dialogueBox.transform.GetChild(0).gameObject.SetActive(false);
            this.dialogueBox.GetComponent<Dialogue>().textMeshComponent.text ="Interact with Tutorial man?";
            this.dialogueBox.GetComponent<Dialogue>().StartDialogue();
        }
    }
}
