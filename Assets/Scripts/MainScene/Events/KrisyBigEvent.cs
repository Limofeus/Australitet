using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrisyBigEvent : BigEvent
{
    public bool happened = false;
    public override void ButtonPressed(int buttonId)
    {
        switch (buttonId)
        {
            case 0:
                Totalres.metal.CurrentValue -= 8;
                Totalres.people.SetTimeout(3);
                break;
            case 1:
                //Totalres.people.Sick += Mathf.Max(Mathf.FloorToInt(0.2f * Totalres.people.Max), 1);
                Totalres.food.CurrentValue -= 20;
                Totalres.people.SetTimeout(2);
                break;
        }
    }
    public override bool IsPossible()
    {
        if (happened) return false;
        //!!! опнбепхрэ врн опнькн 10 дмеи
        if (Totalres.weekCount >= 18 && Totalres.metal.CurrentValue >= 8  && Totalres.food.CurrentValue >= 20)
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

    public KrisyBigEvent(string name, string desc, int iconId, string butText1, string butText2)
    {
        eventName = name;
        eventDescription = desc;
        eventIconId = iconId;
        buttonTexts = new string[] { butText1, butText2 };
    }
}
