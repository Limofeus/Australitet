using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBed : Room
{
    int Capacity = 50;
    int Resources = 10;

    protected override void RoomWork()
    {
        Totalres.AddRes(ref Totalres.rawFood, Resources);
    }
}
