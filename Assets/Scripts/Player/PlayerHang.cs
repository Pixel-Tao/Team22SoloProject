using UnityEngine;

public class PlayerHang : MonoBehaviour
{
    public float distance;
    public LayerMask layerMask;
    public float checkRate;
    public GameObject currentGameObject;
    public bool isHang;
    private float lastCheckTime;

    public float wallStickyForce;
    public float wallClimbForce;
    public float wallJumpForce;

    private RaycastHit hit;
    private Rigidbody rigidbody;

    private Vector3 direction;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            CheckHang();
            lastCheckTime = Time.time;
        }
    }

    private void FixedUpdate()
    {
        if (isHang)
        {
            Vector3 velocity = transform.up * direction.y + transform.right * direction.x;
            rigidbody.velocity = velocity * wallClimbForce;
        }
    }

    private void CheckHang()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.2f, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask))
        {
            if (hit.collider.gameObject != currentGameObject)
            {
                currentGameObject = hit.collider.gameObject;
                isHang = true;
                StartWallHang();
            }
        }
        else
        {
            if (currentGameObject != null)
            {
                currentGameObject = null;
                isHang = false;
                StopWallHang();
            }
        }
    }

    private void StartWallHang()
    {
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;

        rigidbody.AddForce(-hit.normal * wallStickyForce, ForceMode.Impulse);
        Debug.Log("벽에 매달림");
    }

    private void StopWallHang()
    {
        rigidbody.useGravity = true;
    }

    public void WallClimb(Vector3 direction)
    {
        this.direction = direction;

        Debug.Log("벽타고 이동");
    }

    public void Jump()
    {
        if (isHang)
        {
            rigidbody.useGravity = true;
            Vector3 dir = (-transform.forward + transform.up).normalized;
            rigidbody.AddForce(dir * wallJumpForce, ForceMode.Impulse);
            isHang = false;
            Debug.Log($"벽에서 점프 {dir * wallJumpForce}");
        }
    }
}
