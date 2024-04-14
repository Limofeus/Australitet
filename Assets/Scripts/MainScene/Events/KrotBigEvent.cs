using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class KrotBigEvent : BigEvent
{
    public bool happened = false;
    public override void ButtonPressed(int buttonId)
    {
        switch (buttonId)
        {
            case 0:
                Totalres.metal.CurrentValue -= 6;
                Totalres.people.SetTimeout(4);
                break;
            case 1:
                Totalres.people.Sick += Mathf.Max(Mathf.FloorToInt(0.2f * Totalres.people.Max), 1);
                Totalres.people.SetTimeout(7);
                break;
        }
    }
    public override bool IsPossible()
    {
        if (happened) return false;
        //!!! опнбепхрэ врн опнькн 10 дмеи
        if (Totalres.weekCount >= 24 && Totalres.metal.CurrentValue >= 6)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void OnDayStart()
    {
        happened = true;
    }

    public KrotBigEvent(string name, string desc, int iconId, string butText1, string butText2)
    {
        eventName = name;
        eventDescription = desc;
        eventIconId = iconId;
        buttonTexts = new string[] { butText1, butText2 };
    }
}