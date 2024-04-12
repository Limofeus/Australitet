using UnityEngine;

public class People
{
    private int _available;
    private int _hungry;
    private int _sick;

    public int max;

    public int Available 
    { 
        get { return _available; }
        set { _available = Mathf.Clamp(value, 0, max); }
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

    public People(int maxPeople, int hungryPeople, int sickPeople)
    {
        max = maxPeople;
        Hungry = hungryPeople;
        Sick = sickPeople;
    }
}
