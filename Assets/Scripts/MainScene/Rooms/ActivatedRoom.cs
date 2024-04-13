using System;

public abstract class ActivatedRoom : Room
{
    public void OnStartOfDay()
    {
        if (Totalres.people.Available < 0)
            ClearPeople();
    }

    protected abstract void ClearPeople();
}
