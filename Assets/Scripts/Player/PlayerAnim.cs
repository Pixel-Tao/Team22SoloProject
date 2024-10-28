using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement;
    private PlayerAttack attack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
    }

    public void Move(Vector2 moveVector)
    {
        animator.SetFloat("MoveSpeed", moveVector.y);
        animator.SetFloat("MoveDirection", moveVector.x);
    }

    public void Run(bool isRun)
    {
        animator.SetBool("IsRun", isRun);
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Interact()
    {
        animator.SetTrigger("Interact");
    }

    public void OnAttackEvent()
    {
        attack.OnAttacked();
    }

    public void Ground(bool isGrounded)
    {
        if (animator.GetBool("IsGrounded") == isGrounded)
            return;

        animator.SetBool("IsGrounded", isGrounded);
    }
}
