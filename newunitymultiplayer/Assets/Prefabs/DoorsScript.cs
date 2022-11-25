using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorsScript : MonoBehaviour
{
    public Animator animator;
    public GameObject FogParticleSystem;
    // Update is called once per frame
    //public FightBox fightBoxScript=null;
    //public DecideRoom DecideRoomScript =null;
    //private BattleSystem battleSystemScript = null;
    void OnTriggerEnter(Collider col)
    {
        animator.SetBool("Open", true);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        var main = FogParticleSystem.GetComponent<ParticleSystem>().main;
        main.loop = false;
    }
}
