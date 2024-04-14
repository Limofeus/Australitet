using UnityEngine;

public static class Totalres
{
    public static People people = new (80, 0, 0, 60);

    public static Resourse food = new (100);
    public static Resourse rawFood = new (100);
    public static Resourse metal = new (100);

    public static int reviewedPeopleCount = 0;

    public static int weekCount = 0;

    public static void Eat()
    {
        var flaw = people.Max - food.CurrentValue;
        if (flaw < 0)
            flaw = 0;
        food.CurrentValue -= people.Max;
        people.Hungry += flaw;
    }

    public static void Sick()
    {
        var randomSmallValue = Random.Range(-5, 5);
        if (randomSmallValue < 0)
            randomSmallValue = 0;
        var sickPeople = Random.Range(0, people.Sick) + randomSmallValue;
        people.Sick += sickPeople;
    }

    public static void KillHungryPeople()
    {
        var deathCoefficient = Random.Range(3, 6) * 0.01f;
        if (GetHungryPeopleFraction() > 0.5f)
        {
            var randomSmallValue = Random.Range(0, 3);
            var deadPeople = (int)(people.Hungry * deathCoefficient) + randomSmallValue;
            deadPeople = Mathf.Min(people.Hungry, deadPeople);
            people.Max -= deadPeople;
            people.Hungry -= deadPeople;
            people.Available -= deadPeople;

            if (deadPeople != 0)
                people.Happy -= randomSmallValue;
        }
    }

    public static void KillSickyPeople()
    {
        var deathCoefficient = Random.Range(0, 10) * 0.01f;
        var randomSmallValue = Random.Range(0, 3);
        var deadPeople = (int)(people.Sick * deathCoefficient);
        if (deadPeople == 0 && (people.Sick / people.Max > 0.4f))
            deadPeople += randomSmallValue;
        people.Max -= deadPeople;
        people.Sick -= deadPeople;
        people.Available -= deadPeople;

        if (deadPeople != 0)
            people.Happy -= randomSmallValue;
    }

    public static float GetSickyPeopleFraction()
    {
        return Mathf.Clamp01((float)people.Sick / people.Max);
    }

    public static float GetNoInfoPeopleFraction()
    {
        var noInfoPeopleCount = people.Max - reviewedPeopleCount;
        if (noInfoPeopleCount < 0)
            noInfoPeopleCount = 0;

        return (float)noInfoPeopleCount / people.Max;
    }

    public static float GetHungryPeopleFraction()
    {
        return (float)people.Hungry / people.Max;
    }

    public static float GetHappyPeopleFraction()
    {
        return (float)people.Happy / people.Max;
    }
}
