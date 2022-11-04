using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorsScript : MonoBehaviour
{
    public Animator animator;
    public GameObject[] FogParticleSystem;
    // Update is called once per frame
    //public FightBox fightBoxScript=null;
    //public DecideRoom DecideRoomScript =null;
    //private BattleSystem battleSystemScript = null;
    private void Start()
    {
        //battleSystemScript = GameObject.FindObjectOfType<BattleSystem>();
    }
    void OnTriggerEnter(Collider col)
    {
        foreach (GameObject item in FogParticleSystem)
        {
            if (item != null)
            {
                var main = item.GetComponent<ParticleSystem>().main;
                main.loop = false;
            }
        }
        animator.SetBool("Open", true);
        //battleSystemScript.setUpBattle(DecideRoomScript, fightBoxScript);
        //battleSystemScript.CheckOnRoomType();
        //battleSystemScript.EnteredRoom = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    private void Update()
    {
        //battleSystemScript.runCameraSwitchForDoorTransition();
    }
}
