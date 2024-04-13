public class Kitchen : Room
{
    private int foodPerDay = 3;
    private int rawFoodPerDay = 5;

    protected override void RoomWork()
    {
        var hasEnoughSpace = Totalres.food.currentValue + foodPerDay <= Totalres.food.maxValue;
        var hasEnoughRawFood = Totalres.rawFood.currentValue >= rawFoodPerDay;
        if (hasEnoughSpace && hasEnoughRawFood)
        {
            Totalres.AddRes(ref Totalres.food, foodPerDay);
            Totalres.RedRes(ref Totalres.rawFood, rawFoodPerDay);
        }
    }
}
