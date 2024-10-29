using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class JumpPanel : MonoBehaviour
{
    private bool isUsing;
    private PlayerCondition player;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (isUsing) return;
        if (collision.gameObject.TryGetComponent(out player))
        {
            animator.Play("Jump");
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        isUsing = false;
        player = null;
    }

    public void OnJumpStart()
    {
        if (player == null) return;
        isUsing = true;
        player.SuperJump();
    }
}
