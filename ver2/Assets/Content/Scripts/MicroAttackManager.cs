﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.EventSystems;
using System;

public class MicroAttackManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMPro.TextMeshProUGUI AttackName;
    public GameObject PrefabbedExtraInfo;
    public AttackInfo AttacksInfo;
    private bool switchVar=false;
    private Transform ChosenHolder;
    private Transform AvailableHolder;
    public UnityEngine.UI.Button ReadyUpButton;
    private GameObject InstantiatedObj;
    public AttackListHolder characterCustomizer_AttackListHolder = null;
    public PlayerAttackInfosAndChosenAttackNumbers characterCustomizer_PlayerAttackInfosAndChosenAttackNumbers = null;
    [Header("Player Manager")]
    public Unity.Netcode.NetworkManager NetManager;
    public int AttackIDNumber = 0;

    public static Action<AttackInfo, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;

    private void OnEnable()
    {
        OnMouseHover += ShowToolTip;
        OnMouseLoseFocus += HideToolTip;
    }
    private void OnDisable()
    {
        OnMouseHover -= ShowToolTip;
        OnMouseLoseFocus -= HideToolTip;
    }
    public void enable()
    {
        if (ReadyUpButton!=null)
        {
            ReadyUpButton.interactable = false;
        }
        NetManager = GameObject.FindGameObjectWithTag("NetManager").GetComponent<NetworkManager>();
        characterCustomizer_AttackListHolder = GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<AttackListHolder>();
        characterCustomizer_PlayerAttackInfosAndChosenAttackNumbers = GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<PlayerAttackInfosAndChosenAttackNumbers>();
        ChosenHolder = characterCustomizer_AttackListHolder.ChosenholderOfButton;
        ReadyUpButton = GameObject.FindGameObjectWithTag("ReadyUpButton").GetComponent<UnityEngine.UI.Button>();
    }
    public void FlipFlopChosenOrAvailable()
    {
        switchVar = !switchVar;
        if (switchVar && ChosenHolder.childCount < 3)
        {
            transform.SetParent(ChosenHolder);
        }
        else
        {
            switch (AttacksInfo.Position)
            {
                case PositionItCanBeUsedIn.First:
                    transform.SetParent(characterCustomizer_AttackListHolder.AvailableholderOfButtons[0]);
                    break;
                case PositionItCanBeUsedIn.Second:
                    transform.SetParent(characterCustomizer_AttackListHolder.AvailableholderOfButtons[1]);
                    break;
                case PositionItCanBeUsedIn.Third:
                    transform.SetParent(characterCustomizer_AttackListHolder.AvailableholderOfButtons[2]);
                    break;
                case PositionItCanBeUsedIn.Fourth:
                    transform.SetParent(characterCustomizer_AttackListHolder.AvailableholderOfButtons[3]);
                    break;
                case PositionItCanBeUsedIn.All:
                    transform.SetParent(characterCustomizer_AttackListHolder.AvailableholderOfButtons[4]);
                    break;
            }

        }
        Destroy(this.gameObject.transform.GetChild(1).gameObject);
        checkChosenAttacks();

    }
    private void ShowToolTip(AttackInfo tip, Vector2 mousePos)
    {
        InstantiatedObj = Instantiate(PrefabbedExtraInfo, this.gameObject.transform);
        InstantiatedObj.SetActive(true);
        InstantiatedObj.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = tip.Name;
        InstantiatedObj.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = tip.Description;
        InstantiatedObj.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = "Position attack can be used: " + tip.Position.ToString();
        switch (tip.weaponType)
        {
            case WeaponType.Melee_SingleTarget:
                InstantiatedObj.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "Melee strike that deals damage to a single enemy";
                break;
            case WeaponType.Projectile_SingleTarget:
                InstantiatedObj.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "Throw a projectile that deals damage to a single enemy";
                break;
            case WeaponType.Magic_SingleTarget:
                InstantiatedObj.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "Use magic to deal damage to a single enemy";
                break;
            case WeaponType.Melee_MultiTarget:
                InstantiatedObj.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "A series of multiple melee strikes to deal damage to a multiple enemies";
                break;
            case WeaponType.Projectile_MultiTarget:
                InstantiatedObj.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "Fire a series of multiple projectiles to deal damage to a multiple enemies";
                break;
            case WeaponType.Magic_MultiTarget:
                InstantiatedObj.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "Use magic to deal damage to a multiple enemies";
                break;
            case WeaponType.Heal_SingleTarget:
                InstantiatedObj.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "Use Faith to heal target";
                break;
            case WeaponType.Heal_MultiTarget:
                InstantiatedObj.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "Use Faith to heal multiple targets";
                break;
            default:
                break;
        }
        InstantiatedObj.transform.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>().text = "How many targets can the action be used on: " + tip.numberOfTargetsIfApplicable;
        InstantiatedObj.transform.GetChild(5).GetComponent<TMPro.TextMeshProUGUI>().text = "Damage: " + tip.Damage;
        InstantiatedObj.transform.GetChild(6).GetComponent<TMPro.TextMeshProUGUI>().text = "Accuracy: " + tip.Accuracy;
        InstantiatedObj.transform.GetChild(7).GetComponent<TMPro.TextMeshProUGUI>().text = "Crit Chance: " + tip.CritChance;
        foreach (StatInflicted item in tip.AffectedStats)
        {
            switch (item)
            {
                case StatInflicted.None:
                    InstantiatedObj.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "Stat Inflicted: " + item;
                    break;
                case StatInflicted.Stun:
                    InstantiatedObj.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "Stat Inflicted: " + item;
                    break;
                case StatInflicted.Poison:
                    InstantiatedObj.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "Stat Inflicted: " + item;
                    break;
                case StatInflicted.bleed:
                    InstantiatedObj.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "Stat Inflicted: " + item;
                    break;
                case StatInflicted.Move_Self:
                    InstantiatedObj.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "Causes the Player to move position within the group";
                    break;
                case StatInflicted.Move_Target:
                    InstantiatedObj.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "Causes the Target to move position within their group";
                    break;
                case StatInflicted.Remove_Stats:
                    string addedStats = "";
                    for (int i = 0; i < tip.StatRemovedIfApplicable.Length; i++)
                    {
                        addedStats += tip.StatRemovedIfApplicable[i].ToString() + ", ";
                    }
                    InstantiatedObj.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "Stat(s) Removed: " + addedStats;
                    break;
                case StatInflicted.DMG_up:
                    InstantiatedObj.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "Stat Inflicted: " + item;
                    break;
                case StatInflicted.Remove_all:
                    InstantiatedObj.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "All stats removed";
                    break;
                default:
                    break;
            }

        }
    }
    public void checkChosenAttacks()
    {
        if (ChosenHolder.childCount != 3)
        {
            ReadyUpButton.interactable = false;
        }
        else
        {
            characterCustomizer_PlayerAttackInfosAndChosenAttackNumbers.attackInfos.Add(ChosenHolder.transform.GetChild(0).GetComponent<MicroAttackManager>().AttacksInfo);
            characterCustomizer_PlayerAttackInfosAndChosenAttackNumbers.CharChosen_ChosenAttacks_1 = ChosenHolder.transform.GetChild(0).GetComponent<MicroAttackManager>().AttackIDNumber;
            characterCustomizer_PlayerAttackInfosAndChosenAttackNumbers.attackInfos.Add(ChosenHolder.transform.GetChild(1).GetComponent<MicroAttackManager>().AttacksInfo);
            characterCustomizer_PlayerAttackInfosAndChosenAttackNumbers.CharChosen_ChosenAttacks_2 = ChosenHolder.transform.GetChild(1).GetComponent<MicroAttackManager>().AttackIDNumber;
            characterCustomizer_PlayerAttackInfosAndChosenAttackNumbers.attackInfos.Add(ChosenHolder.transform.GetChild(2).GetComponent<MicroAttackManager>().AttacksInfo);
            characterCustomizer_PlayerAttackInfosAndChosenAttackNumbers.CharChosen_ChosenAttacks_3 = ChosenHolder.transform.GetChild(2).GetComponent<MicroAttackManager>().AttackIDNumber;
            characterCustomizer_PlayerAttackInfosAndChosenAttackNumbers.character = characterCustomizer_AttackListHolder.character;
            characterCustomizer_PlayerAttackInfosAndChosenAttackNumbers.PlayerHealth = characterCustomizer_AttackListHolder.playerHealth;
            ReadyUpButton.interactable = true;
        }
    }

    private void HideToolTip()
    {
        if (InstantiatedObj != null)
        {
            Destroy(InstantiatedObj.gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowToolTip(AttacksInfo, Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideToolTip();
    }

}
