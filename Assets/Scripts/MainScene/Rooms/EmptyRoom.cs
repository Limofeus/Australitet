using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyRoom : Room
{
    public Transform roomTypeSelectAnchor;
    public Transform roomTypeSelectRoomAnchor;
    public GameObject roomTypeSelectButtonPrefab;
    public GameObject roomTypeSelectPanelPrefab;
    private RoomTypeSelector roomTypeSelector;
    private RoomTypeSelectionPanel roomTypeSelectionPanel;
    public GameObject[] roomPrefabs;
    void Start()
    {
        GameObject typeSelectButtonInstance = PanelManager.Singleton.CreateUiPanel(roomTypeSelectButtonPrefab);
        GameObject roomTypeSelectPanelInstance = PanelManager.Singleton.CreateUiPanel(roomTypeSelectPanelPrefab);
        PanelManager.Singleton.AddTrackingPair(roomTypeSelectAnchor, (RectTransform)typeSelectButtonInstance.transform);
        PanelManager.Singleton.AddTrackingPair(roomTypeSelectRoomAnchor, (RectTransform)roomTypeSelectPanelInstance.transform);
        roomTypeSelector = typeSelectButtonInstance.GetComponent<RoomTypeSelector>();
        roomTypeSelector.emptyRoom = this;
        roomTypeSelectionPanel = roomTypeSelectPanelInstance.GetComponent<RoomTypeSelectionPanel>();
        roomTypeSelectionPanel.room = this;
    }
    public void ShowTypeSelector()
    {
        roomTypeSelector.hide = false;
    }
    public void ShowRoomTypeSelectWindow()
    {
        roomTypeSelectionPanel.panelOpened = true;
        Debug.Log("AAAAAAA");
    }
    public void SelectRoom(int roomId)
    {
        PanelManager.Singleton.RemoveTrackingPair(roomTypeSelectAnchor);
        PanelManager.Singleton.RemoveTrackingPair(roomTypeSelectRoomAnchor);
        GameObject newRoom = Instantiate(roomPrefabs[roomId], transform.position, Quaternion.identity);
        Room roomComp = newRoom.GetComponent<Room>();
        roomComp.roomCoords = roomCoords;
        BuildGrid.Singleton.rooms[BuildGrid.Singleton.rooms.IndexOf(this)] = roomComp;
        Destroy(gameObject);
    }
}
