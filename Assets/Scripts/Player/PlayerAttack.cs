using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask layerMask;

    private PlayerAnim anim;
    private PlayerCondition player;

    private bool isAttacking = false;
    private float attackTime = 0f;

    private void Awake()
    {
        anim = GetComponent<PlayerAnim>();
        player = GetComponent<PlayerCondition>();
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

        Collider[] objectsInRadius = Physics.OverlapSphere(transform.position + Vector3.up, player.equip.attackDistance, layerMask);

        foreach (Collider obj in objectsInRadius)
        {
            if (obj.gameObject == gameObject) continue;

            Vector3 directionToTarget = (obj.transform.position - transform.position).normalized;
            float angleToTarget = Vector2.Angle(transform.forward, directionToTarget);
            Debug.Log($"{angleToTarget}, {player.equip.attackAngle}");
            if (angleToTarget <= player.equip.attackAngle)
            {
                if (obj.TryGetComponent(out DestructibleObject destructible))
                {
                    destructible.TakeDamage(player.equip.damage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(player != null && player.equip != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + Vector3.up * 1.5f, player.equip.attackDistance);
        }
    }
}
