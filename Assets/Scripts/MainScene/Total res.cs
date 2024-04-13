using UnityEngine;

public static class Totalres
{
    public static People people = new (50, 0, 0, 40);

    public static Resourse food = new (100);
    public static Resourse rawFood = new (110);
    public static Resourse metal = new (120);

    public static int reviewedPeopleCount = 0;

    public static void OnTheEndOfDay()
    {
        Eat();
        Sick();
        KillHungryPeople();
        KillSickyPeople();
        people.ReturnTimeoutPeople();
    }

    private static void Eat()
    {
        var flaw = people.Max - food.CurrentValue;
        if (flaw < 0)
            flaw = 0;
        food.CurrentValue -= people.Max;
        people.Hungry += flaw;
    }

    private static void Sick()
    {
        var randomSmallValue = Random.Range(-5, 5);
        if (randomSmallValue < 0)
            randomSmallValue = 0;
        var sickPeople = Random.Range(0, people.Sick) + randomSmallValue;
        people.Sick += sickPeople;
    }

    private static void KillHungryPeople()
    {
        var deathCoefficient = Random.Range(3, 6) * 0.01f;
        if (GetHungryPeopleFraction() > 0.5f)
        {
            var deadPeople = (int)(people.Hungry * deathCoefficient);
            people.Max -= deadPeople;
            people.Hungry -= deadPeople;
        }
    }

    private static void KillSickyPeople()
    {
        var deathCoefficient = Random.Range(0, 10) * 0.01f;
        var deadPeople = (int)(people.Sick * deathCoefficient);
        people.Max -= deadPeople;
        people.Sick -= deadPeople;
    }

    public static float GetSickyPeopleFraction()
    {
        var noInfoPeopleCount = people.Max - reviewedPeopleCount;
        if (noInfoPeopleCount < 0)
            noInfoPeopleCount = 0;

        var errorFraction = (float)noInfoPeopleCount / people.Max * Random.Range(-1f,1f);
        return Mathf.Clamp01(people.Sick / people.Max + errorFraction / 2);
    }

    public static float GetHungryPeopleFraction()
    {
        return people.Hungry / people.Max;
    }
}
