
using UnityEngine;

public class HealPackObject : InteractableObject
{
    private void Update()
    {
        Rotate();
    }

    public override string GetInteractText()
    {
        return $"{interactData.title}";
    }

    public override void OnInteract()
    {
        PickupData pickupData = (PickupData)interactData;
        PlayerManager.Instance.Player.Heal(pickupData.amount);
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerCondition player))
        {
            OnInteract();
        }
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }
}