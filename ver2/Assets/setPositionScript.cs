using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum blessingType
{
    position1_Blesssing,
    position2_Blesssing,
    position3_Blesssing,
    position4_Blesssing
}
public class setPositionScript : MonoBehaviour
{
    public blessingType blessing;
    public static setPositionScript Instance;
    [Header("multipliers")]
    float MeleeIncrease = 1.25f;
    float MeleeIncrease2 = 1.05f;
    float RangeIncrease = 1.25f;
    float RangeIncrease2 = 1.05f;
    float MagicIncrease = 1.25f;
    float MagicIncrease2 = 1.05f;
    float HealthIncrease = 1.25f;
    [Header("player & UI")]
    public PlayergameObjScript blessedPlayer;
    public GameObject bleesedUI;
    private void Start()
    {
        Instance = this;
    }
    public void enable(PlayergameObjScript player)
    {
        blessedPlayer = player;
        bleesedUI.SetActive(true);
    }
    public void Decline()
    {
        blessedPlayer = null;
        bleesedUI.SetActive(true);
    }
    public void Accept()
    {
        switch (blessing)
        {
            case blessingType.position1_Blesssing:
                //Melee boost
                foreach (AttackInfo item in blessedPlayer.attackInfos)
                {
                    if (item.weaponType ==WeaponType.Melee_MultiTarget || item.weaponType == WeaponType.Melee_SingleTarget)
                    {
                        item.Damage *= MeleeIncrease;
                    }
                }
                break;
            case blessingType.position2_Blesssing:
                //Range and melee
                foreach (AttackInfo item in blessedPlayer.attackInfos)
                {
                    if (item.weaponType == WeaponType.Melee_MultiTarget || item.weaponType == WeaponType.Melee_SingleTarget)
                    {
                        item.Damage *= MeleeIncrease2;
                    }
                    if (item.weaponType == WeaponType.Projectile_MultiTarget || item.weaponType == WeaponType.Projectile_SingleTarget)
                    {
                        item.Damage *= RangeIncrease;
                    }
                }
                break;
            case blessingType.position3_Blesssing:
                //Magic And range
                foreach (AttackInfo item in blessedPlayer.attackInfos)
                {
                    if (item.weaponType == WeaponType.Magic_MultiTarget || item.weaponType == WeaponType.Magic_SingleTarget)
                    {
                        item.Damage *= MagicIncrease;
                    }
                    if (item.weaponType == WeaponType.Projectile_MultiTarget || item.weaponType == WeaponType.Projectile_SingleTarget)
                    {
                        item.Damage *= RangeIncrease2;
                    }
                }
                break;
            case blessingType.position4_Blesssing:
                //Magic and Healing
                foreach (AttackInfo item in blessedPlayer.attackInfos)
                {
                    if (item.weaponType == WeaponType.Magic_MultiTarget || item.weaponType == WeaponType.Magic_SingleTarget)
                    {
                        item.Damage *= MagicIncrease2;
                    }
                    if (item.weaponType == WeaponType.Heal_MultiTarget || item.weaponType == WeaponType.Heal_SingleTarget)
                    {
                        item.Damage *= HealthIncrease;
                    }
                }
                break;
        }
        blessedPlayer.gameObject.GetComponent<PlayerMovement>().allowedMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
