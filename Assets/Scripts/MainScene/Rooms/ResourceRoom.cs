using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ResourceRoom : Room
{
    public string minedResourceName;
    [SerializeField] private Light2D resourceLight;

    public override void OnRoomCreated()
    {
        ResourcePlace resourcePlace = BuildGrid.Singleton.resourcePlaces[roomCoords];
        minedResourceName = resourcePlace.resourceName;
        resourceLight.color = resourcePlace.resourceColor;
        BuildGrid.Singleton.resourcePlaces.Remove(roomCoords);
        Destroy(resourcePlace.gameObject);
        Debug.Log($"NEW RESOURCE ACHIEVED!!!! > {minedResourceName} <");
    }
}
