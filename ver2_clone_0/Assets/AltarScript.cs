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
    public blessingType blessing;
    private setPositionScript sePositions;

    // Update is called once per frame
    private void Start()
    {
        sePositions = GameObject.FindGameObjectWithTag("PositionsManager").GetComponent<setPositionScript>();

    }
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
            sePositions.enable(other.gameObject, blessing, attack_Position, this);
            other.GetComponent<PlayerMovement>().allowedMove = false;
        }
    }
}
