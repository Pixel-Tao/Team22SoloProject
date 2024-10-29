using Cinemachine;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("캐릭터 회전 속도")][Range(0, 3)]
    public float rotationSmoothTime = 0.1f;

    private Camera camera; 
    private float rotationVelocity = 0;
    private CinemachineFreeLook freeLook;

    public GameObject playerCameraRoot;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Start()
    {
        freeLook = FindObjectOfType<CinemachineFreeLook>();
        if (freeLook == null)
        {
            GameObject freeLookGo = new GameObject("FreeLook");
            freeLook = freeLookGo.AddComponent<CinemachineFreeLook>();
        }

        freeLook.Follow = transform;
        freeLook.LookAt = playerCameraRoot.transform;
        freeLook.m_XAxis.m_InvertInput = false;
        freeLook.m_YAxis.m_InvertInput = true;
        freeLook.m_YAxis.m_MaxSpeed = 5f;
        freeLook.m_Orbits[0].m_Radius = 3f;
        freeLook.m_Orbits[0].m_Height = 8f;
        freeLook.m_Orbits[1].m_Radius = 7f;
        freeLook.m_Orbits[1].m_Height = 4f;
        freeLook.m_Orbits[2].m_Radius = 4f;
        freeLook.m_Orbits[2].m_Height = 0.4f;

        freeLook.transform.position = new Vector3(0, 4, -7);

        CinemachineBrain brain = camera.GetComponent<CinemachineBrain>();
        if (brain == null)
        {
            brain = camera.gameObject.AddComponent<CinemachineBrain>();
        }
    }

    public void Rotate()
    {
        float targetRotation = camera.transform.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
