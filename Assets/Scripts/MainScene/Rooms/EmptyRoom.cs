using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyRoom : Room
{
    private int _peopleForBuildRoom = 3;

    public Transform roomTypeSelectAnchor;
    public Transform roomTypeSelectRoomAnchor;

    public GameObject roomTypeSelectButtonPrefab;
    public GameObject roomTypeSelectPanelPrefab;

    private RoomTypeSelector roomTypeSelector;
    private RoomTypeSelectionPanel roomTypeSelectionPanel;
    public GameObject[] roomPrefabs;
    public AudioClip[] roomSounds;
    override public void OnRoomCreated()
    {
        /*
        GameObject typeSelectButtonInstance = PanelManager.Singleton.CreateUiPanel(roomTypeSelectButtonPrefab);
        GameObject roomTypeSelectPanelInstance = PanelManager.Singleton.CreateUiPanel(roomTypeSelectPanelPrefab);
        PanelManager.Singleton.AddTrackingPair(roomTypeSelectAnchor, (RectTransform)typeSelectButtonInstance.transform);
        PanelManager.Singleton.AddTrackingPair(roomTypeSelectRoomAnchor, (RectTransform)roomTypeSelectPanelInstance.transform);
        roomTypeSelector = typeSelectButtonInstance.GetComponent<RoomTypeSelector>();
        roomTypeSelectionPanel = roomTypeSelectPanelInstance.GetComponent<RoomTypeSelectionPanel>();
        */
        roomTypeSelector = CreatePanel<RoomTypeSelector>(roomTypeSelectAnchor, roomTypeSelectButtonPrefab);
        roomTypeSelectionPanel = CreatePanel<RoomTypeSelectionPanel>(roomTypeSelectRoomAnchor, roomTypeSelectPanelPrefab);
        roomTypeSelector.emptyRoom = this;
        roomTypeSelectionPanel.room = this;
    }
    public void ShowTypeSelector()
    {
        roomTypeSelector.hide = false;
    }
    public void ShowRoomTypeSelectWindow()
    {
        roomTypeSelectionPanel.panelOpened = true;
    }
    public void SelectRoom(int roomId)
    {
        if (Totalres.metal.CurrentValue >= 10f && Totalres.people.TrySetTimeout(_peopleForBuildRoom))
        {
            Totalres.metal.CurrentValue -= 10;
            PanelManager.Singleton.RemoveTrackingPair(roomTypeSelectAnchor);
            PanelManager.Singleton.RemoveTrackingPair(roomTypeSelectRoomAnchor);
            GameObject newRoom = Instantiate(roomPrefabs[roomId], transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(roomSounds[roomId], transform.position, MainSceen.CameraMover._currentZoom);
            Room roomComp = newRoom.GetComponent<Room>();
            roomComp.InitiateRoom(roomCoords);
            BuildGrid.Singleton.rooms[BuildGrid.Singleton.rooms.IndexOf(this)] = roomComp;
            OnRoomDestroyed();
            Destroy(gameObject);
        }
    }
}
