using UnityEngine;

public static class Totalres
{
    public static People people = new (100, 0, 0);

    public static Resourse food = new (100, 10);
    public static Resourse rawFood = new (100, 10);

    public static int reviewedPeopleCount = 0;
    public static int sickPeople => people.Sick;
    public static int hungryPeople => people.Hungry;


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

    public static void KillPeople(ref People people, int value)
    {
        if (people.max >= value) 
            people.max -= value;
        else 
            people.max = 0;
    }

    public static void OnTheEndOfDay()
    {
        KillHungryPeople();
        KillSickyPeople();
    }

    private static void KillHungryPeople()
    {
        var deathCoefficient = Random.Range(3, 6) * 0.01f;
        if (GetHungryPeopleFraction() > 0.5f)
            KillPeople(ref people, (int)(people.max * deathCoefficient));
    }

    public static float GetHungryPeopleFraction()
    {
        var hungryPeopleFraction = people.max - food.currentValue;

        if (hungryPeopleFraction < 0) 
            hungryPeopleFraction = 0;

        return hungryPeopleFraction / people.max;
    }

    private static void KillSickyPeople()
    {
        var deathCoefficient = Random.Range(0, 10) * 0.01f;
        KillPeople(ref people, (int)(sickPeople * deathCoefficient));
    }

    public static float GetSickyPeopleFraction()
    {
        var noInfoPeopleCount = people.max - reviewedPeopleCount;
        if (noInfoPeopleCount < 0)
            noInfoPeopleCount = 0;

        var errorFraction = (float)noInfoPeopleCount / people.max * Random.Range(-1f,1f);
        return Mathf.Clamp01(sickPeople / people.max + errorFraction / 2);
    }
}
