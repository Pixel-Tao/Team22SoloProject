using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxObject : InteractableObject
{
    private Animator animator;
    private BoxCollider boxCollider;
    public Transform boxBase;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public override string GetInteractText()
    {
        return $"{interactData.title}\n{interactData.description}";
    }

    public override void OnInteract()
    {
        Debug.Log("BoxObject OnInteract");
        animator.SetBool("IsOpen", true);
        boxCollider.enabled = false;
        DropItem();
    }

    private void DropItem()
    {
        BoxData boxData = interactData as BoxData;
        if (boxData == null) return;

        foreach (InteractData item in boxData.items)
        {
            GameObject itemObject = Instantiate(item.interactPrefab, boxBase.position, Quaternion.identity);
            Vector3 randDirection = new Vector3(Random.Range(-1f, 1f), 8, Random.Range(-1f, 1f));
            if (item.interactType == InteractType.Pickup)
                itemObject.GetComponent<CoinObject>().Shoot(randDirection);
            else if(item.interactType == InteractType.Item)
                itemObject.GetComponent<ItemObject>().Shoot(randDirection);
        }
    }
}
