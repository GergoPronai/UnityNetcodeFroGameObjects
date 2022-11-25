using UnityEngine;
using System.Collections;

public class GameRoom : MonoBehaviour {
	
	public GameObject doorWest, doorEast, doorNorth, doorSouth, Fog;
	public Room room;
	public RoomType roomType;
	public bool StartBool=false;
	
	void Start () 
	{
        // Remove walls if connected
        if (room.parent==null)
        {
			Fog.SetActive(false);
			StartBool = true;
		}
		if (room.IsConnectedTo(room.GetLeft()))
		{
			doorWest.transform.GetChild(0).gameObject.SetActive(true);
			doorWest.transform.GetChild(1).gameObject.SetActive(false);
			doorWest.transform.GetChild(2).gameObject.SetActive(false);
		}
		else
		{
			doorWest.transform.GetChild(0).gameObject.SetActive(false);
			doorWest.transform.GetChild(1).gameObject.SetActive(true);
			doorWest.transform.GetChild(2).gameObject.SetActive(true);
		}
		if (room.IsConnectedTo(room.GetRight()))
		{
			doorEast.transform.GetChild(0).gameObject.SetActive(true);
			doorEast.transform.GetChild(1).gameObject.SetActive(false);
			doorEast.transform.GetChild(2).gameObject.SetActive(false);
		}
		else
		{
			doorEast.transform.GetChild(0).gameObject.SetActive(false);
			doorEast.transform.GetChild(1).gameObject.SetActive(true);
			doorEast.transform.GetChild(2).gameObject.SetActive(true);
		}
		if (room.IsConnectedTo(room.GetTop()))
		{
			doorNorth.transform.GetChild(0).gameObject.SetActive(true);
			doorNorth.transform.GetChild(1).gameObject.SetActive(false);
			doorNorth.transform.GetChild(2).gameObject.SetActive(false);
		}
		else
		{
			doorNorth.transform.GetChild(0).gameObject.SetActive(false);
			doorNorth.transform.GetChild(1).gameObject.SetActive(true);
			doorNorth.transform.GetChild(2).gameObject.SetActive(true);
		}
		if (room.IsConnectedTo(room.GetBottom()))
		{
			doorSouth.transform.GetChild(0).gameObject.SetActive(true);
			doorSouth.transform.GetChild(1).gameObject.SetActive(false);
			doorSouth.transform.GetChild(2).gameObject.SetActive(false);
		}
		else
		{
			doorSouth.transform.GetChild(0).gameObject.SetActive(false);
			doorSouth.transform.GetChild(1).gameObject.SetActive(true);
			doorSouth.transform.GetChild(2).gameObject.SetActive(true);
		}

	}
}
