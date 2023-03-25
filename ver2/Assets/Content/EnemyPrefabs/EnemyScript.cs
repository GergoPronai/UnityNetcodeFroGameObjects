using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class EnemyScript : NetworkBehaviour
{
    public NetworkVariable<float> health = new NetworkVariable<float>(100f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public float maxHealth = 100;
    public float enemyDamage = 6;
    private Canvas canvas;
    public Slider healthBar;
    public int enemyIndex;
    public TMPro.TextMeshProUGUI healthText;
    public TMPro.TextMeshProUGUI Missed;
    public GameObject[] EnemyNormalGameObjects;
    public GameObject[] EnemyDamagedGameObjects;
    private EnemyHealthManager enemyHealthManager;

    private void Start()
    {
        // Set canvas camera
        canvas = healthBar.transform.parent.parent.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;


        // Initialize health
        health.Value = maxHealth;
        healthBar.value = health.Value / maxHealth;
        healthText.text = health.Value.ToString() + " / " + maxHealth.ToString();
        Missed.gameObject.SetActive(false);


        //initialize enemyhealthManager

        enemyHealthManager = GameObject.FindGameObjectWithTag("EnemyHealthManager").transform.GetChild(0).GetComponent<EnemyHealthManager>();

    }

    public void Miss()
    {
        Missed.gameObject.SetActive(true);
        StartCoroutine(WaitForSeconds());
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(2);
        Missed.gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        health.Value -= damage;

        if (health.Value <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void Die()
    {
    }
}
