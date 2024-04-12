using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector2Int roomCoords;
    public bool IsActive;
    public void InitiateRoom(Vector2Int coords)
    {
        roomCoords = coords;
        OnRoomCreated();
    }

    public virtual void OnRoomCreated() { }
    public virtual void OnRoomDestroyed() { }

    public virtual void OnTheEndOfDay() { }
    
    public Room(){}

    public Room(Vector2Int roomCoords)
    {
        this.roomCoords = roomCoords;
    }
}
