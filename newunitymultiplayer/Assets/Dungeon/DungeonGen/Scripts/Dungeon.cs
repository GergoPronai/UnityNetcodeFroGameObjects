using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dungeon : MonoSingleton<Dungeon> 
{
	// Dungeon Rooms
	public int DUNGEON_SIZE_X = 20;
	public int DUNGEON_SIZE_Y = 20;
	
	// Size of 3D Model Prefab in World Space
	public float ROOM_SIZE_X = 14; 
	public float ROOM_SIZE_Z = 9;
	
	// Demo Room Prefab
	public GameObject RoomBasicPrefab; 
	
	// Room structure
	public Room[,] rooms;
	
	// Pointer to Boss Room "Demo" GameObject
	private GameObject bossRoom;
	
	public void enableDungeonGeneration()
    {
		GenerateDungeon();
		GenerateGameRooms();
	}
	
	
	public void GenerateDungeon()
	{
		// Create room structure
		rooms = new Room[DUNGEON_SIZE_X,DUNGEON_SIZE_Y];
				
		Room firstRoom = AddRoom(null, 0,0); // null parent because it's the first node
		
		// Generate childrens
		firstRoom.GenerateChildren();
	}
	
	void GenerateGameRooms()
	{
		// For each room in our matrix generate a 3D Model from Prefab
		List<GameObject> roomList = new List<GameObject>();


		foreach (Room room in rooms)
		{
			float worldX = 0;
			float worldZ = 0;
			GameObject g;

			if (room == null) continue;
            if (!room.IsFirstNode())
            {
				worldX = room.x * ROOM_SIZE_X;
				worldZ = room.y * ROOM_SIZE_Z;

				g = GameObject.Instantiate(RoomBasicPrefab, new Vector3(worldX, 0, worldZ), Quaternion.identity) as GameObject;
            }
            else
            {
				g = GameObject.Instantiate(RoomBasicPrefab, new Vector3(worldX, 0, worldZ), Quaternion.identity) as GameObject;
			}
			// Add the room info to the GameObject main script (Demo)
			GameRoom gameRoom = g.GetComponent<GameRoom>();
			gameRoom.room = room;
			
			if (room.IsFirstNode()) 
			{
				bossRoom = g;
				g.name = "Boss Room";
			}
			else g.name = "Room " + room.x + " " + room.y;
			roomList.Add(g);
		}
        foreach (GameObject item in roomList)
        {
			item.transform.SetParent(transform);
        }
	}
	
	// Helper Methods
	
	
	public Room AddRoom(Room parent, int x, int y)
	{
		Room room = new Room(parent, x, y);
		rooms[x,y] = room;
		return room;
	}
}
