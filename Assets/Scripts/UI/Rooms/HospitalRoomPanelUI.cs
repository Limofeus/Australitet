using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HospitalRoomPanelUI : RoomPanelUI
{
    public Hospital room;
    private Slider Slider;

    public void Init()
    {
        
    }

    public void OnSliderChanged(Slider slider)
    {
        room.OnSliderValueChanged((int)slider.value);
    }
}
