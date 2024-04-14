using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSuggestion : MonoBehaviour
{
    public GameObject buildRoomDownPanel;
    public GameObject buildRoomPanel;
    public GameObject buildResourceRoomPanel;
    public Transform panelTrackTransform;
    public Vector2Int coords;
    public BuildGrid buildGrid;
    private ResourcePlace linkedResourcePlace;

    public AudioClip[] miningAudio;
    
    private float soundVolume;
    public void Init(bool bottomAvailable, Vector2Int sugCoords, BuildGrid curBuildGrid, ResourcePlace resourcePlace = null)
    {
        linkedResourcePlace = resourcePlace;
        coords = sugCoords;
        buildGrid = curBuildGrid;
        soundVolume = Mathf.Pow(10f, PlayerPrefs.GetFloat("AmbientVol", 1f) / 20f);
        if(linkedResourcePlace == null)
        {
            CreatePanel(bottomAvailable);
        }
        else
        {
            CreateResourceRoomPanel();
        }
    }
    public void UpdateSuggestion(bool bottomAvailable)
    {
        if(linkedResourcePlace == null)
        {
            PanelManager.Singleton.RemoveTrackingPair(panelTrackTransform);
            CreatePanel(bottomAvailable);
        }
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
    private void CreateResourceRoomPanel()
    {
        GameObject uiPanel = PanelManager.Singleton.CreateUiPanel(buildResourceRoomPanel);
        uiPanel.GetComponent<CreateResourceRoomButton>().suggestionOject = this;
        PanelManager.Singleton.AddTrackingPair(panelTrackTransform, (RectTransform)uiPanel.transform);
    }
    private void OnDestroy()
    {
        PanelManager.Singleton.RemoveTrackingPair(panelTrackTransform);
    }
    public void BuildRoom(bool isVertical)
    {
        AudioSource.PlayClipAtPoint(miningAudio[Random.Range(0, miningAudio.Length)], transform.position, soundVolume);
        buildGrid.TryBuildRoom(coords, isVertical);
    }
    public void BuildResourceRoom()
    {
        AudioSource.PlayClipAtPoint(miningAudio[Random.Range(0, miningAudio.Length)], transform.position, soundVolume);
        buildGrid.CreateRoom(coords, 3);

    }
}
