using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace VisualEffects
{
    public class RGBMashroomEffect : MonoBehaviour
    {
        [SerializeField] private Light2D _light;
        [SerializeField] private float _speed = 1;
        [SerializeField] private float _offset = 1;

        private void Update()
        {
            var value = (Mathf.Sin(_speed * Time.time + _offset) * 0.15f + 0.6f) % 1f;          
            _light.color = Color.HSVToRGB(value, 1f, 1f);
        }
    }

}