using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnabler : MonoBehaviour
{
    [SerializeField] private GameObject[] enaVariants;

    private void Start()
    {
        enaVariants[Random.Range(0, enaVariants.Length)].SetActive(true);
    }

}
