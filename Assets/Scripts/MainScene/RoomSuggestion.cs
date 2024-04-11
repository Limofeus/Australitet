using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSuggestion : MonoBehaviour
{
    public GameObject buildRoomDownPanel;
    public GameObject buildRoomPanel;
    public Transform panelTrackTransform;
    public void Init(bool bottomAvailable)
    {
        if (bottomAvailable)
        {
            PanelManager.Singleton.AddTrackingPair(panelTrackTransform, (RectTransform)PanelManager.Singleton.CreateUiPanel(buildRoomDownPanel).transform);
        }
        else
        {
            PanelManager.Singleton.AddTrackingPair(panelTrackTransform, (RectTransform)PanelManager.Singleton.CreateUiPanel(buildRoomPanel).transform);
        }
    }
    public void UpdateSuggestion(bool bottomAvailable)
    {
        PanelManager.Singleton.RemoveTrackingPair(panelTrackTransform);
        if (bottomAvailable)
        {
            PanelManager.Singleton.AddTrackingPair(panelTrackTransform, (RectTransform)PanelManager.Singleton.CreateUiPanel(buildRoomDownPanel).transform);
        }
        else
        {
            PanelManager.Singleton.AddTrackingPair(panelTrackTransform, (RectTransform)PanelManager.Singleton.CreateUiPanel(buildRoomPanel).transform);
        }
    }
    private void OnDestroy()
    {
        PanelManager.Singleton.RemoveTrackingPair(panelTrackTransform);
    }
}
