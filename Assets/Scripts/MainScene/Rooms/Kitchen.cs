using UnityEngine;

public class Kitchen : ActivatedRoom
{

    private int foodPerDay = 4;
    private int rawFoodPerDay = 6;
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
        var hasEnoughRawFood = Totalres.rawFood.CurrentValue >= rawFoodPerDay;
        if (hasEnoughRawFood)
        {
            Totalres.food.CurrentValue += foodPerDay;
            Totalres.rawFood.CurrentValue -= rawFoodPerDay;
        }
        else
        {
            Totalres.food.CurrentValue += (int)(foodPerDay * (float)(Totalres.rawFood.CurrentValue/ rawFoodPerDay));
            Totalres.rawFood.CurrentValue = 0;
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
