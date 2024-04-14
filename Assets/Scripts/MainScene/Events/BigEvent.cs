using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BigEvent : GameEvent
{
    public int eventIconId;
    public int limitTimeSinceCast;
    public string[] buttonTexts;

    public abstract void ButtonPressed(int buttonId);

    public override void UpdateTimeSinceCast()
    {
        if(timeSinceEvent >= 0)
        {
            timeSinceEvent = Mathf.Min(timeSinceEvent + 1, limitTimeSinceCast);
        }
    }
}
