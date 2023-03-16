using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class EnemyScript : NetworkBehaviour
{
    public GameObject[] EnemyNormalGameObjects;
    public GameObject[] EnemyDamagedGameObjects;

    public float maxHealth = 100;
    public NetworkVariable<float> currentHealth = new NetworkVariable<float>(100f);

    public float enemyDamage = 6;
    private Canvas canvas;
    public Slider healthBar;
    public TMPro.TextMeshProUGUI healthText;
    public TMPro.TextMeshProUGUI Missed;

    public event System.Action Dead;

    private bool HasTakenDamage
    {
        get { return currentHealth.Value < maxHealth; }
    }


    public EnemyHealthManager healthManager;

    private void Start()
    {
        healthManager = EnemyHealthManager.Instance;

        // Set the health bar and text for the enemy
        healthBar.value = healthManager.EnemyHealth.Value / maxHealth;
        healthText.text = healthManager.EnemyHealth.Value.ToString() + " / " + maxHealth.ToString();

        // Disable damaged game objects initially
        foreach (GameObject item in EnemyDamagedGameObjects)
        {
            item.SetActive(false);
        }

        // Set canvas camera
        canvas = healthBar.transform.parent.parent.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;

        // Hide the missed text
        Missed.gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        // Subtract damage from the enemy's health
        healthManager.EnemyHealth.Value -= damage;

        // Update the health bar and text
        healthBar.value = healthManager.EnemyHealth.Value / maxHealth;
        healthText.text = healthManager.EnemyHealth.Value.ToString() + " / " + maxHealth.ToString();

        // Show damaged game object if health is below a certain threshold
        for (int i = 0; i < EnemyDamagedGameObjects.Length; i++)
        {
            if (currentHealth.Value <= maxHealth * (i + 1) / EnemyDamagedGameObjects.Length)
            {
                EnemyDamagedGameObjects[i].SetActive(true);
                EnemyNormalGameObjects[i].SetActive(false);
            }
        }

        // Check if the enemy is dead and trigger the Dead event if it is
        if (currentHealth.Value == 0 && Dead != null)
        {
            Dead();
        }
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

    public float Health
    {
        get { return currentHealth.Value; }
    }
}
