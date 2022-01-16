using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolBox.Pattern;


namespace Chanhyeong
{
    public class InputManager : Singleton<InputManager>
    {
        public static Vector2 MousePosition =>
#if UNITY_EDITOR
            Input.mousePosition;
#elif UNITY_ANDROID || UNITY_IOS
            Input.GetTouch(0).position;  
#endif

        public static Vector3 MouseWorldPosition => Instance._mainCamera.ScreenToWorldPoint(MousePosition);

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }
    }
}
