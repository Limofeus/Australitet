using UnityEngine;

public class People
{
    private int _hungry;
    private int _sick;
    private int _available;
    private int _happy;
    private int _max;
    private int _timeout;

    public int Max
    {
        get { return _max; }
        set
        {
            _max = value;
            PlayerParams.Singleton?.UpdateRealtimeParams();
        }
    }

    public int Hungry
    {
        get { return _hungry; }
        set 
        {
            _hungry = Mathf.Clamp(value, 0, Max); 
            PlayerParams.Singleton?.UpdateRealtimeParams();
        }
    }
    public int Sick
    {
        get { return _sick; }
        set 
        {
            _sick = Mathf.Clamp(value, 0, Max); 
            PlayerParams.Singleton?.UpdateRealtimeParams();
        }
    }
    public int Available
    {
        get { return _available; }
        set 
        {
            if (value > Max)
                value = Max;
            _available = value; 
            PlayerParams.Singleton?.UpdateRealtimeParams();
        }
    }    
    
    public int Happy
    {
        get { return _happy; }
        set 
        {
            _happy = Mathf.Clamp(value, 0, Max); 
            PlayerParams.Singleton?.UpdateRealtimeParams();
        }
    }

    public bool TrySetTimeout(int value)
    {
        if (Available - value >= 0)
        {
            SetTimeout(value);
            return true;
        }
        return false;
    }

    public void SetTimeout(int value)
    {
        _timeout += value;
        Available -= value;
    }

    public void ReturnTimeoutPeople()
    {
        Available += _timeout;
        _timeout = 0;
    }

    public People(int maxPeople, int hungryPeople, int sickPeople, int happy)
    {
        Max = maxPeople;
        Hungry = hungryPeople;
        Sick = sickPeople;
        Available = maxPeople;
        Happy = happy;
    }

    public void UpdatePeopleCount()
    {
        Hungry = Hungry;
        Sick = Sick;
        Available = Available;
        Happy = Happy;
        Hungry = Hungry;
        SetTimeout(0);
    }
}
