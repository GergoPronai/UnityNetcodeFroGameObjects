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

    public NetworkVariable<float> enemy1Health = new NetworkVariable<float>(100f);
    public NetworkVariable<float> enemy2Health = new NetworkVariable<float>(100f);
    public NetworkVariable<float> enemy3Health = new NetworkVariable<float>(100f);
    public NetworkVariable<float> enemy4Health = new NetworkVariable<float>(100f);

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        switch (enemies.Length)
        {
            case 1:
                enemy1 = enemies[0];
                break;
            case 2:
                enemy1 = enemies[0];
                enemy2 = enemies[1];
                break;
            case 3:
                enemy1 = enemies[0];
                enemy2 = enemies[1];
                enemy3 = enemies[2];
                break;
            case 4:
                enemy1 = enemies[0];
                enemy2 = enemies[1];
                enemy3 = enemies[2];
                enemy4 = enemies[3];
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
                if (enemy1Health.Value <= 0)
                {
                    Destroy(enemy1);
                }
                break;
            case 1:
                enemy2Health.Value -= damage;
                if (enemy2Health.Value <= 0)
                {
                    Destroy(enemy2);
                }
                break;
            case 2:
                enemy3Health.Value -= damage;
                if (enemy3Health.Value <= 0)
                {
                    Destroy(enemy3);
                }
                break;
            case 3:
                enemy4Health.Value -= damage;
                if (enemy4Health.Value <= 0)
                {
                    Destroy(enemy4);
                }
                break;
            default:
                Debug.LogError("Invalid enemy index!");
                break;
        }
    }
}
