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
    public float currentHealth;

    public float enemyDamage = 6;
    private Canvas canvas;
    public Slider healthBar;
    public TMPro.TextMeshProUGUI healthText;
    public TMPro.TextMeshProUGUI Missed;

    public event System.Action Dead;

    private bool HasTakenDamage
    {
        get { return currentHealth < maxHealth; }
    }

    private int enemyIndex;
    public int EnemyIndex
    {
        get { return enemyIndex; }
        set
        {
            enemyIndex = value;
            // Update the enemy's index in the EnemyHealthManager
            if (healthManager != null)
            {
                switch (enemyIndex)
                {
                    case 0:
                        healthManager.enemy1Health.Value = currentHealth;
                        break;
                    case 1:
                        healthManager.enemy2Health.Value = currentHealth;
                        break;
                    case 2:
                        healthManager.enemy3Health.Value = currentHealth;
                        break;
                    case 3:
                        healthManager.enemy4Health.Value = currentHealth;
                        break;
                    default:
                        Debug.LogError("Invalid enemy index!");
                        break;
                }
            }
        }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            // Update the enemy's health in the EnemyHealthManager
            if (healthManager != null)
            {
                switch (enemyIndex)
                {
                    case 0:
                        healthManager.enemy1Health.Value = currentHealth;
                        break;
                    case 1:
                        healthManager.enemy2Health.Value = currentHealth;
                        break;
                    case 2:
                        healthManager.enemy3Health.Value = currentHealth;
                        break;
                    case 3:
                        healthManager.enemy4Health.Value = currentHealth;
                        break;
                    default:
                        Debug.LogError("Invalid enemy index!");
                        break;
                }
            }
            // Update the health bar and text for the enemy
            if (healthBar != null && healthText != null)
            {
                healthBar.value = currentHealth / maxHealth;
                healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
            }
        }
    }

    public EnemyHealthManager healthManager;

    private void Start()
    {
        currentHealth = maxHealth;
        healthManager = GameObject.FindGameObjectWithTag("EnemyHealthManager").transform.GetChild(0).GetComponent<EnemyHealthManager>();

        currentHealth = maxHealth;

        // Set the health bar and text for the enemy
        healthBar.value = currentHealth / maxHealth;
        healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

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
        get
        {
            return currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        // Only allow the server to take damage
        if (!IsServer)
            return;
        // Reduce the current health of the enemy
        currentHealth -= damage;

        // Update the enemy's health in the EnemyHealthManager
        if (healthManager != null)
        {
            switch (enemyIndex)
            {
                case 0:
                    healthManager.enemy1Health.Value = currentHealth;
                    break;
                case 1:
                    healthManager.enemy2Health.Value = currentHealth;
                    break;
                case 2:
                    healthManager.enemy3Health.Value = currentHealth;
                    break;
                case 3:
                    healthManager.enemy4Health.Value = currentHealth;
                    break;
                default:
                    Debug.LogError("Invalid enemy index!");
                    break;
            }
        }

        // If the enemy's health is less than or equal to 0, they are dead
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // Disable the normal game object and enable the damaged game object
            foreach (GameObject item in EnemyNormalGameObjects)
            {
                item.SetActive(false);
            }
            foreach (GameObject item in EnemyDamagedGameObjects)
            {
                item.SetActive(true);
            }
            // Invoke the Dead event
            Dead?.Invoke();
            // Destroy the enemy after a short delay
            StartCoroutine(DestroyAfterDelay());
        }
        else
        {
            // Play the hit animation
            foreach (GameObject item in EnemyNormalGameObjects)
            {
                item.GetComponent<Animator>().SetTrigger("Hit");
            }
            foreach (GameObject item in EnemyDamagedGameObjects)
            {
                item.GetComponent<Animator>().SetTrigger("Hit");
            }
        }
        // Update the health bar and text for the enemy
        if (healthBar != null && healthText != null)
        {
            healthBar.value = currentHealth / maxHealth;
            healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}