using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    public Image healthImage;

    private void Start()
    {
        PlayerManager.Instance.Player.HealthChangedEvent += HealthChanged;
    }

    private void HealthChanged(int value)
    {
        int maxHealth = PlayerManager.Instance.Player.maxHealth;

        healthImage.fillAmount = (float)value / maxHealth;
    }
}
