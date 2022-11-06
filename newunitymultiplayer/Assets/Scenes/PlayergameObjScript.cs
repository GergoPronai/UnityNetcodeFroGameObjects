using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class PlayergameObjScript : NetworkBehaviour
{
    [Header("healthBar Stuff")]
    public Slider HealthBar;
    [Header("CharInfo Stuff")]
    public CharacterChoices characterChoice;
    public GameObject[] barbarian;
    public GameObject[] knight;
    public GameObject[] mage;
    public GameObject[] rogue;
    public List<AttackInfo> attackInfos = new List<AttackInfo>();
    public int votes_Cast = 0;
    [Header("Local Player Stuff")]
    public ulong LocalClientid;

    public void disable()
    {
        HealthBar.gameObject.SetActive(true);
    }
    public void enable()
    {
        HealthBar.gameObject.SetActive(false);
    }
    private void Start()
    {
        LocalClientid = NetworkManager.LocalClient.PlayerObject.OwnerClientId;
        enable();
    }
    public void checkChosenAttacks(Transform ChosenHolder, MicroAttackManager microAttackManager)
    {
        if (ChosenHolder.childCount == 3)
        {
            NetworkManager.LocalClient.PlayerObject.GetComponent<PlayergameObjScript>().attackInfos.Add(ChosenHolder.transform.GetChild(0).GetComponent<MicroAttackManager>().AttacksInfo);
            NetworkManager.LocalClient.PlayerObject.GetComponent<PlayergameObjScript>().attackInfos.Add(ChosenHolder.transform.GetChild(1).GetComponent<MicroAttackManager>().AttacksInfo);
            NetworkManager.LocalClient.PlayerObject.GetComponent<PlayergameObjScript>().attackInfos.Add(ChosenHolder.transform.GetChild(2).GetComponent<MicroAttackManager>().AttacksInfo);
            microAttackManager.ReadyUpButton.interactable = true;
        }
        else
        {
            microAttackManager.ReadyUpButton.interactable = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
