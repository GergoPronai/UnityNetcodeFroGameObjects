﻿using System.Collections;
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
    public Transform[] AvailableholderOfButtons;
    public Transform ChosenholderOfButton;
    [Header("Set AttackInfos For Available Characters")]
    public List<AttackInfo> BarabarianAttackInfos;
    public List<AttackInfo> KnightAttackInfos;
    public List<AttackInfo> MageAttackInfos;
    public List<AttackInfo> RogueAttackInfos;
    private GameObject prefabInstantiated=null;
    public TMPro.TextMeshProUGUI healthTxt=null;
    public float playerHealth = 100f;

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
        int idnum = 0;
        switch (characterChosen)
        {
            case CharacterChoices.Barbarian:
                idnum = 0;
                foreach (Transform child in AvailableholderOfButtons)
                {
                    foreach (Transform child2 in child)
                    {
                        Destroy(child2.gameObject);
                    }
                }
                foreach (Transform child in ChosenholderOfButton)
                {
                    Destroy(child.gameObject);
                }
                foreach (AttackInfo item in BarabarianAttackInfos)
                {
                    switch (item.Position)
                    {
                        
                        case PositionItCanBeUsedIn.First:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[0]);
                            break;
                        case PositionItCanBeUsedIn.Second:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[1]);
                            break;
                        case PositionItCanBeUsedIn.Third:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[2]);
                            break;
                        case PositionItCanBeUsedIn.Fourth:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[3]);
                            break;
                        case PositionItCanBeUsedIn.All:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[4]);
                            break;
                        default:
                            break;
                    }
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttacksInfo = item;
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttackName.text = item.Name;

                    prefabInstantiated.GetComponent<MicroAttackManager>().enable();
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttackIDNumber = idnum;
                    idnum++;

                }
                break;
            case CharacterChoices.Knight:
                idnum = 0;
                foreach (Transform child in AvailableholderOfButtons)
                {
                    foreach (Transform child2 in child)
                    {
                        Destroy(child2.gameObject);
                    }
                }
                foreach (Transform child in ChosenholderOfButton)
                {
                    Destroy(child.gameObject);
                }
                foreach (AttackInfo item in KnightAttackInfos)
                {
                    switch (item.Position)
                    {

                        case PositionItCanBeUsedIn.First:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[0]);
                            break;
                        case PositionItCanBeUsedIn.Second:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[1]);
                            break;
                        case PositionItCanBeUsedIn.Third:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[2]);
                            break;
                        case PositionItCanBeUsedIn.Fourth:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[3]);
                            break;
                        case PositionItCanBeUsedIn.All:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[4]);
                            break;
                        default:
                            break;
                    }
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttacksInfo = item;
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttackName.text = item.Name;
 
                    prefabInstantiated.GetComponent<MicroAttackManager>().enable();
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttackIDNumber = idnum;
                    idnum++;
                }
                break;
            case CharacterChoices.Mage:
                foreach (Transform child in AvailableholderOfButtons)
                {
                    foreach (Transform child2 in child)
                    {
                        Destroy(child2.gameObject);
                    }
                }
                foreach (Transform child in ChosenholderOfButton)
                {
                    Destroy(child.gameObject);
                }
                foreach (AttackInfo item in MageAttackInfos)
                {
                    switch (item.Position)
                    {
                        case PositionItCanBeUsedIn.First:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[0]);
                            break;
                        case PositionItCanBeUsedIn.Second:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[1]);
                            break;
                        case PositionItCanBeUsedIn.Third:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[2]);
                            break;
                        case PositionItCanBeUsedIn.Fourth:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[3]);
                            break;
                        case PositionItCanBeUsedIn.All:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[4]);
                            break;
                        default:
                            break;
                    }
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttacksInfo = item;
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttackName.text = item.Name;

                    prefabInstantiated.GetComponent<MicroAttackManager>().enable();
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttackIDNumber = idnum;
                    idnum++;
                }
                break;
            case CharacterChoices.Rogue:
                idnum = 0;
                foreach (Transform child in AvailableholderOfButtons)
                {
                    foreach (Transform child2 in child)
                    {
                        Destroy(child2.gameObject);
                    }
                }
                foreach (Transform child in ChosenholderOfButton)
                {
                    Destroy(child.gameObject);
                }
                foreach (AttackInfo item in RogueAttackInfos)
                {
                    switch (item.Position)
                    {
                        case PositionItCanBeUsedIn.First:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[0]);
                            break;
                        case PositionItCanBeUsedIn.Second:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[1]);
                            break;
                        case PositionItCanBeUsedIn.Third:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[2]);
                            break;
                        case PositionItCanBeUsedIn.Fourth:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[3]);
                            break;
                        case PositionItCanBeUsedIn.All:
                            prefabInstantiated = Instantiate(prefabButton, AvailableholderOfButtons[4]);
                            break;
                        default:
                            break;
                    }
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttacksInfo = item;
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttackName.text = item.Name;

                    prefabInstantiated.GetComponent<MicroAttackManager>().enable();
                    prefabInstantiated.GetComponent<MicroAttackManager>().AttackIDNumber=idnum;
                    idnum++;
                }
                break;
            default:
                break;
        }
    }
}
