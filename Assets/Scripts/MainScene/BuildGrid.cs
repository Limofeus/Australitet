using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildGrid : MonoBehaviour
{
    public static BuildGrid Singleton;
    public float cellWidth;
    public float cellHeight;
    public List<Room> rooms = new List<Room>();
    public Dictionary<Vector2Int, GameObject> roomSugggestions = new Dictionary<Vector2Int, GameObject>();
    public Dictionary<Vector2Int, ResourcePlace> resourcePlaces = new Dictionary<Vector2Int, ResourcePlace>();
    public GameObject[] roomPrefabs;
    public AudioClip[] roomAudio;
    public GameObject roomSuggestionPrefab;

    private void Awake()
    {
        Singleton = this;
    }
    private void Start()
    {
        CreateRoom(Vector2Int.zero);
    }
    private void AddRandRoom()
    {
        var posSuggestions = new List<Vector2Int>();
        foreach(var kvp in roomSugggestions)
        {
            Debug.Log(kvp.Key);
            if(kvp.Value != null)
            {
                posSuggestions.Add(kvp.Key);
            }
        }
        int rnd = Random.Range(0, posSuggestions.Count);
        if(Random.Range(0f, 1f) > 0.3f)
        {
            TryBuildRoom(posSuggestions[rnd] * new Vector2(cellWidth, cellHeight), true);
        }
        else
        {
            TryBuildRoom(posSuggestions[rnd] * new Vector2(cellWidth, cellHeight), false);
        }
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    TryBuildRoom(new Vector2(clickPos.x, clickPos.y));
        //}
        //else if (Input.GetMouseButtonDown(1))
        //{
        //    Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    TryBuildRoom(new Vector2(clickPos.x, clickPos.y), true);
        //}
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    AddRandRoom();
        //}
    }
    public void TryBuildRoom(Vector2 buildPos, bool verticalRoom = false)
    {
        Vector2Int roomCoords = new Vector2Int(Mathf.FloorToInt(buildPos.x / cellWidth), Mathf.FloorToInt(buildPos.y / cellHeight));
        TryBuildRoom(roomCoords);
    }
    public void TryBuildRoom(Vector2Int roomCoords, bool verticalRoom = false)
    {
        Debug.Log($"HorCoor: {roomCoords.x} | VerCoor: {roomCoords.y}");

        bool hasNearbyRoom = false;
        foreach(Room room in rooms)
        {
            if(room.roomCoords.y == roomCoords.y)
            {
                if(room.roomCoords.x + 1 == roomCoords.x || room.roomCoords.x - 1 == roomCoords.x)
                {
                    hasNearbyRoom = true;
                }
                else if(room.roomCoords.x == roomCoords.x)
                {
                    hasNearbyRoom = false;
                    break;
                }
            }
            if(verticalRoom && room.roomCoords.y + 1 == roomCoords.y && room.roomCoords.x == roomCoords.x)
            {
                hasNearbyRoom = false;
                break;
            }
        }

        if (hasNearbyRoom)
        {
            CreateRoom(roomCoords, verticalRoom ? 1 : 0);
            if (verticalRoom)
            {
                CreateRoom(roomCoords + Vector2Int.down, 2);
            }
        }
        else
        {
            Debug.Log("No room nearby");
        }
    }
    public void CreateRoom(Vector2Int roomCoords, int roomType = 0) //RoomType: 0 - empty, 1 - stT, 2 - stB, 3 - resourceRoom
    {
        Vector2 roomPos = new Vector2(roomCoords.x * cellWidth, roomCoords.y * cellHeight);
        GameObject createdRoom = Instantiate(roomPrefabs[roomType], roomPos, Quaternion.identity);
        AudioSource.PlayClipAtPoint(roomAudio[roomType], roomPos, MainSceen.CameraMover._currentZoom);
        Room room = createdRoom.GetComponent<Room>();
        room.InitiateRoom(roomCoords);
        rooms.Add(room);
        GameObject suggestionToDestroy;
        if(roomSugggestions.TryGetValue(roomCoords, out suggestionToDestroy))
        {
            Destroy(suggestionToDestroy);
            roomSugggestions.Remove(roomCoords);
        }
        GameObject suggestionToUpdate;
        if (roomSugggestions.TryGetValue(roomCoords + Vector2Int.up, out suggestionToUpdate))
        {
            suggestionToUpdate.GetComponent<RoomSuggestion>().UpdateSuggestion(false);
        }
        TryAddRoomSuggestion(roomCoords + Vector2Int.right);
        TryAddRoomSuggestion(roomCoords + Vector2Int.left);
    }

    private void TryAddRoomSuggestion(Vector2Int suggestionCoords)
    {
        bool placeOccupied = false;
        bool downOccupied = false;
        foreach (Room room in rooms)
        {
            if(room.roomCoords == suggestionCoords)
            {
                placeOccupied = true;
            }
            if(room.roomCoords + Vector2Int.up == suggestionCoords)
            {
                downOccupied = true;
            }
        }
        if (roomSugggestions.ContainsKey(suggestionCoords))
            placeOccupied = true;
        if (resourcePlaces.ContainsKey(suggestionCoords + Vector2Int.down))
            downOccupied = true;

        if (!placeOccupied)
        {
            ResourcePlace resourcePlace;
            Vector2 suggestionPos = new Vector2(suggestionCoords.x * cellWidth, suggestionCoords.y * cellHeight);
            GameObject roomSuggest = Instantiate(roomSuggestionPrefab, suggestionPos, Quaternion.identity);
            roomSugggestions.Add(suggestionCoords, roomSuggest);
            if(resourcePlaces.TryGetValue(suggestionCoords, out resourcePlace))
            {
                roomSuggest.GetComponent<RoomSuggestion>().Init(!downOccupied, suggestionCoords, this, resourcePlace);
            }
            else
            {
                roomSuggest.GetComponent<RoomSuggestion>().Init(!downOccupied, suggestionCoords, this);
            }
        }
    }
}
