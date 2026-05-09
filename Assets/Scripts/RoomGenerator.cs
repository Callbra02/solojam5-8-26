using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private Vector2Int roomGridSize;

    private Room[,] rooms;
    private List<Room> _enabledRooms =  new List<Room>();

    private Vector2 roomPrefabSize;
    
    private void Start()
    {
        rooms = new Room[roomGridSize.x, roomGridSize.y];

        roomPrefabSize = roomPrefab.GetComponentInChildren<BoxCollider2D>().size;

        for (int i = 0; i < roomGridSize.y; i++)
        {
            for (int j = 0; j < roomGridSize.x; j++)
            {
                Room newRoom = new Room();
                newRoom.roomSize = roomPrefabSize;
                newRoom.roomGridNumber = new Vector2Int(i, j);
                newRoom.isEnabled = false;
                newRoom.roomObject = null;
                
                rooms[i, j] = newRoom;
            }
        }

        CreateRandomRooms();
        CreateStartAndEnd();

        CreatePathway();
    }

    private void CreatePathway()
    {
        for (int i = 0; i < roomGridSize.y; i++)
        {
            for (int j = 0; j < roomGridSize.x; j++)
            {
                Room currentRoom = rooms[i, j];

                if (!currentRoom.isEnabled)
                {
                    continue;
                }

                DeleteIfAlone(currentRoom);
            }
        }
    }

    private void DeleteIfAlone(Room room)
    {
        RoomObject currentRoomObject = room.roomObject.gameObject.GetComponent<RoomObject>();
        currentRoomObject.CheckNeighbors();
        
        if (currentRoomObject.shouldbedeleted)
        {
            DestroyRoom(room);
            
        }
    }

    private void CreateRandomRooms()
    {
        for (int i = 0; i < roomGridSize.y; i++)
        {
            for (int j = 0; j < roomGridSize.x; j++)
            {
                if (Random.Range(0.0f, 1.0f) > 0.4f)
                {
                    Room currentRoom = rooms[i, j];
                    EnableRoom(currentRoom, i, j);
                }
                else
                {
                    Room currentRoom = rooms[i, j];
                    DisableRoom(currentRoom);
                }
            }
        }
    }

    private void CreateStartAndEnd()
    {

        if (_enabledRooms.Count < 2)
        {
            Debug.Log("No enabled rooms!");
            return;
        }
        
        Room startRoom = _enabledRooms[0];
        Room endRoom = _enabledRooms[_enabledRooms.Count - 1];
        
        startRoom.roomObject.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);  //-------------------------Debug
        endRoom.roomObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
    }

    private Vector2 GetRoomWorldPosition(int col, int row)
    {
        Vector2 newPos = transform.position;
        newPos.x += col * roomGridSize.x * 3.55f;
        newPos.y -= row * roomGridSize.y * 2;
        return newPos;
    }

    private void EnableRoom(Room room, int x, int y)
    {
        room.isEnabled = true;
        room.roomObject = Instantiate(roomPrefab, new Vector2(x, y), Quaternion.identity);
        room.roomObject.transform.position = GetRoomWorldPosition(x, y);
        room.roomObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.9f, 1.0f), Random.Range(0.9f, 1.0f), Random.Range(0.5f, 1.0f)); //-----------------Debug
        _enabledRooms.Add(room);
    }

    private void DisableRoom(Room room)
    {
        room.isEnabled = false;
        room.roomObject = null;
    }

    private void DestroyRoom(Room room)
    {
        room.isEnabled = false;
        Destroy(room.roomObject);
        room.roomObject = null;
    }
}

public struct Room
{
    public bool isEnabled;
    public Vector2 roomSize;
    public Vector2Int roomGridNumber;
    public GameObject roomObject;

}
