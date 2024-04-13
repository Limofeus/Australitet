using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Hospital : Room
{
    public int WorkerForCure = 1;
    public int HospitalCapacity = 30;
    public int peopleInHospital = 0;
    private HospitalRoomPanelUI hospitalRoomPanelUI;

    public override void OnRoomCreated()
    {
        hospitalRoomPanelUI = CreatePanel<HospitalRoomPanelUI>();
        hospitalRoomPanelUI.room = this;
        hospitalRoomPanelUI.Init();
    }

    protected override void RoomWork()
    {
        var curePeople = Mathf.Min(peopleInHospital, Totalres.sickPeople);
        Totalres.people.Sick -= curePeople;
        Totalres.reviewedPeopleCount = HospitalCapacity;
    }

    public void OnSliderValueChanged(int value)
    {
        if (Totalres.people.Available > 0)
        {
            Totalres.people.Available += (peopleInHospital + (peopleInHospital > 0 ? 1 : 0));
            if ((int)value > 0)
            {
                if ((int)value + WorkerForCure <= Totalres.people.Available)
                {
                    Totalres.people.Available -= (int)value + WorkerForCure;
                }
                else
                {
                    if (Totalres.people.Available > WorkerForCure)
                    {
                        value = peopleInHospital;
                    }
                    else
                    {
                        value = 0;
                    }
                }
            }
        }
        Debug.Log("aaa " + Totalres.people.Available);
        peopleInHospital = value;
        IsActive = peopleInHospital > 0 ? true : false;
    }
}



