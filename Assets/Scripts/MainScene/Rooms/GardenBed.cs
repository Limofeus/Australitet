using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBed : Room
{
    private int Resources = 10;

    protected override void RoomWork()
    {
        Totalres.rawFood.CurrentValue += Resources;
    }
}
