using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Research : Room
{
    public GameObject panelPrefab;
    public Transform panelPivot;
    public override void OnRoomCreated()
    {
        TestRoomPanelUI testRoomPanelUI = CreatePanel<TestRoomPanelUI>(panelPivot, panelPrefab);
        testRoomPanelUI.thisPanelsResearchRoom = this;
    }

    public void OnButtonTest()
    {
        Debug.Log("RESEARCH");
        Debug.Log(roomCoords);
    }
}
