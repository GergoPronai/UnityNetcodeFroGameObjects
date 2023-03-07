using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public enum blessingType
{
    position1_Blesssing,
    position2_Blesssing,
    position3_Blesssing,
    position4_Blesssing
}
public class setPositionScript : NetworkBehaviour
{
    public blessingType blessing;
    public static setPositionScript Instance;
    [Header("multipliers")]
    float MeleeIncrease = 1.25f;
    float MeleeIncrease2 = 1.05f;
    float CritIncrease = 1.05f;
    float RangeIncrease = 1.25f;
    float RangeIncrease2 = 1.05f;
    float MagicIncrease = 1.25f;
    float MagicIncrease2 = 1.05f;
    float HealthIncrease = 1.25f;
    [Header("player & player UI")]
    public Sprite[] characters;
    public PlayergameObjScript blessedPlayer;
    public GameObject bleesedUI;
    public TMPro.TextMeshProUGUI blessingName;
    public Image blessingImage;
    public TMPro.TextMeshProUGUI description;
    public int SetPosition = 0;
    public int hasPlayersSetPosition = 0;
    public int PlayersInScene = 0;
    [Header("StartingRoomUI")]
    public TMPro.TextMeshProUGUI playersReadyinRoom;
    private void Start()
    {
        Instance = this;
        playersReadyinRoom.text=hasPlayersSetPosition+"/"+PlayersInScene+" are Ready";
    }
    public void enable(PlayergameObjScript player,blessingType blessing_)
    {
        blessing = blessing_;
        blessedPlayer = player;
        bleesedUI.SetActive(true);
        switch (blessing)
        {
            case blessingType.position1_Blesssing:
                blessingImage.sprite = characters[0];
                blessingName.text = "Accepting My Blessing Will Put You In First Position";
                description.text = "My Blessing Greatly Increases Melee Damage And Boosts Crit Chance";
                break;
            case blessingType.position2_Blesssing:
                blessingImage.sprite = characters[1];
                blessingName.text = "Accepting My Blessing Will Put You In second Position";
                description.text = "My Blessing Greatly Increases Your Range Damage And Boosts Melee damage";
                break;
            case blessingType.position3_Blesssing:
                blessingImage.sprite = characters[2];
                blessingName.text = "Accepting My Blessing Will Put You In third Position";
                description.text = "My Blessing Greatly Increases Your Magic Damage And Boosts Range damage";
                break;
            case blessingType.position4_Blesssing:
                blessingImage.sprite = characters[3];
                blessingName.text = "Accepting My Blessing Will Put You In fourth Position";
                description.text = "My Blessing Greatly Increases Your Healing Ability And Boosts Magic damage";
                break;
        }
    }
    public void Decline()
    {
        if (blessedPlayer != null)
        {
            blessedPlayer = null;
            blessing = blessingType.position1_Blesssing;
            bleesedUI.SetActive(false);
            hasPlayersSetPosition--;
            checkPlayersSetPositionAgainstLobbyAmount();
        }
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
                        item.CritChance *= CritIncrease;
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
        blessedPlayer.playerPositionInBattle = SetPosition;
        hasPlayersSetPosition++;
        checkPlayersSetPositionAgainstLobbyAmount();
    }
    void checkPlayersSetPositionAgainstLobbyAmount()
    {
        playersReadyinRoom.text = hasPlayersSetPosition + "/" + PlayersInScene + " are Ready";
        if (PlayersInScene== hasPlayersSetPosition)
        {
            //make the players able to embark on their journey
        }
        else
        {
            //remove ui options
        }
    }
}
