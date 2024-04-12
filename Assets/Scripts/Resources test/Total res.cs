public static class Totalres
{
    public static Resourse food;
    public static Resourse people;
    public static Resourse rawFood;

    public static int sick;
    public static float hungryPeopleFraction;

    public static void AddRes(ref Resourse res, int value)
    {
        var newValue = res.currentValue + value;
        if (newValue > res.maxValue)
            res.currentValue = newValue;
        else
            res.currentValue = res.maxValue;
    }

    public static void RedRes(ref Resourse res, int value)
    {
        if (res.currentValue >= value) 
            res.currentValue -= value;
        else 
            res.currentValue = 0;
    }

    public static void OnTheEndOfDay()
    {
        hungryPeopleFraction = GetHungryPeopleFraction();
        KillHungryPeople();
    }

    private static void KillHungryPeople()
    {
        var deathCoefficient = UnityEngine.Random.Range(3, 6)* 0.01f;
        if (hungryPeopleFraction > people.maxValue * 0.5f)
            RedRes(ref people, (int)(people.maxValue * deathCoefficient));
        hungryPeopleFraction = GetHungryPeopleFraction();
    }

    private static float GetHungryPeopleFraction()
    {
        var hungryPeopleFraction = people.maxValue - food.currentValue;

        if (hungryPeopleFraction < 0) 
            hungryPeopleFraction = 0;

        return hungryPeopleFraction / people.maxValue;
    }
}
