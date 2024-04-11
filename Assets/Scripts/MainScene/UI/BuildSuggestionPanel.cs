using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSuggestionPanel : MonoBehaviour
{
    public RoomSuggestion suggestionOject;

    public void BuildNormal()
    {
        suggestionOject.BuildRoom(false);
    }
    public void BuildVertical()
    {
        suggestionOject.BuildRoom(true);
    }
}
