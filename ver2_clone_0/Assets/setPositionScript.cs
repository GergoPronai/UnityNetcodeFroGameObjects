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
    public int hasPlayersSetPosition = 0;
    public int PlayersInScene = 0;
    [Header("StartingRoomUI")]
    public TMPro.TextMeshProUGUI playersReadyinRoom;
    public GameObject isEmbarking;
    private AltarScript altarScript_;
    private int AttackPosition;
    private GameObject Player;
    private bool AlreadyChosen=false;

    private void Start()
    {
        Instance = this;
        playersReadyinRoom.text=hasPlayersSetPosition+"/"+PlayersInScene+" are Ready";
    }
    public void enable(GameObject player,blessingType blessing_,int position, AltarScript altarScript)
    {        
        altarScript_ = altarScript;
        AttackPosition = position;
        Player = player;
        PlayergameObjScript _player = Player.GetComponent<PlayergameObjScript>();
        blessing = blessing_;
        blessedPlayer = _player;
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
            Player.GetComponent<PlayergameObjScript>().playerPositionInBattle = 0;
            Player.GetComponent<PlayergameObjScript>().HasPosition = false;
            blessedPlayer = null;
            blessing = blessingType.position1_Blesssing;
            bleesedUI.SetActive(false);
            if (hasPlayersSetPosition>0) {
                hasPlayersSetPosition--;
            }
            checkPlayersSetPositionAgainstLobbyAmount();
            Player.GetComponent<PlayerMovement>().allowedMove = true;
            if (altarScript_ != null)
            {
                AlreadyChosen = false;
            }
        }
    }
    public void Accept()
    {
        if (AlreadyChosen)
        {
            hasPlayersSetPosition--;
            AcceptAltar();
        }
        else
        {
            AcceptAltar();
            if (altarScript_ != null)
            {
                AlreadyChosen = true;
            }
        }
    }
    public void AcceptAltar()
    {
        Player.GetComponent<PlayergameObjScript>().playerPositionInBattle = AttackPosition;
        Player.GetComponent<PlayergameObjScript>().HasPosition = true;
        if (hasPlayersSetPosition < PlayersInScene)
        {
            hasPlayersSetPosition++;
        }
        altarScript_.stopSpinning = true;
        altarScript_.playername = Player.GetComponent<PlayergameObjScript>().PlayerName;
        Player.GetComponent<PlayerMovement>().allowedMove = true;
        bleesedUI.SetActive(false);
        checkPlayersSetPositionAgainstLobbyAmount();
    }
    public void SetBonusess()
    {
        switch (blessing)
        {
            case blessingType.position1_Blesssing:
                //Melee boost
                foreach (AttackInfo item in blessedPlayer.attackInfos)
                {
                    if (item.weaponType == WeaponType.Melee_MultiTarget || item.weaponType == WeaponType.Melee_SingleTarget)
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
        Player.GetComponent<PlayergameObjScript>().attackInfos = blessedPlayer.attackInfos;
    }
    void checkPlayersSetPositionAgainstLobbyAmount()
    {
        playersReadyinRoom.text = hasPlayersSetPosition + "/" + PlayersInScene + " are Ready";
        if (PlayersInScene == hasPlayersSetPosition)
        {
            isEmbarking.SetActive(true);
        }
        else
        {
            isEmbarking.SetActive(false);
        }
    }
    public void OpenLockedDoorsAndBorders()
    {
        StartingRoom SatringRoomScript = GameObject.FindGameObjectWithTag("StartingRoom").GetComponent<StartingRoom>();
        foreach (GameObject item in SatringRoomScript.locked)
        {
            item.SetActive(false);
            item.transform.GetChild(2).gameObject.SetActive(true);
        }
        StartCoroutine(StartGameWaitCycle(2));
        
        SatringRoomScript.Borders.SetActive(false);
    }
    IEnumerator StartGameWaitCycle(int sec)
    {
        yield return new WaitForSeconds(sec);
    }
}
