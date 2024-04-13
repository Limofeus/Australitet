using UnityEngine;

public class People
{
    private int _hungry;
    private int _sick;
    private int _available;
    private int _happy;
    private int _max;

    public int Max
    {
        get { return _max; }
        set
        {
            PlayerParams.Singleton.UpdateParams();
            _max = value;
        }
    }

    public int Hungry
    {
        get { return _hungry; }
        set 
        {
            PlayerParams.Singleton.UpdateParams();
            _hungry = Mathf.Clamp(value, 0, Max); 
        }
    }
    public int Sick
    {
        get { return _sick; }
        set 
        {
            PlayerParams.Singleton.UpdateParams();
            _sick = Mathf.Clamp(value, 0, Max); 
        }
    }
    public int Available
    {
        get { return _available; }
        set 
        {
            PlayerParams.Singleton.UpdateParams();
            _available = Mathf.Clamp(value, 0, Max); 
        }
    }    
    
    public int Happy
    {
        get { return _happy; }
        set 
        {
            PlayerParams.Singleton.UpdateParams();
            _happy = Mathf.Clamp(value, 0, Max); 
        }
    }

    public People(int maxPeople, int hungryPeople, int sickPeople, int happy)
    {
        Max = maxPeople;
        Hungry = hungryPeople;
        Sick = sickPeople;
        Available = maxPeople;
        Happy = happy;
    }
}
