using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Room
{
    public override void OnRoomCreated()
    {
        base.OnRoomCreated();
        Totalres.metal.MaxValue += 50;
        Totalres.food.MaxValue += 50;
        Totalres.rawFood.MaxValue += 50;
    }
}
