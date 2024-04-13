using UnityEngine;

public static class Totalres
{
    public static People people = new (7, 0, 0, 7);

    public static Resourse food = new (100, 10);
    public static Resourse rawFood = new (100, 10);
    public static Resourse metal = new (100, 10);

    public static int reviewedPeopleCount = 0;
    public static int sickPeople => people.Sick;
    public static int hungryPeople => people.Hungry;

    public static void OnTheEndOfDay()
    {
        KillHungryPeople();
        KillSickyPeople();
    }

    private static void KillHungryPeople()
    {
        var deathCoefficient = Random.Range(3, 6) * 0.01f;
        if (GetHungryPeopleFraction() > 0.5f)
            people.Max -= (int)(people.Max * deathCoefficient);
    }

    private static void KillSickyPeople()
    {
        var deathCoefficient = Random.Range(0, 10) * 0.01f;
        people.Max -= (int)(sickPeople * deathCoefficient);
    }

    public static float GetSickyPeopleFraction()
    {
        var noInfoPeopleCount = people.Max - reviewedPeopleCount;
        if (noInfoPeopleCount < 0)
            noInfoPeopleCount = 0;

        var errorFraction = (float)noInfoPeopleCount / people.Max * Random.Range(-1f,1f);
        return Mathf.Clamp01(sickPeople / people.Max + errorFraction / 2);
    }

    public static float GetHungryPeopleFraction()
    {
        var hungryPeopleFraction = people.Max - food.CurrentValue;

        if (hungryPeopleFraction < 0)
            hungryPeopleFraction = 0;

        return hungryPeopleFraction / people.Max;
    }
}
