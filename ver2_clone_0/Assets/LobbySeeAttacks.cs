using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LobbySeeAttacks : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMPro.TextMeshProUGUI AttackName;
    public AttackInfo attackinfo;
    private AttackListHolder infos;
    public GameObject tipWindowPrefab;
    private GameObject tipWindow;

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
    private void Start()
    {
        infos=GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<AttackListHolder>();
        HideToolTip();
    }
    public void SetInfo(string pos, CharacterChoices character)
    {
        if (attackinfo != null)
        {
            switch (character)
            {
                case CharacterChoices.Barbarian:
                    attackinfo = infos.BarabarianAttackInfos[int.Parse(pos)];
                    break;
                case CharacterChoices.Knight:
                    attackinfo = infos.KnightAttackInfos[int.Parse(pos)];
                    break;
                case CharacterChoices.Mage:
                    attackinfo = infos.MageAttackInfos[int.Parse(pos)];
                    break;
                case CharacterChoices.Rogue:
                    attackinfo = infos.RogueAttackInfos[int.Parse(pos)];
                    break;
            }
            AttackName.text = attackinfo.Name;
        }
    }
    private void ShowToolTip(AttackInfo tip, Vector2 mousePos)
    {
        tipWindow = Instantiate(tipWindowPrefab, this.gameObject.transform);
        tipWindow.SetActive(true);
        tipWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = tip.Name;
        tipWindow.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = tip.Description;
        tipWindow.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = "Position attack can be used: " + tip.Position.ToString();
        switch (tip.weaponType)
        {
            case WeaponType.Melee_SingleTarget:
                tipWindow.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "Melee strike that deals damage to a single enemy";
                break;
            case WeaponType.Projectile_SingleTarget:
                tipWindow.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "Throw a projectile that deals damage to a single enemy";
                break;
            case WeaponType.Magic_SingleTarget:
                tipWindow.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "Use magic to deal damage to a single enemy";
                break;
            case WeaponType.Melee_MultiTarget:
                tipWindow.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "A series of multiple melee strikes to deal damage to a multiple enemies";
                break;
            case WeaponType.Projectile_MultiTarget:
                tipWindow.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "Fire a series of multiple projectiles to deal damage to a multiple enemies";
                break;
            case WeaponType.Magic_MultiTarget:
                tipWindow.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "Use magic to deal damage to a multiple enemies";
                break;
            case WeaponType.Heal_SingleTarget:
                tipWindow.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "Use Faith to heal target";
                break;
            case WeaponType.Heal_MultiTarget:
                tipWindow.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = "Use Faith to heal multiple targets";
                break;
            default:
                break;
        }
        tipWindow.transform.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>().text = "How many targets can the action be used on: " + tip.numberOfTargetsIfApplicable;
        tipWindow.transform.GetChild(5).GetComponent<TMPro.TextMeshProUGUI>().text = "Damage: " + tip.Damage;
        tipWindow.transform.GetChild(6).GetComponent<TMPro.TextMeshProUGUI>().text = "Accuracy: " + tip.Accuracy;
        tipWindow.transform.GetChild(7).GetComponent<TMPro.TextMeshProUGUI>().text = "Crit Chance: " + tip.CritChance;
        foreach (StatInflicted item in tip.AffectedStats)
        {
            switch (item)
            {
                case StatInflicted.None:
                    tipWindow.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "Stat Inflicted: " + item;
                    break;
                case StatInflicted.Stun:
                    tipWindow.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "Stat Inflicted: " + item;
                    break;
                case StatInflicted.Poison:
                    tipWindow.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "Stat Inflicted: " + item;
                    break;
                case StatInflicted.bleed:
                    tipWindow.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "Stat Inflicted: " + item;
                    break;
                case StatInflicted.Move_Self:
                    tipWindow.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "Causes the Player to move position within the group";
                    break;
                case StatInflicted.Move_Target:
                    tipWindow.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "Causes the Target to move position within their group";
                    break;
                case StatInflicted.Remove_Stats:
                    string addedStats = "";
                    for (int i = 0; i < tip.StatRemovedIfApplicable.Length; i++)
                    {
                        addedStats += tip.StatRemovedIfApplicable[i].ToString() + ", ";
                    }
                    tipWindow.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "Stat(s) Removed: " + addedStats;
                    break;
                case StatInflicted.DMG_up:
                    tipWindow.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "Stat Inflicted: " + item;
                    break;
                case StatInflicted.Remove_all:
                    tipWindow.transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = "All stats removed";
                    break;
                default:
                    break;
            }
        }
    }
    private void HideToolTip()
    {
        if (tipWindow!=null)
        {
            Destroy(tipWindow.gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowToolTip(attackinfo, Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideToolTip();
    }
}
