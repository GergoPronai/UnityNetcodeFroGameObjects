using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingRoom : MonoBehaviour
{
    public GameObject Borders;
    public GameObject[] Doors;
    public List<GameObject> locked = new List<GameObject>();
    public GameObject startCam;

    void Start()
    {
        foreach (var item in Doors)
        {
            if (item.activeInHierarchy)
            {
                item.transform.GetChild(3).gameObject.SetActive(true);
                locked.Add(item.transform.GetChild(3).gameObject);                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
