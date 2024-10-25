using UnityEngine;

public class ItemObject : InteractableObject
{
    public override string GetInteractText()
    {
        return $"{interactData.title}\n{interactData.description}";
    }

    public override void OnInteract()
    {

    }

    public void Shoot(Vector3 dir)
    {

    }
}