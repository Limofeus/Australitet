using UnityEngine;

public class People
{
    private int _hungry;
    private int _sick;
    private int _available;
    private int _happy;

    public int max;

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
    public int Available
    {
        get { return _available; }
        set { _available = Mathf.Clamp(value, 0, max); }
    }    
    
    public int Happy
    {
        get { return _happy; }
        set { _happy = Mathf.Clamp(value, 0, max); }
    }

    public People(int maxPeople, int hungryPeople, int sickPeople, int happy)
    {
        max = maxPeople;
        Hungry = hungryPeople;
        Sick = sickPeople;
        Available = maxPeople;
        Happy = happy;
    }
}
