using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent
{
    public string eventName;
    public string eventDescription;

    public abstract bool IsPossible();
    public abstract void OnDayStart();
    public abstract void OnDayEnd();
}
