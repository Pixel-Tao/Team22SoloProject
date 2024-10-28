using UnityEngine;

public class LeverObject : InteractableObject
{
    public LeverHandler handler;
    public bool isOn;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if (handler != null)
            handler.lever = this;

        if (isOn)
        {
            handler?.On();
        }
        else
        {
            handler?.Off();
        }
        animator.SetBool("IsOn", isOn);
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
            handler?.Off();
            isOn = false;
        }
        else
        {
            animator.SetBool("IsOn", true);
            handler?.On();
            isOn = true;
        }
    }
}