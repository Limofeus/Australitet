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
        public float _cameraSensitivity = 0.01f;
        public Vector2 _mapSize = new Vector2(15f, 13f);
        public float _mapOffsVert = 8f;
        public static float _currentZoom = 0;

        private void Start()
        {
            Zoom(0f);
        }
        private void Update()
        {
            Move();
            var scrollDelta = Input.mouseScrollDelta.y;
            if (scrollDelta != 0)
                Zoom(scrollDelta);

        }

        private void Move()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _mousePositionDelta = Input.mousePosition;
            }
            if (Input.GetMouseButton(1))
            {
                Debug.Log(GetSensitivityMultiply());
                Vector2 newMousePos = Input.mousePosition;
                var delta = _mousePositionDelta - newMousePos;
                var newPosition = transform.position + new Vector3(delta.x, delta.y) 
                    * _cameraSensitivity * GetSensitivityMultiply();
                if (Mathf.Abs(newPosition.x) > _mapSize.x)
                    newPosition.x = transform.position.x;
                if (Mathf.Abs(newPosition.y - _mapOffsVert) > _mapSize.y)
                    newPosition.y = transform.position.y;
                _mousePositionDelta = newMousePos;
                transform.position = newPosition;
            }
        }

        private void Zoom(float scrollDelta)
        {
            _currentZoom += scrollDelta * _scrollSpeed;
            _currentZoom = Mathf.Clamp01(_currentZoom);
            var zoom = Mathf.Lerp(_minPositionZ, _maxPositionZ, _currentZoom);
            transform.position = new Vector3(transform.position.x, transform.position.y, zoom);
        }

        private float GetSensitivityMultiply()
        {
            return 1.2f - _currentZoom;
        }
    }

}
