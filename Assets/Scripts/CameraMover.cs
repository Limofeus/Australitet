using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace MainSceen
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _minPositionZ;
        [SerializeField] private float _maxPositionZ;
        [SerializeField] private float _scrollSpeed;
        private Vector2 _mousePositionDelta;
        private float _cameraSensitivity = 0.01f;
        private Vector2 _mapSize = new Vector2(15f, 13f);
        private float _currentZoom = 0;

        private void Update()
        {
            Move();
            var scrollDelta = Input.mouseScrollDelta.y;
            if (scrollDelta != 0)
                Zoom(scrollDelta);

        }

        private void Move()
        {
            if (Input.GetMouseButtonDown(2))
            {
                _mousePositionDelta = Input.mousePosition;
            }
            if (Input.GetMouseButton(2))
            {
                Debug.Log(GetSensitivityMultiply());
                Vector2 newMousePos = Input.mousePosition;
                var delta = _mousePositionDelta - newMousePos;
                var newPosition = transform.position + new Vector3(delta.x, delta.y) 
                    * _cameraSensitivity * GetSensitivityMultiply();
                if (Mathf.Abs(newPosition.x) > _mapSize.x)
                    newPosition.x = transform.position.x;
                if (Mathf.Abs(newPosition.y) > _mapSize.y)
                    newPosition.y = transform.position.y;
                _mousePositionDelta = newMousePos;
                transform.position = newPosition;
            }
        }

        private void Zoom(float scrollDelta)
        {
            _currentZoom += scrollDelta * _scrollSpeed;
            _currentZoom = Mathf.Clamp01(_currentZoom);
            var zoom = _minPositionZ + (_maxPositionZ - _minPositionZ) * _currentZoom;
            transform.position = new Vector3(transform.position.x, transform.position.y, zoom);
        }

        private float GetSensitivityMultiply()
        {
            return 1.2f - _currentZoom;
        }
    }

}
