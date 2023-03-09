using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIngEnemy : MonoBehaviour
{
    private List<AttackInfo> Attack_attackInfos;
    public GameObject attackHolder;
    public GameObject attackPrefab;
    private GameObject attackObj;
    private bool clickedOnce=false;

    // Start is called before the first frame update
    void Start()
    {
        Attack_attackInfos = gameObject.GetComponent<PlayergameObjScript>().attackInfos;

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
}
