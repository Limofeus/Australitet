using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildGrid : MonoBehaviour
{
    public float cellWidth;
    public float cellHeight;
    public List<Room> rooms = new List<Room>();
    public Dictionary<Vector2Int, GameObject> roomSugggestions = new Dictionary<Vector2Int, GameObject>();
    public GameObject testPrefab;
    public GameObject roomSuggestionPrefab;

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
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TryBuildRoom(new Vector2(clickPos.x, clickPos.y));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TryBuildRoom(new Vector2(clickPos.x, clickPos.y), true);
        }
        //else if (Input.GetKey(KeyCode.Space))
        //{
        //    AddRandRoom();
        //}
    }
    private void TryBuildRoom(Vector2 buildPos, bool verticalRoom = false)
    {
        Vector2Int roomCoords = new Vector2Int(Mathf.FloorToInt(buildPos.x / cellWidth), Mathf.FloorToInt(buildPos.y / cellHeight));

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
            CreateRoom(roomCoords);
            if (verticalRoom)
            {
                CreateRoom(roomCoords + Vector2Int.down);
            }
        }
        else
        {
            Debug.Log("No room nearby");
        }
    }
    public void CreateRoom(Vector2Int roomCoords)
    {
        Vector2 roomPos = new Vector2(roomCoords.x * cellWidth, roomCoords.y * cellHeight);
        GameObject createdRoom = Instantiate(testPrefab, roomPos, Quaternion.identity);
        Room room = new Room(roomCoords, createdRoom);
        rooms.Add(room);
        GameObject suggestionToDestroy;
        if(roomSugggestions.TryGetValue(roomCoords, out suggestionToDestroy))
        {
            Destroy(suggestionToDestroy);
            roomSugggestions.Remove(roomCoords);
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
        if (!placeOccupied)
        {
            Vector2 suggestionPos = new Vector2(suggestionCoords.x * cellWidth, suggestionCoords.y * cellHeight);
            GameObject roomSuggest = Instantiate(roomSuggestionPrefab, suggestionPos, Quaternion.identity);
            roomSugggestions.Add(suggestionCoords, roomSuggest);
            roomSuggest.GetComponent<RoomSuggestion>().Init(!downOccupied);
        }
    }
}