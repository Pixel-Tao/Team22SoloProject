using Cinemachine;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("캐릭터 회전 속도")]
    [Range(0, 3)]
    public float rotationSmoothTime = 0.1f;
    public float lookSensitivity;
    public float maxXLook;
    public float minXLook;

    public float currentZoomValue = -10;

    private Camera camera;
    private float rotationVelocity = 0;
    private CinemachineFreeLook freeLook;
    private float camCurXRot;


    public GameObject playerCameraRoot;
    private Vector2 mouseDelta;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Start()
    {
        camera.transform.SetParent(playerCameraRoot.transform);
        camera.transform.localPosition = new Vector3(0, 0, currentZoomValue);
    }

    private void LateUpdate()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        playerCameraRoot.transform.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void Look(Vector2 mouseDelta)
    {
        this.mouseDelta = mouseDelta;
    }

    public void Rotate()
    {
        float targetRotation = camera.transform.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
