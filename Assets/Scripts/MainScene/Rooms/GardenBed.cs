using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBed : ActivatedRoom
{
    private int _foodPerDay = 10;
    private int _infectionProbabilityPerProcent = 30; 
    private int _sickPeoplePerDay = 5;
    private GardenRoomPanelUI gardenRoomPanelUI;
    private int condition;

    public int WorkerCount = 2;
    
    public override void OnRoomCreated()
    {
        gardenRoomPanelUI = CreatePanel<GardenRoomPanelUI>();
        gardenRoomPanelUI.room = this;
        gardenRoomPanelUI.Init();
    }

    protected override void ClearPeople()
    {
        if (IsActive)
            Totalres.people.Available += WorkerCount;
        IsActive = false;
        gardenRoomPanelUI.Init();
    }

    public void OnSliderChanged(int sliderCondition)
    {
        condition = sliderCondition;

        if (sliderCondition != 0 && !IsActive)
        {
            IsActive = true;
            Totalres.people.Available -= WorkerCount;
        }
        else if (sliderCondition == 0 && IsActive)
        {
            IsActive = false;
            Totalres.people.Available += WorkerCount;
        }   
    }

    protected override void RoomWork()
    {
        Totalres.rawFood.CurrentValue += _foodPerDay * condition;
        if (Random.Range(0,100) < _infectionProbabilityPerProcent)
            Totalres.people.Sick += _sickPeoplePerDay * (condition - 1);
    }
}
