using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health;
    [Range(0, 500)] public int maxHealth;

    public event Action<int> HealthChangedEvent;

    private PlayerMovement movement;

    private void Awake()
    {
        PlayerManager.Instance.SetPlayer(this);
        movement = GetComponent<PlayerMovement>();  
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void OnDamaged(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        HealthChangedEvent?.Invoke(health);
    }

    public void SuperJump()
    {
        movement.SuperJump();
    }
}
