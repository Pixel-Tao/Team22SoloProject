using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public InteractData interactData;

    public abstract string GetInteractText();
    public abstract void OnInteract();
}
