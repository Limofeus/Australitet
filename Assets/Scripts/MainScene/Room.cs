using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2Int roomCoords;
    public GameObject roomVisual;

    public void InitiateRoom(Vector2Int coords, GameObject roomObj)
    {
        roomCoords = coords;
        roomVisual = roomObj;
    }
    public Room(){}

    public Room(Vector2Int roomCoords, GameObject roomVisual)
    {
        this.roomCoords = roomCoords;
        this.roomVisual = roomVisual;
    }
}
