using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingRoom : MonoBehaviour
{
    public GameObject Borders;
    public GameObject[] Doors;
    private GameObject locked;

    void Start()
    {
        foreach (var item in Doors)
        {
            if (item.activeInHierarchy)
            {
                item.transform.GetChild(3).gameObject.SetActive(true);
                locked = item.transform.GetChild(3).gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
