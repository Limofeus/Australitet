using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recreation : Room
{
    public override void OnRoomCreated()
    {
        //RoomPanelUI testRoomPanelUI = CreatePanel<RoomPanelUI>(panelPivot, panelPrefab);
        //testRoomPanelUI.thisPanelsResearchRoom = this;
    }

    public void OnButtonTest()
    {
        Debug.Log("RESEARCH");
        Debug.Log(roomCoords);
    }
}
