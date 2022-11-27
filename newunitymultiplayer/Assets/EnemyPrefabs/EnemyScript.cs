using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject[] EnemyNormalGameObjects;
    public GameObject[] EnemyDamagedGameObjects;

    void Start()
    {
        foreach (GameObject item in EnemyDamagedGameObjects)
        {
            item.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
