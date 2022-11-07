using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLobbyScript : MonoBehaviour
{
    [Header("Player Stuff")]
    public List<AttackInfo> attackInfos = new List<AttackInfo>();
    public GameObject prefabAttackToInstantiateHOLDER;
    public GameObject prefabAttackToInstantiate;
    [Header("UI Stuff")]
    [SerializeField] TMPro.TextMeshProUGUI PlayerNameText;
    [SerializeField] UnityEngine.UI.Image PlayerCharacterImage;
    [SerializeField] TMPro.TextMeshProUGUI PlayerCharacterName;
    [SerializeField] TMPro.TextMeshProUGUI PlayerhealthText;
    [SerializeField] Sprite[] CharacterImages;

    // Start is called before the first frame update
    void Start()
    {
        PlayerNameText.text = GameObject.FindGameObjectWithTag("NetManager").GetComponent<Unity.Netcode.NetworkManager>().LocalClient.PlayerObject.gameObject.GetComponent<PlayergameObjScript>().PlayerName;
        PlayerhealthText.text = GameObject.FindGameObjectWithTag("NetManager").GetComponent<Unity.Netcode.NetworkManager>().LocalClient.PlayerObject.gameObject.GetComponent<PlayergameObjScript>().playerHealth.ToString();
        attackInfos = GameObject.FindGameObjectWithTag("NetManager").GetComponent<Unity.Netcode.NetworkManager>().LocalClient.PlayerObject.gameObject.GetComponent<PlayergameObjScript>().attackInfos;
        getCharImage(GameObject.FindGameObjectWithTag("NetManager").GetComponent<Unity.Netcode.NetworkManager>().LocalClient.PlayerObject.gameObject.GetComponent<PlayergameObjScript>().CharChosen);
    }
    void getCharImage(CharacterChoices CharChosen)
    {
        switch (CharChosen)
        {
            case CharacterChoices.Barbarian:
                PlayerCharacterImage.sprite = CharacterImages[0];
                PlayerCharacterName.text = "Barbarian";
                break;
            case CharacterChoices.Knight:
                PlayerCharacterImage.sprite = CharacterImages[1];
                PlayerCharacterName.text = "Knight";
                break;
            case CharacterChoices.Mage:
                PlayerCharacterImage.sprite = CharacterImages[2];
                PlayerCharacterName.text = "Mage";
                break;
            case CharacterChoices.Rogue:
                PlayerCharacterImage.sprite = CharacterImages[3];
                PlayerCharacterName.text = "Rogue";
                break;
        }
    }
    public void instantiateAttackInfos(List<AttackInfo> attackInfos)
    {

    }
}
