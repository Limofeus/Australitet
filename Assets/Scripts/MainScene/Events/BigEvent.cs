using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BigEvent : GameEvent
{
    public int eventIconId;
    public int buttonCount;

    public abstract void ButtonPressed(int buttonId);
}
