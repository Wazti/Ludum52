using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using PixelPerfectCamera = UnityEngine.Experimental.Rendering.Universal.PixelPerfectCamera;

public class CanvasRescale : MonoBehaviour
{
    [SerializeField] private PixelPerfectCamera _camera;
    [SerializeField] private CanvasScaler _scaler;
    private void Awake()
    {
        _scaler.scaleFactor = _camera.pixelRatio;
    }
}
