using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyHealthManager : NetworkBehaviour
{
    private GameObject[] enemies;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;

    public float _enemy1Health;
    public float _enemy2Health;
    public float _enemy3Health;
    public float _enemy4Health;

    public NetworkVariable<float> enemy1Health = new NetworkVariable<float>(0f);
    public NetworkVariable<float> enemy2Health = new NetworkVariable<float>(0f);
    public NetworkVariable<float> enemy3Health = new NetworkVariable<float>(0f);
    public NetworkVariable<float> enemy4Health = new NetworkVariable<float>(0f);

    /*private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        switch (enemies.Length)
        {
            case 1:
                enemy1 = enemies[0];
                enemy1Health.Value = enemy1.GetComponent<EnemyScript>().currentHealth;
                enemy1.GetComponent<EnemyScript>().enemyIndex = 1;
                _enemy1Health = enemy1Health.Value;
                break;
            case 2:
                enemy1 = enemies[0];
                enemy2 = enemies[1];
                enemy1Health.Value = enemy1.GetComponent<EnemyScript>().currentHealth;
                enemy2Health.Value = enemy2.GetComponent<EnemyScript>().currentHealth;
                enemy1.GetComponent<EnemyScript>().enemyIndex = 1;
                enemy2.GetComponent<EnemyScript>().enemyIndex = 2;
                _enemy1Health = enemy1Health.Value;
                _enemy2Health = enemy2Health.Value;
                break;
            case 3:
                enemy1 = enemies[0];
                enemy2 = enemies[1];
                enemy3 = enemies[2];
                enemy1Health.Value = enemy1.GetComponent<EnemyScript>().currentHealth;
                enemy2Health.Value = enemy2.GetComponent<EnemyScript>().currentHealth;
                enemy3Health.Value = enemy3.GetComponent<EnemyScript>().currentHealth;
                enemy1.GetComponent<EnemyScript>().enemyIndex = 1;
                enemy2.GetComponent<EnemyScript>().enemyIndex = 2;
                enemy3.GetComponent<EnemyScript>().enemyIndex = 3;
                _enemy1Health = enemy1Health.Value;
                _enemy2Health = enemy2Health.Value;
                _enemy3Health = enemy3Health.Value;
                break;
            case 4:
                enemy1 = enemies[0];
                enemy2 = enemies[1];
                enemy3 = enemies[2];
                enemy4 = enemies[3];
                enemy1Health.Value = enemy1.GetComponent<EnemyScript>().currentHealth;
                enemy2Health.Value = enemy2.GetComponent<EnemyScript>().currentHealth;
                enemy3Health.Value = enemy3.GetComponent<EnemyScript>().currentHealth;
                enemy4Health.Value = enemy4.GetComponent<EnemyScript>().currentHealth;
                enemy1.GetComponent<EnemyScript>().enemyIndex = 1;
                enemy2.GetComponent<EnemyScript>().enemyIndex = 2;
                enemy3.GetComponent<EnemyScript>().enemyIndex = 3;
                enemy4.GetComponent<EnemyScript>().enemyIndex = 4;
                _enemy1Health = enemy1Health.Value;
                _enemy2Health = enemy2Health.Value;
                _enemy3Health = enemy3Health.Value;
                _enemy4Health = enemy4Health.Value;
                break;
        }
    }

    public void HurtEnemy(float damage, int enemyIndex)
    {
        // subtract the damage from the health of the specified enemy
        switch (enemyIndex)
        {
            case 0:
                enemy1Health.Value -= damage;
                _enemy1Health = enemy1Health.Value;
                if (enemy1Health.Value <= 0)
                {
                    Destroy(enemy1);
                }
                break;
            case 1:
                enemy2Health.Value -= damage;
                _enemy2Health = enemy2Health.Value;
                if (enemy2Health.Value <= 0)
                {
                    Destroy(enemy2);
                }
                break;
            case 2:
                enemy3Health.Value -= damage;
                _enemy3Health = enemy3Health.Value;
                if (enemy3Health.Value <= 0)
                {
                    Destroy(enemy3);
                }
                break;
            case 3:
                enemy4Health.Value -= damage;
                _enemy4Health = enemy4Health.Value;
                if (enemy4Health.Value <= 0)
                {
                    Destroy(enemy4);
                }
                break;
            default:
                Debug.LogError("Invalid enemy index!");
                break;
        }
    }*/
}
