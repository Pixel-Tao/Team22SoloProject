using UnityEngine;

public class PlayerHang : MonoBehaviour
{
    public float distance;
    public LayerMask layerMask;
    public float checkRate;
    public GameObject currentGameObject;
    public bool isHang;
    private float lastCheckTime;

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

        Climb();
    }

    private void Climb()
    {
        if (isHang)
        {
            Vector3 movePosition = transform.up * direction.y + transform.right * direction.x;
            rigidbody.velocity = movePosition * wallClimbForce;
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
                StartWallHang();
            }
        }
        else
        {
            if (currentGameObject != null)
            {
                currentGameObject = null;
                StopWallHang();
            }
        }
    }

    private void StartWallHang()
    {
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;
        isHang = true;
    }

    private void StopWallHang()
    {
        rigidbody.useGravity = true;
        isHang = false;
    }

    public void WallClimb(Vector3 direction)
    {
        this.direction = direction;
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
