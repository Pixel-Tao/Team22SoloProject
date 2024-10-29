using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBoxObject : MonoBehaviour, ILeverTarget
{
    public void LeverOff()
    {
        gameObject.SetActive(false);
    }

    public void LeverOn()
    {
        gameObject.SetActive(true);
    }
}
