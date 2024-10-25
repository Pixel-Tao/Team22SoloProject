using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform rightHandSlot;
    public ConditionUI condition;

    private PlayerMovement movement;

    public ConditionSlot health => condition.health;
    public ConditionSlot stamina => condition.stamina;

    private void Awake()
    {
        PlayerManager.Instance.SetPlayer(this);
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        condition.stamina.Add(condition.stamina.passiveValue * Time.deltaTime);
    }

    public void Damaged(int damage)
    {
        condition.health.Subtract(damage);
    }

    public void Heal(int heal)
    {
        condition.health.Add(heal);
    }

    public void UseStamina(int cost)
    {
        condition.stamina.Subtract(cost);
    }

    public void SuperJump()
    {
        movement.SuperJump();
    }
}
