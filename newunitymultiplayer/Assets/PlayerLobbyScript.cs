using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLobbyScript : MonoBehaviour
{
    [Header("Player Stuff")]
    public AttackInfo[] attackInfos = new AttackInfo[3];
    public GameObject prefabAttackToInstantiateHOLDER;
    public GameObject prefabAttackToInstantiate;
    [Header("UI Stuff")]
    public TMPro.TextMeshProUGUI PlayerNameText;
    public UnityEngine.UI.Image PlayerCharacterImage;
    public TMPro.TextMeshProUGUI PlayerCharacterName;
    public TMPro.TextMeshProUGUI PlayerhealthText;
    [SerializeField] Sprite[] CharacterImages;

    // Start is called before the first frame update
    void Start()
    {
        instantiateAttackInfos(attackInfos);
    }
    public void getCharImage(CharacterChoices CharChosen)
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
    public void instantiateAttackInfos(AttackInfo[] attackInfos)
    {
        foreach (AttackInfo item in attackInfos)
        {
            GameObject InstantiateObj = Instantiate(prefabAttackToInstantiate, prefabAttackToInstantiateHOLDER.transform);
            InstantiateObj.GetComponent<AttackLobbyScript>().attacksInfo = item;
            InstantiateObj.GetComponent<AttackLobbyScript>().AttackNameText.text = item.Name.Value.ToString();
        }
    }
}
