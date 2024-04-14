using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatsBigEvent : BigEvent
{
    public bool happened = false;
    public override void ButtonPressed(int buttonId)
    {
        switch (buttonId)
        {
            case 0:
                int newPeople = Random.Range(10, 21);
                Totalres.people.Max += newPeople;
                Totalres.people.Available += newPeople;
                break;
            case 1:
                Totalres.people.Happy -= Random.Range(2, 5);
                break;
        }
    }

    public override bool IsPossible()
    {
        if (happened) return false;
        //!!! опнбепхрэ врн опнькн 10 дмеи
        return true;
    }

    public override void OnDayStart()
    {
        throw new System.NotImplementedException();
    }
}
