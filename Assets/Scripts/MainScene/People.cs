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
            _max = value;
            PlayerParams.Singleton?.UpdateParams();
        }
    }

    public int Hungry
    {
        get { return _hungry; }
        set 
        {
            _hungry = Mathf.Clamp(value, 0, Max); 
            PlayerParams.Singleton?.UpdateParams();
        }
    }
    public int Sick
    {
        get { return _sick; }
        set 
        {
            _sick = Mathf.Clamp(value, 0, Max); 
            PlayerParams.Singleton?.UpdateParams();
        }
    }
    public int Available
    {
        get { return _available; }
        set 
        {
            _available = Mathf.Clamp(value, -100, Max); 
            PlayerParams.Singleton?.UpdateParams();
        }
    }    
    
    public int Happy
    {
        get { return _happy; }
        set 
        {
            _happy = Mathf.Clamp(value, 0, Max); 
            PlayerParams.Singleton?.UpdateParams();
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
