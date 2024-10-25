using UnityEngine;

public class ButtonObject : InteractableObject
{
    public override string GetInteractText()
    {
        return $"{interactData.title}\n{interactData.description}";
    }

    public override void OnInteract()
    {

    }
}