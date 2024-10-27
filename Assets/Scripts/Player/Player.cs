using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform rightHandSlot;
    public ConditionUI condition;
    public BuffUI buff;

    private PlayerMovement movement;

    public ConditionSlot health => condition.health;
    public ConditionSlot stamina => condition.stamina;

    public BuffSlot healthRegenBuff => buff.healthRegenBuff;
    public BuffSlot staminaRegenBuff => buff.staminaRegenBuff;
    public BuffSlot moveSpeedBuff => buff.moveSpeedBuff;

    private void Awake()
    {
        PlayerManager.Instance.SetPlayer(this);
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (buff.healthRegenBuff.isActiveAndEnabled)
            condition.health.Add(healthRegenBuff.ActivatePassiveValue * Time.deltaTime);
        if (buff.staminaRegenBuff.isActiveAndEnabled)
            condition.stamina.Add(stamina.passiveValue + staminaRegenBuff.ActivatePassiveValue * Time.deltaTime);
        else
            condition.stamina.Add(stamina.passiveValue * Time.deltaTime);
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

    public void Buff(ConsumableType type, float duration)
    {
        switch (type)
        {
            case ConsumableType.HealthRegen:
                healthRegenBuff.Buff(duration);
                break;
            case ConsumableType.StaminaRegen:
                staminaRegenBuff.Buff(duration);
                break;
            case ConsumableType.MoveSpeed:
                moveSpeedBuff.Buff(duration);
                break;
        }

    }

    public void SuperJump()
    {
        movement.SuperJump();
    }
}
