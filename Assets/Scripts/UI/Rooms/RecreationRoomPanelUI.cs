using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecreationRoomPanelUI : RoomPanelUI
{
    public Research room;
    [SerializeField] private Slider _curePeople;

    public override void ActivateResearch()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log("asdad");
    }
}