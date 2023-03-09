using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public GameObject[] EnemyNormalGameObjects;
    public GameObject[] EnemyDamagedGameObjects;

    public float maxHealth = 100;
    public float currentHealth;

    public Slider healthBar;
    public TMPro.TextMeshProUGUI healthText;


    void Start()
    {
        foreach (GameObject item in EnemyDamagedGameObjects)
        {
            item.SetActive(false);
        }
        currentHealth = maxHealth;
        healthBar.value = currentHealth / maxHealth;
        healthText.text = currentHealth.ToString()+"/"+ maxHealth.ToString();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.value = (float)currentHealth / (float)maxHealth;
        healthText.text = currentHealth.ToString();
    }
}
