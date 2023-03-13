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
                foreach (GameObject Obj in item.GetComponent<DoorsScript>().lockobjs)
                {
                    Obj.SetActive(true);
                    locked.Add(Obj);
                }                              
            }
        }
    }

}
