using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyRoom : Room
{
    public Transform roomTypeSelectAnchor;
    public Transform roomTypeSelectRoomAnchor;
    public GameObject roomTypeSelectButtonPrefab;
    public GameObject roomTypeSelectPanelPrefab;
    void Start()
    {
        GameObject typeSelectButtonInstance = PanelManager.Singleton.CreateUiPanel(roomTypeSelectButtonPrefab);
        GameObject roomTypeSelectPanelinstance = PanelManager.Singleton.CreateUiPanel(roomTypeSelectPanelPrefab);
        PanelManager.Singleton.AddTrackingPair(roomTypeSelectAnchor, (RectTransform)typeSelectButtonInstance.transform);
        PanelManager.Singleton.AddTrackingPair(roomTypeSelectRoomAnchor, (RectTransform)roomTypeSelectPanelinstance.transform);
    }
}
