using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSuggestion : MonoBehaviour
{
    public GameObject buildRoomDownPanel;
    public GameObject buildRoomPanel;
    public Transform panelTrackTransform;
    public Vector2Int coords;
    public BuildGrid buildGrid;
    public void Init(bool bottomAvailable, Vector2Int sugCoords, BuildGrid curBuildGrid)
    {
        CreatePanel(bottomAvailable);
        coords = sugCoords;
        buildGrid = curBuildGrid;
    }
    public void UpdateSuggestion(bool bottomAvailable)
    {
        PanelManager.Singleton.RemoveTrackingPair(panelTrackTransform);
        CreatePanel(bottomAvailable);
    }
    private void CreatePanel(bool bottomAvailable)
    {
        GameObject uiPanel;
        if (bottomAvailable)
        {
            uiPanel = PanelManager.Singleton.CreateUiPanel(buildRoomDownPanel);
        }
        else
        {
            uiPanel = PanelManager.Singleton.CreateUiPanel(buildRoomPanel);
        }
        uiPanel.GetComponent<BuildSuggestionPanel>().suggestionOject = this;
        PanelManager.Singleton.AddTrackingPair(panelTrackTransform, (RectTransform)uiPanel.transform);
    }
    private void OnDestroy()
    {
        PanelManager.Singleton.RemoveTrackingPair(panelTrackTransform);
    }
    public void BuildRoom(bool isVertical)
    {
        buildGrid.TryBuildRoom(coords, isVertical);
    }

}
