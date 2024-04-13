using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector2Int roomCoords;
    public bool IsActive;
    public Transform PanelAnchor;
    public GameObject SelectActivityPanel;
    private RoomTypeSelectionPanel roomTypeSelectionPanel;
    public void InitiateRoom(Vector2Int coords)
    {
        roomCoords = coords;
        OnRoomCreated();
    }

    public virtual void OnRoomCreated()
    {
        if(SelectActivityPanel != null)
            roomTypeSelectionPanel = CreatePanel<RoomTypeSelectionPanel>(PanelAnchor, SelectActivityPanel);
    }
    
    public virtual void OnRoomDestroyed() {}

    public void OnTheEndOfDay() 
    {
        if (IsActive)
            RoomWork();
    }

    protected virtual void RoomWork() { }
    
    public T CreatePanel<T>(Transform trackingPosition, GameObject panelPrefab)
    {
        GameObject createdPanel = PanelManager.Singleton.CreateUiPanel(panelPrefab);
        PanelManager.Singleton.AddTrackingPair(trackingPosition, (RectTransform)createdPanel.transform);
        return createdPanel.GetComponent<T>();
    }

    public Room(){}

    public Room(Vector2Int roomCoords)
    {
        this.roomCoords = roomCoords;
    }
}
