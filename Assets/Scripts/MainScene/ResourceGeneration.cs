using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGeneration : MonoBehaviour
{
    public GameObject[] resourcesToGenerate;
    public int maxOffset;

    private void Start()
    {
        GenerateResources();
    }

    private void GenerateResources()
    {
        Dictionary<Vector2Int, ResourcePlace> vecResDict = new Dictionary<Vector2Int, ResourcePlace>();
        foreach (var resource in resourcesToGenerate)
        {
            ResourcePlace resourcePlace = Instantiate(resource).GetComponent<ResourcePlace>();
            Vector2Int coords = new Vector2Int(Random.Range(-maxOffset, maxOffset + 1), Random.Range(-resourcePlace.maxGenerationDepth, -resourcePlace.minGenerationDepth + 1));
            resourcePlace.transform.position = new Vector3(coords.x * BuildGrid.Singleton.cellWidth, coords.y * BuildGrid.Singleton.cellHeight);
            vecResDict.Add(coords, resourcePlace);
        }
        BuildGrid.Singleton.resourcePlaces = vecResDict;
    }
}
