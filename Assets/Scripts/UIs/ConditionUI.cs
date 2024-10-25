using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    public ConditionSlot health;
    public ConditionSlot stamina;

    private void Start()
    {
        PlayerManager.Instance.Player.condition = this;
    }
 }
