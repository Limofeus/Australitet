using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent
{
    public int timeSinceEvent = -1;
    public string eventName;
    public string eventDescription;

    public void Cast()
    {
        timeSinceEvent = 0;
        OnDayStart();
    }
    public abstract void UpdateTimeSinceCast();
    public abstract bool IsPossible();
    public abstract void OnDayStart();
}
