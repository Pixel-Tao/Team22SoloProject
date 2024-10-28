using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILeverTarget
{
    public void LeverOn();
    public void LeverOff();
}

public class LeverHandler : MonoBehaviour
{
    private ILeverTarget leverTarget;
    public LeverObject lever;

    private void Awake()
    {
        leverTarget = GetComponent<ILeverTarget>();
    }

    public void On()
    {
        leverTarget?.LeverOn();
    }

    public void Off()
    {
        leverTarget?.LeverOff();
    }
}
