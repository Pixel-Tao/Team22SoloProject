using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask layerMask;

    private PlayerAnim anim;
    private Player player;

    private bool isAttacking = false;
    private float attackTime = 0f;

    private void Awake()
    {
        anim = GetComponent<PlayerAnim>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (isAttacking)
        {
            if (Time.time - attackTime > player.equip.attackRate)
            {
                isAttacking = false;
            }
        }
    }

    public void Attack()
    {
        if (player.equip == null) return;
        if (!player.CanUseStamina(player.equip.staminaCost)) return;
        if (isAttacking) return;
        isAttacking = true;
        attackTime = Time.time;

        anim.Attack();
    }

    public void OnAttacked()
    {
        player.UseStamina(player.equip.staminaCost);
        // TODO : 공격 성공 여부
    }
}
