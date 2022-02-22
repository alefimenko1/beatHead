using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 _prevPosition;
    private Vector2 _delta;

    private bool _isSwipe;

    public Vector2 Delta => _delta;
    public bool IsSwipe => _isSwipe;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _isSwipe = true;
#if UNITY_EDITOR
            _prevPosition = Input.mousePosition;
#else
            _prevPosition = Input.GetTouch(0).position;
#endif
        }
        else if(Input.GetMouseButton(0))
        {
#if UNITY_EDITOR
            var currentPosition = Input.mousePosition;
#else
            var currentPosition = Input.mousePosition;
#endif
            var diff = currentPosition - _prevPosition;
            _delta = new Vector2(diff.x, diff.y);
            _prevPosition = currentPosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            _isSwipe = false;
            _delta = Vector2.zero;
        }
    }
}
