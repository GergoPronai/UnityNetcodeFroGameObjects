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
    public GameObject[] enemies;
    public GameObject[] players;
    public Sprite imageSelection;
    public GameObject selector;
    public GameObject selectorObj;
    private GameObject selected;
    private AttackInfo attackInfo;
    public bool canSelect = false;
    private string selectedTag = "";
    private bool playerGoesFirst;
    public GameObject battleBox;
    private bool isFinished = false;
    void Start()
    {
        state = BattleState.Start;
        selectorObj = Instantiate(selector, transform);
        selectorObj.GetComponent<Canvas>().worldCamera = Camera.main;
        // Set the sprite of the selector to imageSelection
        selectorObj.transform.GetChild(0).GetComponent<Image>().sprite = imageSelection;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        players = GameObject.FindGameObjectsWithTag("Player");

        // randomly choose who goes first
        playerGoesFirst = Random.value < 0.5f;

        if (playerGoesFirst)
        {
            state = BattleState.PlayerTurn;
        }
        else
        {
            state = BattleState.EnemyTurn;
        }
    }

    private void Update()
    {
        if (!isFinished)
        {
            switch (state)
            {
                case BattleState.PlayerTurn:
                    if (Input.GetMouseButtonDown(0) && canSelect)
                    {
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.collider.tag == "Enemy")
                            {
                                selectorObj.transform.position = hit.transform.position;
                                selectorObj.transform.GetChild(0).GetComponent<Image>().color = Color.red;
                                selectedTag = "Enemy";
                                selected = hit.collider.gameObject;
                            }
                            if (hit.collider.tag == "Player")
                            {
                                selectorObj.transform.position = hit.transform.position;
                                selectorObj.transform.GetChild(0).GetComponent<Image>().color = Color.green;
                                selectedTag = "Player";
                                selected = hit.collider.gameObject;
                            }
                        }
                    }
                    break;

                case BattleState.EnemyTurn:
                    EnemyAttacks();
                    break;

                case BattleState.Won:
                    Debug.Log("You won the battle!");
                    GameObject chest = Instantiate(battleBox.GetComponent<BattleScript>().ChestPrefab, battleBox.transform);
                    chest.transform.eulerAngles = new Vector3(90, 0, 0);
                    chest.GetComponent<chestScript>().ObjectsToSpawn = battleBox.GetComponent<BattleScript>().possibleItems;
                    chest.GetComponent<chestScript>().enable();
                    isFinished = true;
                    break;

                case BattleState.Lost:
                    Debug.Log("You lost the battle!");
                    break;
            }

            if (CheckWin())
            {
                state = BattleState.Won;
            }
            else if (CheckLoss())
            {
                state = BattleState.Lost;
            }
        }
    }

    public void Select()
    {
        canSelect = true;
    }

    private IEnumerator WaitForAttacks()
    {
        yield return new WaitForSeconds(2f);

        state = BattleState.PlayerTurn;
    }

    private void EnemyAttacks()
    {
        GameObject enemyToAttack = enemies[Random.Range(0, enemies.Length)];
        GameObject playerToAttack = GameObject.FindWithTag("Player");
        // perform attack
        float actualDamage = enemyToAttack.GetComponent<EnemyScript>().enemyDamage;
        if (Random.Range(0f, 100f) < attackInfo.Accuracy)
        {
            playerToAttack.GetComponent<PlayergameObjScript>().TakeDamage(actualDamage);
        }
        else
        {
            playerToAttack.GetComponent<PlayergameObjScript>().miss();
        }

        // switch back to player's turn
        state = BattleState.PlayerTurn;
    }

    public void Attack(AttackInfo attackInfo)
    {
        this.attackInfo = attackInfo;
        if (selected != null)
        {
            if (selectedTag == "Enemy")
            {
                if (Random.Range(0f, 100f) < attackInfo.Accuracy)
                {
                    selected.GetComponent<EnemyScript>().TakeDamage(attackInfo.Damage);
                }
                else
                {
                    selected.GetComponent<EnemyScript>().Miss();
                }                
            }
            /*else if (selectedTag == "Player")
            {
                if (Random.Range(0f, 1f) < attackInfo.Accuracy)
                {
                    selected.GetComponent<PlayergameObjScript>().TakeDamage(attackInfo.Damage);
                }
            }*/
            selected = null;
            selectedTag = "";
            selectorObj.transform.position = new Vector3(100, 1000, 100);
            state = BattleState.EnemyTurn;
            if (CheckWin())
            {
                state = BattleState.Won;
            }
            else if (CheckLoss())
            {
                state = BattleState.Lost;
            }
        }
    }

    private bool CheckWin()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<EnemyScript>().health.Value <= 0)
            {
                enemy.SetActive(false);
            }
            if (enemy.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckLoss()
    {
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayergameObjScript>().currentHealth <= 0)
            {
                player.GetComponent<PlayergameObjScript>().YouDied.SetActive(true);
                StartCoroutine(youDied(player));
                return true; // exit the loop when a player dies
            }
        }
        return false;
    }
    private IEnumerator youDied(GameObject player)
    {
        yield return new WaitForSeconds(3f);
        player.GetComponent<PlayergameObjScript>().YouDied.SetActive(false);
        yield return new WaitForSeconds(2f);
        Destroy(player);
        if (players.Length == 1)
        {
            state = BattleState.Lost;
        }
    }

}
