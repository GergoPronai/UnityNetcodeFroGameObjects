using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine.UI;


public class AltarScript : NetworkBehaviour
{
    public int attack_Position;
    public StartingRoom StartingRoomScript;
    public Transform obj;
    public bool stopSpinning=false;
    public string playername=null;

    // Update is called once per frame
    void Update()
    {
        if (stopSpinning==false)
        {
            activeCryatal();
        }
    }
    void activeCryatal()
    {
        obj.Rotate(0f, 15f * Time.deltaTime, 0f);
        float size = Mathf.Sin(Time.time * 1f) * 1f;
        size /= 30f;
        obj.localScale = new Vector3(obj.localScale.x+size, obj.localScale.y+size, obj.localScale.z+size);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            if (other.GetComponent<PlayergameObjScript>().HasPosition==false)
            {
                other.GetComponent<PlayergameObjScript>().playerPositionInBattle = attack_Position;
                other.GetComponent<PlayergameObjScript>().HasPosition = true;
                this.playername = other.GetComponent<PlayergameObjScript>().PlayerName;
                this.stopSpinning = true;
                setPositionScript.Instance.enable(other.GetComponent<PlayergameObjScript>());
                other.gameObject.GetComponent<PlayerMovement>().allowedMove = false;

            }
            else if (this.playername == other.GetComponent<PlayergameObjScript>().PlayerName)
            {
                other.GetComponent<PlayergameObjScript>().playerPositionInBattle = 0;
                other.GetComponent<PlayergameObjScript>().HasPosition = false;
                this.stopSpinning = false;
                this.playername = null;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {            
            other.gameObject.GetComponent<PlayerMovement>().allowedMove = true;
            setPositionScript.Instance.Decline();
        }
    }
}
