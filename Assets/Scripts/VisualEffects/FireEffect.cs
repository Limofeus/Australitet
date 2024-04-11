using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace VisualEffects
{
    public class FireEffect : MonoBehaviour
    {
        [SerializeField] private Light2D _light;
        [SerializeField] private float _currentIntensity = 5;
        [SerializeField] private float _speed = 1;
        [SerializeField] private float _lightRange = 1;

        private void Update()
        {
            _light.pointLightInnerRadius = Mathf.Lerp(_light.pointLightInnerRadius, GetRandonOffset(), Time.deltaTime * _speed);
        }

        private float GetRandonOffset()
        {
            return Random.value * _lightRange;
        }
    }
}