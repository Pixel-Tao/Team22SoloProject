using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0, 10)]
    public float walkSpeed = 5f;
    [Range(0, 20)]
    public float runSpeed = 10f;
    [Range(0, 1000)]
    public float jumpPower = 20f;
    public LayerMask groundLayerMask;
    public bool isRun;
    public float fallMultiplier;
    public float maxSlopeLimit;
    public int jumpStaminaCost;

    public bool isLaunching;

    public float currentSpeed => (isRun ? runSpeed : walkSpeed) + player.moveSpeedBuff.ActivatePassiveValue;

    private Rigidbody rigidbody;
    private PlayerAnim anim;
    private PlayerLook look;
    private Vector3 direction;
    private Player player;
    private PlayerHang hang;
    private RaycastHit slopeHit;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<PlayerAnim>();
        look = GetComponent<PlayerLook>();
        player = GetComponent<Player>();
        hang = GetComponent<PlayerHang>();
    }

    private void Update()
    {
        if (direction != Vector3.zero && !hang.isHang)
        {
            look.Rotate();
        }
    }

    private void FixedUpdate()
    {
        if (hang.isHang) return;
        if (isLaunching)
        {
            if (IsGrounded())
            {
                rigidbody.velocity = Vector3.zero;
                isLaunching = false;
            }
        }
        else
        {
            GroundMovement();
        }
    }

    public void Move(Vector2 dir)
    {
        if (hang.isHang) hang.WallClimb(dir);
        direction = dir.normalized;
    }

    private void GroundMovement()
    {
        bool isGrounded = IsGrounded();
        bool isOnSlope = IsOnSlope();
        Vector3 velocity = transform.forward * direction.y + transform.right * direction.x;
        Vector3 gravity = Vector3.zero;
        if (isOnSlope && isGrounded)
        {
            velocity = Vector3.ProjectOnPlane(velocity, slopeHit.normal).normalized;
            rigidbody.useGravity = false;
            velocity = velocity * currentSpeed;
            velocity += gravity;
        }
        else
        {
            rigidbody.useGravity = true;

            velocity = velocity * currentSpeed;
            if (direction.y < 0)
                velocity = velocity * 0.5f;

            if (!isGrounded)
                gravity = Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;

            velocity.y = rigidbody.velocity.y;
            velocity += gravity;
        }

        rigidbody.velocity = velocity;
        anim.Move(direction);
        anim.Ground(isGrounded);
    }

    public void RunToggle()
    {
        isRun = !isRun;
        anim.Run(isRun);
    }

    public void Jump()
    {
        if (hang.isHang)
        {
            hang.Jump();
            return;
        }

        if (IsGrounded() == false)
            return;

        if (player.stamina.currentValue < jumpStaminaCost)
            return;

        anim.Jump();
        AddForce();
        player.UseStamina(jumpStaminaCost);
    }

    public void SuperJump()
    {
        if (IsGrounded() == false)
            return;

        anim.Jump();
        AddForce(2);
        player.UseStamina(jumpStaminaCost * 2);
    }

    public void AddForce(int multiplier = 1)
    {
        rigidbody.AddForce(Vector3.up * jumpPower * multiplier, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        Ray[] ray = new Ray[4]
       {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.1f), Vector3.down),
       };

        for (int i = 0; i < ray.Length; i++)
        {
            if (Physics.Raycast(ray[i], 0.15f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsOnSlope()
    {
        Ray ray = new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.1f), Vector3.down);
        if (Physics.Raycast(ray, out slopeHit, 0.15f, groundLayerMask))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle != 0f && angle < maxSlopeLimit;
        }

        return false;
    }
}
