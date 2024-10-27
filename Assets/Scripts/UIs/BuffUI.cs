using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUI : MonoBehaviour
{
    public BuffSlot healthRegenBuff;
    public BuffSlot staminaRegenBuff;
    public BuffSlot moveSpeedBuff;

    private void Start()
    {
        PlayerManager.Instance.Player.buff = this;
    }
}
