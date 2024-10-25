using UnityEngine;

public class CoinObject : InteractableObject
{
    Rigidbody rigidbody;
    public LayerMask layerMask;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckDrop();
        Rotate();
    }

    private void CheckDrop()
    {
        if (!rigidbody.isKinematic && rigidbody.velocity.y < 0)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, 1f, layerMask))
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.isKinematic = true;
            }
            else
            {
                rigidbody.isKinematic = false;
            }
        }
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }

    public override string GetInteractText()
    {
        return $"{interactData.title}\n{interactData.description}";
    }

    public override void OnInteract()
    {
        PickupData resourceData = (PickupData)interactData;
        PlayerManager.Instance.AddCoin(resourceData.amount);
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            OnInteract();
        }
    }

    public void Shoot(Vector3 dir)
    {
        rigidbody.AddForce(dir, ForceMode.Impulse);
    }
}