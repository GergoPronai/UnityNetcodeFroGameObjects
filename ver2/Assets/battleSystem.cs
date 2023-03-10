using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost
}

public class battleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject[] Enemies;
    public GameObject[] Players;
    public Sprite imageSelection;
    public GameObject selector;
    public GameObject selectorObj;
    private GameObject Selected;
    private AttackInfo _attackInfo;
    private bool canSelect= false;

    void Start()
    {
        state = BattleState.Start;
        selectorObj = Instantiate(selector, transform);
        selectorObj.GetComponent<Canvas>().worldCamera = Camera.main;
        // Set the sprite of the selector to imageSelection

        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0) && canSelect)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Enemy")
                {
                    Debug.Log("Hit object: " + hit.collider.gameObject.name);
                    selectorObj.transform.position = hit.transform.position;
                    selectorObj.transform.GetChild(0).GetComponent<Image>().color = Color.red;
                }
                if (hit.collider.tag == "Player")
                {
                    Debug.Log("Hit object: " + hit.collider.gameObject.name);
                    selectorObj.transform.position = hit.transform.position;
                    selectorObj.transform.GetChild(0).GetComponent<Image>().color = Color.green;
                }
                Selected = hit.collider.gameObject;
            }

        }
    }
    public void Attack(AttackInfo attackInfo)
    {
        canSelect = true;
        _attackInfo = attackInfo;
        StartCoroutine(WaitForSeconds());
    }
    public void StopAttack()
    {
        canSelect = false;
        _attackInfo = null;
    }
    private void isAttacking()
    {
        float actualDamage = 0f;

        // Check if the attack is a critical hit
        if (Random.Range(0f, 1f) < _attackInfo.CritChance)
        {
            actualDamage = _attackInfo.Damage * 1.5f;
        }
        else
        {
            actualDamage = _attackInfo.Damage;
        }

        // Check if the attack hits based on accuracy
        if (Random.Range(0f, 1f) < _attackInfo.Accuracy)
        {
            Selected.GetComponent<EnemyScript>().TakeDamage(actualDamage);
        }
        else
        {
            Selected.GetComponent<EnemyScript>().miss();
        }
    }
    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(2);
        isAttacking();
    }
}
