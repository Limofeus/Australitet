using UnityEngine;

public class Hospital : ActivatedRoom
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
        var curePeople = Mathf.Min(peopleInHospital, Totalres.people.Sick);
        Totalres.people.Sick -= curePeople;
        Totalres.reviewedPeopleCount += HospitalCapacity;
    }

    public void OnSliderValueChanged(int value)
    {
        if (peopleInHospital != value)
        {
            Totalres.people.Available += (peopleInHospital + (peopleInHospital > 0 ? WorkerForCure : 0));
            if (value > 0)
            {
                if (value + WorkerForCure <= Totalres.people.Available)
                {
                    Totalres.people.Available -= value + WorkerForCure;
                }
                else
                {
                    if (Totalres.people.Available > WorkerForCure)
                    {
                        value = peopleInHospital;
                    }
                    else
                    {
                        hospitalRoomPanelUI.Init();
                        value = 0;
                    }
                }
            }
        }
        peopleInHospital = value;
        IsActive = peopleInHospital > 0;
    }

    protected override void ClearPeople()
    {
        if (IsActive)
            Totalres.people.Available += peopleInHospital + WorkerForCure;
        peopleInHospital = 0;
        IsActive = false;
        hospitalRoomPanelUI.Init();
    }
}



