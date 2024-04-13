using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SmallEvent : GameEvent
{
    const int maxSmallEventTime = 4;

    public override void UpdateTimeSinceCast()
    {
        if(timeSinceEvent >= 0)
        {
            timeSinceEvent = Math.Min(maxSmallEventTime, timeSinceEvent + 1);
        }
    }
}
