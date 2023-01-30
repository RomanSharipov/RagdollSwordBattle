using System;
using UnityEngine;

namespace UILookAt
{
    public class UILookAtCamera : MonoBehaviour
    {
        private Camera _camera;

        private void OnValidate()
        {
            if (Camera.main == null)
                return;

            transform.LookAt(Camera.main.transform);
        }

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            transform.LookAt(_camera.transform);
        }
    }
}