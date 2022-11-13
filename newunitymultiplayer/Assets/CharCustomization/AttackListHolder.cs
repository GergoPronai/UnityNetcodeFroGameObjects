using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CharacterChoices
{
    Barbarian,
    Knight,
    Mage,
    Rogue,
    None
}
public class AttackListHolder : MonoBehaviour
{
    
    public Sprite[] characterImages;
    public UnityEngine.UI.Image characterImage;
    private int ImgNum=1;
    public CharacterChoices character = CharacterChoices.Barbarian;
    public TMPro.TextMeshProUGUI NameText;
    [Header("AttackInfo Prefab Stuff")]
    public GameObject PlayerListItemPrefab;
    public GameObject prefabButton;
    public Transform AvailableholderOfButton;
    public Transform ChosenholderOfButton;
    [Header("Set AttackInfos For Available Characters")]
    public List<AttackInfo> BarabarianAttackInfos;
    public List<AttackInfo> KnightAttackInfos;
    public List<AttackInfo> MageAttackInfos;
    public List<AttackInfo> RogueAttackInfos;
    private GameObject prefabInstantiated=null;
    public TMPro.TextMeshProUGUI healthTxt=null;
    public int playerHealth = 100;
    public string PlayerName="";

    public void SetPlayerName(TMPro.TMP_InputField textField)
    {
        PlayerName = textField.text;
    }
    private void Start()
    {
        ChangeAttacks(character);
        characterImage.sprite = characterImages[0];
        healthTxt.text = "Health: "+ 150f.ToString();
    }
    public void ChangeCharacter(int amount)
    {
        ImgNum += amount;        
        if (ImgNum == 5)
        {
            ImgNum = 1;
        }
        else if (ImgNum == 0)
        {
            ImgNum = 4;
        }
        switch (ImgNum)
        {
            case 1:
                character = CharacterChoices.Barbarian;
                NameText.text = "Barbarian";
                playerHealth = 150;
                break;
            case 2:
                character = CharacterChoices.Knight;
                NameText.text = "Knight";
                playerHealth = 120;
                break;
            case 3:
                character = CharacterChoices.Mage;
                NameText.text = "Mage";
                playerHealth = 80;
                break;
            case 4:
                character = CharacterChoices.Rogue;
                NameText.text = "Rogue";
                playerHealth = 100;
                break;
        }
        healthTxt.text = "Health: "+playerHealth.ToString();
        ChangeAttacks(character);
        characterImage.sprite = characterImages[ImgNum-1];        
    }
    public void ChangeAttacks(CharacterChoices characterChosen)
    {
        switch (characterChosen)
        {
            case CharacterChoices.Barbarian:
                foreach (Transform child in AvailableholderOfButton)
                {
                    Destroy(child.gameObject);
                }
                foreach (Transform child in ChosenholderOfButton)
                {
                    Destroy(child.gameObject);
                }
                foreach (AttackInfo item in BarabarianAttackInfos)
                {
                    prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButton);
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttacksInfo = item;
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttackName.text = item.Name;

                    prefabInstantiated.GetComponent<MicroAttackManager>().enable();

                }
                break;
            case CharacterChoices.Knight:
                foreach (Transform child in AvailableholderOfButton)
                {
                    Destroy(child.gameObject);
                }
                foreach (Transform child in ChosenholderOfButton)
                {
                    Destroy(child.gameObject);
                }
                foreach (AttackInfo item in KnightAttackInfos)
                {
                    prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButton);
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttacksInfo = item;
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttackName.text = item.Name;
 
                    prefabInstantiated.GetComponent<MicroAttackManager>().enable();
                }
                break;
            case CharacterChoices.Mage:
                foreach (Transform child in AvailableholderOfButton)
                {
                    Destroy(child.gameObject);
                }
                foreach (Transform child in ChosenholderOfButton)
                {
                    Destroy(child.gameObject);
                }
                foreach (AttackInfo item in MageAttackInfos)
                {
                    prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButton);
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttacksInfo = item;
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttackName.text = item.Name;

                    prefabInstantiated.GetComponent<MicroAttackManager>().enable();

                }
                break;
            case CharacterChoices.Rogue:
                foreach (Transform child in AvailableholderOfButton)
                {
                    Destroy(child.gameObject);
                }
                foreach (Transform child in ChosenholderOfButton)
                {
                    Destroy(child.gameObject);
                }
                foreach (AttackInfo item in RogueAttackInfos)
                {
                    prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButton);
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttacksInfo = item;
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttackName.text = item.Name;

                    prefabInstantiated.GetComponent<MicroAttackManager>().enable();

                }
                break;
            default:
                break;
        }
    }
}
