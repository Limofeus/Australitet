public class Kitchen : Room
{
    private int foodPerDay = 3;
    private int rawFoodPerDay = 5;

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
}
