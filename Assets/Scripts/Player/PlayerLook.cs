using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerLook : MonoBehaviour
{
    [Header("캐릭터 회전 속도")][Range(0, 3)]
    public float rotationSmoothTime = 0.1f;

    private Camera camera; 
    private float rotationVelocity = 0;

    public GameObject playerCameraRoot;

    private void Awake()
    {
        camera = Camera.main;
    }

    public void Rotate()
    {
        float targetRotation = camera.transform.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
