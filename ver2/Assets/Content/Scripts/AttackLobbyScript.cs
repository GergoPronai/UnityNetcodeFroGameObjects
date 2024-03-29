using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackLobbyScript : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    // Start is called before the first frame update
    private GameObject InstantiatedObj=null;
    public GameObject PrefabbedExtraInfo;
    public AttackInfo attacksInfo;
    public TMPro.TextMeshProUGUI AttackNameText;

    void SetUpExtraInfo(AttackInfo AttacksInfo)
    {
        if (InstantiatedObj!=null)
        {
            Destroy(InstantiatedObj);
        }
        InstantiatedObj = Instantiate(PrefabbedExtraInfo, transform.parent.transform.parent);
        InstantiatedObj.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = AttacksInfo.Name;
        InstantiatedObj.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = AttacksInfo.Description;
        InstantiatedObj.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = "Position attack can be used: " + AttacksInfo.Position.ToString();
        switch (AttacksInfo.weaponType)
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
        InstantiatedObj.transform.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>().text = "How many targets can the action be used on: " + AttacksInfo.numberOfTargetsIfApplicable;
        InstantiatedObj.transform.GetChild(5).GetComponent<TMPro.TextMeshProUGUI>().text = "Damage: " + AttacksInfo.Damage;
        InstantiatedObj.transform.GetChild(6).GetComponent<TMPro.TextMeshProUGUI>().text = "Accuracy: " + AttacksInfo.Accuracy;
        InstantiatedObj.transform.GetChild(7).GetComponent<TMPro.TextMeshProUGUI>().text = "Crit Chance: " + AttacksInfo.CritChance;
        foreach (StatInflicted item in AttacksInfo.AffectedStats)
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
                    for (int i = 0; i < AttacksInfo.StatRemovedIfApplicable.Length; i++)
                    {
                        addedStats += AttacksInfo.StatRemovedIfApplicable[i].ToString()+ ", ";
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetUpExtraInfo(attacksInfo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (InstantiatedObj != null)
        {
            Destroy(InstantiatedObj);
        }
    }
}
