using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoomPanelUI : MonoBehaviour
{
    public Research thisPanelsResearchRoom;

    public void ActivateResearch()
    {
        thisPanelsResearchRoom.OnButtonTest();
    }
}
