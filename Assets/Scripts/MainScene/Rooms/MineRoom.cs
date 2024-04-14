using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MineRoom : ActivatedRoom
{
    [SerializeField] private Light2D resourceLight;
    private int metalPerDay = 3;
    private MineRoomPanelUI mineRoomPanelUI;

    public int WorkerCount = 3;
    public string minedResourceName;

    public override void OnRoomCreated()
    {
        ResourcePlace resourcePlace = BuildGrid.Singleton.resourcePlaces[roomCoords];
        minedResourceName = resourcePlace.resourceName;
        resourceLight.color = resourcePlace.resourceColor;
        BuildGrid.Singleton.resourcePlaces.Remove(roomCoords);
        Destroy(resourcePlace.gameObject);
        Debug.Log($"NEW RESOURCE ACHIEVED!!!! > {minedResourceName} <");
        mineRoomPanelUI = CreatePanel<MineRoomPanelUI>();
        mineRoomPanelUI.room = this;
        mineRoomPanelUI.Init();
    }

    protected override void ClearPeople()
    {
        if (IsActive)
            Totalres.people.Available += WorkerCount;
        IsActive = false;
    }

    protected override void RoomWork()
    {
        Totalres.metal.CurrentValue += metalPerDay;
    }

    public void OnCheckBoxChanged(bool isOn)
    {
        IsActive = isOn;
        if (IsActive)
            Totalres.people.Available -= WorkerCount;
        else
            Totalres.people.Available += WorkerCount;
    }
}
