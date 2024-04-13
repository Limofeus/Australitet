using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTestEvent : SmallEvent
{
    public override bool IsPossible()
    {
        return true;
    }
    public override void OnDayStart()
    {
        throw new System.NotImplementedException();
    }

    public SmallTestEvent(string evName)
    {
        eventName = evName;
    }
}
