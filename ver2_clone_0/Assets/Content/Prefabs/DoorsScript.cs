using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomOrientation
{
    None,
    North,
    South,
    East,
    West
}
public class DoorsScript : MonoBehaviour
{
    public Animator animator;
    public GameObject FogParticleSystem;
    public RoomOrientation roomOrientation;
    private GameObject currentPlayerCam;
    private BattleScript battleScript;
    private void Start()
    {
        battleScript = transform.parent.parent.transform.GetChild(7).GetComponent<BattleScript>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (animator.GetBool("Open")!=true)
            {
                animator.SetBool("Open", true);            
                var main = FogParticleSystem.GetComponent<ParticleSystem>().main;
                main.loop = false;
                GameObject ParentsParent = transform.parent.transform.parent.gameObject;
                currentPlayerCam = col.gameObject.transform.GetChild(3).gameObject;
                if (ParentsParent.GetComponent<GameRoom>().roomType == RoomType.BattleRoom)
                {
                    switch (roomOrientation)
                    {
                        case RoomOrientation.North:
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);

                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(false);
                            battleScript.SpawnPointHolder_Enemies = ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(1).gameObject;
                            battleScript.SpawnPointHolder_Players = ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(0).gameObject;
                            break;
                        case RoomOrientation.South:
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);

                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(false);
                            battleScript.SpawnPointHolder_Enemies = ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(0).gameObject;
                            battleScript.SpawnPointHolder_Players = ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(1).gameObject;
                            break;
                        case RoomOrientation.East:
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);

                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(true);
                            battleScript.SpawnPointHolder_Enemies = ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(3).gameObject;
                            battleScript.SpawnPointHolder_Players = ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(2).gameObject;
                            break;
                        case RoomOrientation.West:
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);

                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);
                            ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(false);
                            battleScript.SpawnPointHolder_Enemies = ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(2).gameObject;
                            battleScript.SpawnPointHolder_Players = ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(3).gameObject;
                            break;
                    }                    
                    battleScript.enable(currentPlayerCam);
                    Debug.Log(battleScript.SpawnPointHolder_Enemies.name);
                    Debug.Log(battleScript.SpawnPointHolder_Players.name);
                }
            }
        }
    }
}
