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
    void OnTriggerEnter(Collider col)
    {
        animator.SetBool("Open", true);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        var main = FogParticleSystem.GetComponent<ParticleSystem>().main;
        main.loop = false;
        GameObject ParentsParent = transform.parent.transform.parent.gameObject;
        if (ParentsParent.GetComponent<GameRoom>().roomType==RoomType.BattleRoom)
        {            
            switch (roomOrientation)
            {
                case RoomOrientation.North:
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);

                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(false);
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
                    break;
                case RoomOrientation.East:
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);

                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(false);
                    break;
                case RoomOrientation.West:
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);

                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);
                    ParentsParent.GetComponent<GameRoom>().battleRoom.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(true);
                    break;
            }
        }
    }
}
