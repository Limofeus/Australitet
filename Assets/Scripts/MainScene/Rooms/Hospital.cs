using UnityEngine;
using UnityEngine.UI;

public class Hospital : Room
{
    private int _hospitalCapacity = 30;
    private HospitalRoomPanelUI hospitalRoomPanelUI;

    public override void OnRoomCreated()
    {
        hospitalRoomPanelUI = CreatePanel<HospitalRoomPanelUI>();
        hospitalRoomPanelUI.room = this;
        hospitalRoomPanelUI.Init();
    }

    protected override void RoomWork()
    {
        var curePeople = Mathf.Min(_hospitalCapacity, Totalres.sickPeople);

    }

    public void OnSliderValueChanged(int value)
    {

    }
}



