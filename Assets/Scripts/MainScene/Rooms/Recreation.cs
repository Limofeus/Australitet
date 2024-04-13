using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recreation : ActivatedRoom
{
    public int RecreationCapacity = 20;
    public int peopleInRoom = 0;
    private RecreationRoomPanelUI recreationRoomPanelUI;

    public override void OnRoomCreated()
    {
        recreationRoomPanelUI = CreatePanel<RecreationRoomPanelUI>();
        recreationRoomPanelUI.room = this;
        recreationRoomPanelUI.Init();
    }

    protected override void RoomWork()
    {
        var sadPeople = Mathf.Min(peopleInRoom, Totalres.people.Max - Totalres.people.Happy);
        Totalres.people.Happy += sadPeople;
    }

    public void OnSliderValueChanged(int value)
    {
        if (peopleInRoom != value)
        {
            Totalres.people.Available += peopleInRoom;
            if (value > 0)
            {
                if (value <= Totalres.people.Available)
                {
                    Totalres.people.Available -= value;
                }
                else
                {
                    if (Totalres.people.Available > 0)
                    {
                        value = peopleInRoom;
                    }
                    else
                    {
                        value = 0;
                    }
                }
            }
        }
        peopleInRoom = value;
        IsActive = peopleInRoom > 0 ? true : false;
    }

    protected override void ClearPeople()
    {
        peopleInRoom = 0;
        recreationRoomPanelUI.Init();
    }
}
