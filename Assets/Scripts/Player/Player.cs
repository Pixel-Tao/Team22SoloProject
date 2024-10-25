using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health;
    [Range(0, 500)] public int maxHealth;

    public event Action<int> HealthChangedEvent;

    private void Awake()
    {
        PlayerManager.Instance.SetPlayer(this);
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
}
