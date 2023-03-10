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

    public float enemyDamage=12;
    private Canvas canvas;
    public Slider healthBar;
    public TMPro.TextMeshProUGUI healthText;
    public TMPro.TextMeshProUGUI Missed;
    public static EnemyScript instance;

    void Start()
    {
        instance=this;
    
        foreach (GameObject item in EnemyDamagedGameObjects)
        {
            item.SetActive(false);
        }
        currentHealth = maxHealth;
        healthBar.value = currentHealth / maxHealth;
        healthText.text = currentHealth.ToString()+" / "+ maxHealth.ToString();
        canvas = healthBar.transform.parent.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        Missed.gameObject.SetActive(false);

    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.value = currentHealth / maxHealth;
        healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
    public void miss()
    {
        Missed.gameObject.SetActive(true);
        StartCoroutine(WaitForSeconds());
    }
    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(2);
        Missed.gameObject.SetActive(false);
    }
}
