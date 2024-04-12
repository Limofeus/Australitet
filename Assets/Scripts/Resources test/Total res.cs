using UnityEngine;

public static class Totalres
{
    public static People people = new (100, 0, 0);

    public static Resourse food = new (100, 10);
    public static Resourse rawFood = new (100, 10);

    public static int numOfHospitals = 2;
    public static int hospitalCapacity = 20;

    public static float sickPeopleFraction;
    public static float hungryPeopleFraction;

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
        hungryPeopleFraction = GetHungryPeopleFraction();
        sickPeopleFraction = GetSickyPeopleFraction();
        KillHungryPeople();
        KillSickyPeople();
        Debug.Log(sickPeopleFraction);
    }

    private static void KillHungryPeople()
    {
        var deathCoefficient = UnityEngine.Random.Range(3, 6) * 0.01f;
        if (hungryPeopleFraction > 0.5f)
            KillPeople(ref people, (int)(people.max * deathCoefficient));
        hungryPeopleFraction = GetHungryPeopleFraction();
    }

    private static float GetHungryPeopleFraction()
    {
        var hungryPeopleFraction = people.max - food.currentValue;

        if (hungryPeopleFraction < 0) 
            hungryPeopleFraction = 0;

        return hungryPeopleFraction / people.max;
    }

    private static void KillSickyPeople()
    {
        var deathCoefficient = Random.Range(2, 4) * 0.01f;
        if (sickPeople > hospitalCapacity * numOfHospitals)
            KillPeople(ref people, (int)(sickPeople * deathCoefficient));
            
        sickPeopleFraction = GetSickyPeopleFraction();
    }

    private static float GetSickyPeopleFraction()
    {   
        int newSickPeople = (int)(Random.Range(2, 4) * 0.01f * people.max);
        
        return 1 - (sickPeople + newSickPeople) / people.max;
    }
}
