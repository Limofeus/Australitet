using UnityEngine;

public class Kitchen : ActivatedRoom
{
    private int foodPerDay = 7;
    private int rawFoodPerDay = 9;
    private KitchenRoomPanelUI kitchenRoomPanelUI;

    public int WorkerCount = 2;

    public override void OnRoomCreated()
    {
        kitchenRoomPanelUI = CreatePanel<KitchenRoomPanelUI>();
        kitchenRoomPanelUI.room = this;
        kitchenRoomPanelUI.Init();
    }

    protected override void ClearPeople()
    {
        if (IsActive)
            Totalres.people.Available += WorkerCount;
        IsActive = false;
        kitchenRoomPanelUI.Init();
    }

    protected override void RoomWork()
    {
        var food = (int)(foodPerDay * (float)(Mathf.Min(Totalres.rawFood.CurrentValue, rawFoodPerDay) / rawFoodPerDay));
        Totalres.food.CurrentValue += food;
        Totalres.rawFood.CurrentValue -= food;
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
