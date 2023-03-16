using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System.Linq;
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
    public GameObject[] ui_elementsFromEmbarking;
    private int temp;

    public void enable(GameObject player,blessingType blessing_,int position, AltarScript altarScript)
    {
        if (NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayergameObjScript>().PlayerName == player.GetComponent<PlayergameObjScript>().PlayerName)
        {
            this.altarScript_ = altarScript;
            this.AttackPosition = position;
            this.Player = player;
            PlayergameObjScript _player = this.Player.GetComponent<PlayergameObjScript>();
            this.blessing = blessing_;
            this.blessedPlayer = _player;
            this.bleesedUI.SetActive(true);
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
    }
    public void Decline()
    {
        if (this.blessedPlayer != null)
        {
            this.Player.GetComponent<PlayergameObjScript>().playerPositionInBattle = 0;
            this.Player.GetComponent<PlayergameObjScript>().HasPosition = false;
            this.blessedPlayer = null;
            this.blessing = blessingType.position1_Blesssing;
            this.bleesedUI.SetActive(false);
            if (this.hasPlayersSetPosition >0) {
                this.hasPlayersSetPosition--;
            }
            this.Player.GetComponent<PlayerMovement>().allowedMove = true;
            if (altarScript_ != null)
            {
                this.AlreadyChosen = false;
            }
        }
    }
    private void Update()
    {
        checkPlayersSetPositionAgainstLobbyAmount();

        playersReadyinRoom.text = hasPlayersSetPosition + "/" + PlayersInScene + " are Ready";
    }
    public void Accept()
    {
        if (this.AlreadyChosen)
        {
            this.hasPlayersSetPosition--;
            this.AcceptAltar();
        }
        else
        {
            this.AcceptAltar();
            if (altarScript_ != null)
            {
                this.AlreadyChosen = true;
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
        bool allPlayersHavePosition = true;
        temp=0;
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            PlayergameObjScript playerScript = player.GetComponent<PlayergameObjScript>();

            if (!playerScript.HasPosition)
            {
                allPlayersHavePosition = false;
                break;
            }
            else if(playerScript.HasPosition)
            {
                temp ++;
            }
        }
        hasPlayersSetPosition = temp;

        if (NetworkManager.Singleton.IsHost && allPlayersHavePosition)
        {
            this.isEmbarking.SetActive(true);
        }
        else
        {
            isEmbarking.SetActive(false);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SetLockedDoorsAndBordersServerRpc(bool isActive)
    {
        StartingRoom startingRoomScript = GameObject.FindGameObjectWithTag("StartingRoom").GetComponent<StartingRoom>();

        startingRoomScript.Borders.SetActive(!isActive);

        foreach (GameObject door in startingRoomScript.locked)
        {
            door.SetActive(isActive);
        }

        SetLockedDoorsAndBordersClientRpc(isActive);
    }

    [ClientRpc]
    void SetLockedDoorsAndBordersClientRpc(bool isActive)
    {
        StartingRoom startingRoomScript = GameObject.FindGameObjectWithTag("StartingRoom").GetComponent<StartingRoom>();

        startingRoomScript.Borders.SetActive(!isActive);

        foreach (GameObject door in startingRoomScript.locked)
        {
            door.SetActive(isActive);
        }
        var altars = FindObjectsOfType<AltarScript>();
        foreach (var altar in altars)
        {
            altar.gameObject.SetActive(false);
        }
        foreach (GameObject item in ui_elementsFromEmbarking)
        {
            item.SetActive(false);
        }
    }

    public void OpenLockedDoorsAndBorders()
    {
        SetLockedDoorsAndBordersServerRpc(true);
    }

}
