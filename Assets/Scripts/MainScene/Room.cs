using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector2Int roomCoords;

    public void InitiateRoom(Vector2Int coords)
    {
        roomCoords = coords;
    }
    public Room(){}

    public Room(Vector2Int roomCoords)
    {
        this.roomCoords = roomCoords;
    }
}
