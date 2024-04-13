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
            _maxValue = value; 
            PlayerParams.Singleton?.UpdateParams();
        }
    }

    public int CurrentValue 
    {
        get { return _currentValue; }
        set
        {
            _currentValue = Mathf.Clamp(value, 0, _maxValue);
            PlayerParams.Singleton?.UpdateParams();
        }
    }

    public Resourse(int mx, int cur)
    {
        MaxValue = mx;
        CurrentValue = cur;
    }
}
