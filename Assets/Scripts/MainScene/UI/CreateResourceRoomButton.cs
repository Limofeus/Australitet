using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateResourceRoomButton : MonoBehaviour
{
    public RoomSuggestion suggestionOject;
    public void BuildResourceRoom()
    {
        suggestionOject.BuildResourceRoom();
    }
}
