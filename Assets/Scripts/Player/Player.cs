using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    [Range(0, 500)] public int maxHealth;
    public Transform rightHandSlot;

    public event Action<int> HealthChangedEvent;

    private PlayerMovement movement;

    private void Awake()
    {
        PlayerManager.Instance.SetPlayer(this);
        movement = GetComponent<PlayerMovement>();
        health = maxHealth / 2;
    }

    public void OnDamaged(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        HealthChangedEvent?.Invoke(health);
    }

    public void OnHeal(int heal)
    {
        health += heal;
        health = Mathf.Clamp(health, 0, maxHealth);
        HealthChangedEvent?.Invoke(health);
    }

    public void SuperJump()
    {
        movement.SuperJump();
    }
}
