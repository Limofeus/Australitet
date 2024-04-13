using UnityEngine;

public class Kitchen : ActivatedRoom
{
    private int foodPerDay = 3;
    private int rawFoodPerDay = 5;
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
        IsActive = false;
    }

    protected override void RoomWork()
    {
        var hasEnoughSpace = Totalres.food.CurrentValue + foodPerDay <= Totalres.food.MaxValue;
        var hasEnoughRawFood = Totalres.rawFood.CurrentValue >= rawFoodPerDay;
        if (hasEnoughSpace && hasEnoughRawFood)
        {
            Totalres.food.CurrentValue += foodPerDay;
            Totalres.rawFood.CurrentValue -= rawFoodPerDay;
        }
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
