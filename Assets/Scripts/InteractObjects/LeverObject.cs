using System.Collections.Generic;
using UnityEngine;

public class LeverObject : InteractableObject
{
    public List<LeverHandler> handler;
    public bool isOn;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if (handler != null)
        {
            foreach (var h in handler)
            {
                h.lever = this;
            }
        }
        if (isOn)
        {
            LeverOn();
        }
        else
        {
            LeverOff();
        }
        animator.SetBool("IsOn", isOn);
    }

    private void LeverOn()
    {
        foreach (var h in handler)
        {
            h.On();
        }
    }

    private void LeverOff()
    {
        foreach (var h in handler)
        {
            h.Off();
        }
    }

    public override string GetInteractText()
    {
        return $"{interactData.title} {(isOn ? "끄기" : "켜기")}\n{interactData.description}";
    }

    public override void OnInteract()
    {
        if (isOn)
        {
            animator.SetBool("IsOn", false);
            LeverOff();
            isOn = false;
        }
        else
        {
            animator.SetBool("IsOn", true);
            LeverOn();
            isOn = true;
        }
    }
}