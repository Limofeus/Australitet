using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resourse
{
    private int _maxValue;
    private int _currentValue;

    public int MaxValue
    {
        get { return _maxValue; }
        set 
        {
            PlayerParams.Singleton.UpdateParams();
            _maxValue = value; 
        }
    }

    public int CurrentValue 
    {
        get { return _currentValue; }
        set
        {
            PlayerParams.Singleton.UpdateParams();
            _currentValue = Mathf.Clamp(value, 0, _maxValue);
        }
    }

    public Resourse(int mx, int cur)
    {
        MaxValue = mx;
        CurrentValue = cur;
    }
}
