using UnityEngine;

public static class Totalres
{
    public static Resourse food = new Resourse(100, 10);
    public static Resourse people= new Resourse(100, 100);
    public static Resourse rawFood= new Resourse(100, 10);
    public static Resourse sickPeople = new Resourse(100, 0);

    public static int numOfHospitals = 2;
    public static int hospitalCapacity = 20;
    public static float sickPeopleFraction;
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

    public static void KillPip(ref Resourse res, int value)
    {
        if (res.maxValue >= value) 
            res.maxValue -= value;
        else 
            res.maxValue = 0;
    }

    public static void OnTheEndOfDay()
    {
        hungryPeopleFraction = GetHungryPeopleFraction();
        sickPeopleFraction = GetSickyPeopleFraction();
        KillHungryPeople();
        GetPeopleSick();
        KillSickyPeople();
        Debug.Log(sickPeopleFraction);
    }

    private static void KillHungryPeople()
    {
        var deathCoefficient = UnityEngine.Random.Range(3, 6) * 0.01f;
        if (hungryPeopleFraction > 0.5f)
            KillPip(ref people, (int)(people.maxValue * deathCoefficient));
        hungryPeopleFraction = GetHungryPeopleFraction();
    }

    private static float GetHungryPeopleFraction()
    {
        var hungryPeopleFraction = people.maxValue - food.currentValue;

        if (hungryPeopleFraction < 0) 
            hungryPeopleFraction = 0;

        return hungryPeopleFraction / people.maxValue;
    }

    private static void GetPeopleSick()
    {
        var sickCoefficient = UnityEngine.Random.Range(3, 6) * 0.01f;
        var possibleToSick = people.maxValue * sickCoefficient;
        if (sickPeople.currentValue + possibleToSick < people.maxValue - sickPeople.currentValue * sickCoefficient)
            sickPeople.currentValue += (int)possibleToSick;
        else
            sickPeople.currentValue = people.maxValue;
    }

    private static void KillSickyPeople()
    {
        var deathCoefficient = UnityEngine.Random.Range(2, 4) * 0.01f;
        if (sickPeople.currentValue >  hospitalCapacity * numOfHospitals)
        {
            RedRes(ref sickPeople, (int)(sickPeople.currentValue * deathCoefficient));
            KillPip(ref people, (int)(sickPeople.currentValue * deathCoefficient));
        }
            
        sickPeopleFraction = GetSickyPeopleFraction();
    }

    private static float GetSickyPeopleFraction()
    {
        float sickPeopleFraction = people.maxValue - sickPeople.currentValue;
        if (sickPeopleFraction < 0)
            sickPeopleFraction = 0;
        
        return sickPeopleFraction / people.maxValue;
    }
}
