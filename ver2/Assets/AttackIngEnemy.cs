using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIngEnemy : MonoBehaviour
{
    private List<AttackInfo> Attack_attackInfos;
    private battleSystem battleSystem;
    public GameObject attackHolder;
    public GameObject attackPrefab;
    private GameObject attackObj;
    private bool clickedOnce=false;
    private bool StartedAttacking=false;

    // Start is called before the first frame update
    void Start()
    {
        Attack_attackInfos = gameObject.GetComponent<PlayergameObjScript>().attackInfos;
        battleSystem = GameObject.FindGameObjectWithTag("BattleManager").transform.GetChild(0).GetComponent<battleSystem>();
    }

    public void playerAttackEnemy()
    {
        if (!clickedOnce)
        {
            foreach (AttackInfo item in Attack_attackInfos)
            {
                attackObj = Instantiate(attackPrefab, attackHolder.transform);
                attackObj.transform.localScale = new Vector3(1f, 1f, 1f);
                attackObj.GetComponent<AttackIngEnemyPrefab>().AttacksInfo = item;
                attackObj.GetComponent<AttackIngEnemyPrefab>().nameOfAttack.text = item.Name;
            }
            clickedOnce = true;
        }
        else
        {
            attackHolder.SetActive(true);
        }
    }
    public void toggleAttacking()
    {
        StartedAttacking = !StartedAttacking;
        if (StartedAttacking)
        {
            playerAttackEnemy();
        }
        else
        {
            battleSystem.StopAttack();
            battleSystem.selectorObj.SetActive(false);
            battleSystem.selectorObj.transform.position= new Vector3(0f, 0f, 0f);
            attackHolder.SetActive(false);

        }
    }
}
