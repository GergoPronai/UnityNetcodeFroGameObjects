using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class PlayergameObjScript : NetworkBehaviour
{
    [Header("UI Stuff")]
    public Slider HealthBar;
    [Header("CharInfo Stuff")]
    public GameObject[] barbarian;
    public GameObject[] knight;
    public GameObject[] mage;
    public GameObject[] rogue;
    public List<AttackInfo> attackInfos = new List<AttackInfo>();
    public int votes_Cast = 0;
    [Header("Local Player Stuff")]
    public string PlayerName;
    public float currentHealth;
    public float playerHealth;
    public int _Charchosen;
    public CharacterChoices CharChosen = CharacterChoices.None;
    public int CharChosen_ChosenAttacks_1=0;
    public int CharChosen_ChosenAttacks_2=0;
    public int CharChosen_ChosenAttacks_3=0;
    public ulong clientID;
    public int playerPositionInBattle = 0;
    public GameObject battleCamCanvas;
    public bool HasPosition=false;
    private PlayerAttackInfosAndChosenAttackNumbers PlayerAttackInfosAndChosenAttackNumbers_script;
    private GameObject LobbyUIPage;
    [Header("Camera Stuff")]
    public GameObject playerCamera;
    public GameObject Missed;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        //unoptimized but will do
        setPositionScript.Instance.PlayersInScene++;
        setPositionScript.Instance.playersReadyinRoom.text = setPositionScript.Instance.hasPlayersSetPosition + "/" + setPositionScript.Instance.PlayersInScene + " are Ready";
    }
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        Destroy(this.gameObject);
    }
    public void disable()
    {
        HealthBar.gameObject.SetActive(true);
    }
    public void enable()
    {
        HealthBar.gameObject.SetActive(false);
        battleCamCanvas.SetActive(false);
    }
    public void Start()
    {        
        enable();
        checkUIactive.Instance.OnSubmit();
        attackInfos = GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<PlayerAttackInfosAndChosenAttackNumbers>().attackInfos;
        CharChosen_ChosenAttacks_1 = GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<PlayerAttackInfosAndChosenAttackNumbers>().CharChosen_ChosenAttacks_1;
        CharChosen_ChosenAttacks_2 = GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<PlayerAttackInfosAndChosenAttackNumbers>().CharChosen_ChosenAttacks_2;
        CharChosen_ChosenAttacks_3 = GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<PlayerAttackInfosAndChosenAttackNumbers>().CharChosen_ChosenAttacks_3;
        SetUpCharacterFromLobby(GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<PlayerAttackInfosAndChosenAttackNumbers>().character);
        PlayerAttackInfosAndChosenAttackNumbers_script = GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<PlayerAttackInfosAndChosenAttackNumbers>();
        attackInfos = PlayerAttackInfosAndChosenAttackNumbers_script.attackInfos;
        CharChosen_ChosenAttacks_1 = PlayerAttackInfosAndChosenAttackNumbers_script.CharChosen_ChosenAttacks_1;
        CharChosen_ChosenAttacks_2 = PlayerAttackInfosAndChosenAttackNumbers_script.CharChosen_ChosenAttacks_2;
        CharChosen_ChosenAttacks_3 = PlayerAttackInfosAndChosenAttackNumbers_script.CharChosen_ChosenAttacks_3;
        PlayerName = PlayerAttackInfosAndChosenAttackNumbers_script.PlayerName;
        currentHealth = playerHealth = PlayerAttackInfosAndChosenAttackNumbers_script.PlayerHealth;
        LobbyUIPage = GameObject.FindGameObjectWithTag("LobbyPage");

        //transform.GetComponent<PlayerMovement>().allowedMove = false;
        StartCoroutine(StartGameWaitCycle(2));

    }
    IEnumerator StartGameWaitCycle(int sec)
    {
        yield return new WaitForSeconds(sec);
        StartUp();
        disable();
    }
    public void StartUp()
    {
        if (LobbyUIPage!=null)
        {
            LobbyUIPage.SetActive(false);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, playerHealth);
        HealthBar.value = currentHealth / playerHealth;
    }
    public void SetPlayerName(TMPro.TMP_InputField textField)
    {
        PlayerName = textField.text;
    }
    public void SetUpCharacterFromLobby(CharacterChoices charChoice)
    {
        CharChosen = charChoice;
        switch (charChoice)
        {
            case CharacterChoices.Barbarian:
                foreach (GameObject item in barbarian)
                {
                    item.SetActive(true);
                }
                foreach (GameObject item in knight)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in mage)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in rogue)
                {
                    item.SetActive(false);
                }
                _Charchosen = 0;
                break;
            case CharacterChoices.Knight:
                foreach (GameObject item in barbarian)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in knight)
                {
                    item.SetActive(true);
                }
                foreach (GameObject item in mage)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in rogue)
                {
                    item.SetActive(false);
                }
                _Charchosen = 1;
                break;
            case CharacterChoices.Mage:
                foreach (GameObject item in barbarian)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in knight)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in mage)
                {
                    item.SetActive(true);
                }
                foreach (GameObject item in rogue)
                {
                    item.SetActive(false);
                }
                _Charchosen = 2;
                break;
            case CharacterChoices.Rogue:
                foreach (GameObject item in barbarian)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in knight)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in mage)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in rogue)
                {
                    item.SetActive(true);
                }
                _Charchosen = 3;
                break;
        }
    }
    public void miss()
    {
        Missed.gameObject.SetActive(true);
        StartCoroutine(WaitForSeconds());
    }
    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(2);
        Missed.gameObject.SetActive(false);
    }
}
