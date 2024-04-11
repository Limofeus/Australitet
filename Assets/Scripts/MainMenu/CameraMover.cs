using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private Vector2 _offset;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            _camera.transform.position = GetMousePositionOnScreen() * _offset;
        }

        private Vector2 GetMousePositionOnScreen()
        {
            var mousePosInPixels = Input.mousePosition;
            var mousePositionX = mousePosInPixels.x / Screen.width;
            var mousePositionY = mousePosInPixels.y / Screen.height;
            return new Vector2(mousePositionX - 0.5f, mousePositionY - 0.5f) * 2;
        }
    }

}