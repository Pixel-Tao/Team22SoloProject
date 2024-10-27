using UnityEngine;

public class ItemObject : InteractableObject
{
    Rigidbody rigidbody;
    public LayerMask layerMask;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public override string GetInteractText()
    {
        return $"{interactData.title}\n{interactData.description}";
    }

    private void Update()
    {
        CheckDrop();
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, 100 * Time.deltaTime);
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

    public override void OnInteract()
    {
        ItemData itemData =(ItemData)interactData;
        PlayerManager.Instance.Player.inventory.AddItem(itemData);
        Destroy(gameObject);
    }

    public void Shoot(Vector3 dir)
    {
        rigidbody.AddForce(dir, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * 1f);
    }
}
