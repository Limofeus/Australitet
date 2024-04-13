using UnityEngine;

public class People
{
    private int _hungry;
    private int _sick;
    private int _worked;

    public int max;

    public int Worked 
    { 
        get { return _worked; }
        set { _worked = Mathf.Clamp(value, 0, max); }
    }
    public int Hungry
    {
        get { return _hungry; }
        set { _hungry = Mathf.Clamp(value, 0, max); }
    }
    public int Sick
    {
        get { return _sick; }
        set { _sick = Mathf.Clamp(value, 0, max); }
    }

    public int Available => max - _worked;

    public People(int maxPeople, int hungryPeople, int sickPeople)
    {
        max = maxPeople;
        Hungry = hungryPeople;
        Sick = sickPeople;
    }
}
