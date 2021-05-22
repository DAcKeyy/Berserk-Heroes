using System;
using UnityEngine;

public class Canvas_SetMainCamera : MonoBehaviour
{
    [SerializeField]private Canvas _canvas;
    private void Reset()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
    }

    public void OnEnable()
    {
        _canvas.worldCamera = Camera.main;
        _canvas.overrideSorting = true;
    }
}
