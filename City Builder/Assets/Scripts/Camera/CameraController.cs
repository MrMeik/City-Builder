using Assets.Scripts.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Serializable]
    public struct MouseMovementConfig
    {
        [Range(0.05f, 0.35f)]
        public float panBorderSize;

        [Range(0.05f, 1f)]
        public float startingPanSpeedOffset;
    }

    [Serializable]
    public class OrthoController
    {
    }

    [Serializable]
    public class PerspController
    {
    }

    public enum RenderMode
    {
        Orthographic,
        TightPerspective
    }

    public float MaxPanSpeed;

    public MouseMovementConfig mouseMovementConfig;
    public RenderMode mode;
    public OrthoController orthoController;
    public PerspController perspController;

    // Start is called before the first frame update
    void Start()
    {
        switch (mode)
        {
            case RenderMode.Orthographic:
                SetupOrthographic();
                break;
            case RenderMode.TightPerspective:
                SetupTightPerspective();
                break;
        }
    }

    private void SetupOrthographic()
    {
        //throw new NotImplementedException();
    }

    private void SetupTightPerspective()
    {
        //throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        PollInputs();
    }

    private void PollInputs()
    {
        Vector2 delta = GetTranslation();
        Debug.Log(delta);
    }

    private Vector2 GetTranslation()
    {
        Vector2 delta = Vector2.zero;
        //Priority is given to last function
        GetMouseCameraTranslation(ref delta);
        //GetKeyboardCameraTranslation(ref delta);
        //GetControllerCameraTranslation(ref delta);
        return delta;
    }

    private void GetKeyboardCameraTranslation(ref Vector2 delta)
    {
        
    }

    private void GetControllerCameraTranslation(ref Vector2 delta)
    {
        
    }

    private void GetMouseCameraTranslation(ref Vector2 delta)
    {
        var width = Screen.width;
        var height = Screen.height;
        var borderWidth = width * mouseMovementConfig.panBorderSize;
        var borderHeight = height * mouseMovementConfig.panBorderSize;

        Vector2 mousePos = Input.mousePosition;

        Vector2 lowerLeft = new Vector2(borderWidth, borderHeight);
        Vector2 upperRight = new Vector2(width - borderWidth, height - borderHeight);

        Vector2 clamped = V2Plus.ClampToBox(mousePos, lowerLeft, upperRight);
        if (clamped != mousePos)
        {
            var actualDelta = mousePos - clamped;
            var scaledDelta = new Vector2(actualDelta.x / borderWidth, actualDelta.y / borderWidth);

            var xSpeed = Mathf.Lerp(MaxPanSpeed * mouseMovementConfig.startingPanSpeedOffset, MaxPanSpeed, Mathf.Abs(scaledDelta.x));
            var ySpeed = Mathf.Lerp(MaxPanSpeed * mouseMovementConfig.startingPanSpeedOffset, MaxPanSpeed, Mathf.Abs(scaledDelta.y));

            delta = new Vector2(Mathf.Sign(scaledDelta.x) * xSpeed, Mathf.Sign(scaledDelta.y) * ySpeed);
        }
    }
}


