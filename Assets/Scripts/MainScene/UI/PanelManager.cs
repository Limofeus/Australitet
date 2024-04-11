using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Singleton;

    public List<Transform> worldTransforms;
    public List<RectTransform> uiTransforms;

    private void Awake()
    {
        Singleton = this;
    }

    private void Update()
    {
        for(int i = 0; i < worldTransforms.Count; i++)
        {
            uiTransforms[i].position = WorldToScreenSpace(worldTransforms[i].position, Camera.main);
        }
    }
    public GameObject CreateUiPanel(GameObject panel)
    {
        return Instantiate(panel, transform);
    }
    public void AddTrackingPair(Transform worldTransform, RectTransform uiTransform)
    {
        worldTransforms.Add(worldTransform);
        uiTransforms.Add(uiTransform);
        uiTransform.position = WorldToScreenSpace(worldTransform.position, Camera.main);
    }
    public void RemoveTrackingPair(Transform worldTransform)
    {
        for(int i = 0; i < worldTransforms.Count; i++)
        {
            if(worldTransform == worldTransforms[i])
            {
                worldTransforms.RemoveAt(i);
                Destroy(uiTransforms[i].gameObject);
                uiTransforms.RemoveAt(i);
                break;
            }
        }
    }
    public static Vector3 WorldToScreenSpace(Vector3 worldPos, Camera cam)
    {
        Vector3 screenPoint = cam.WorldToScreenPoint(worldPos);
        screenPoint.z = 0;

        return screenPoint;
    }
}
