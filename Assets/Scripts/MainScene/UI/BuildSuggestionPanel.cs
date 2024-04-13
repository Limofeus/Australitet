using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSuggestionPanel : MonoBehaviour
{
    private int _peopleForBuildRoom = 2;
    public RoomSuggestion suggestionOject;

    public void BuildNormal()
    {
        if (Totalres.people.TrySetTimeout(_peopleForBuildRoom))
            suggestionOject.BuildRoom(false);
    }
    public void BuildVertical()
    {
        if (Totalres.people.TrySetTimeout(_peopleForBuildRoom))
            suggestionOject.BuildRoom(true);
    }
}
