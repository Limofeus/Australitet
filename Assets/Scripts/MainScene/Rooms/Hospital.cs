using UnityEngine;
using UnityEngine.UI;

public class Hospital : Room
{
    public int WorkerForAnalysis = 2;
    public int WorkerForCure = 1;
    public int HospitalCapacity = 30;
    public bool IsAnalyzed = false;
    private int _peopleInHospital = 0;
    private HospitalRoomPanelUI hospitalRoomPanelUI;

    public int WorkerCount => (_peopleInHospital > 0? WorkerForCure : 0) + (IsAnalyzed? WorkerForAnalysis : 0);

    public override void OnRoomCreated()
    {
        hospitalRoomPanelUI = CreatePanel<HospitalRoomPanelUI>();
        hospitalRoomPanelUI.room = this;
        hospitalRoomPanelUI.Init();
    }

    protected override void RoomWork()
    {
        var curePeople = Mathf.Min(_peopleInHospital, Totalres.sickPeople);
        Totalres.people.Sick -= curePeople;
        Totalres.reviewedPeopleCount = HospitalCapacity;
    }

    public void OnSliderValueChanged(int value)
    {
        _peopleInHospital = value;
        IsActive = _peopleInHospital > 0 ? true : false;
    }

    public void OnCheckboxValueChanged(bool isActive)
    {
        IsAnalyzed = isActive;
        IsActive |= isActive ;
    }
}



