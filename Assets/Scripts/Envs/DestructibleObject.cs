using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour, IDamageable
{
    [Range(1, 999)] public int health;
    public float flashTime;
    public Color flashColor;

    private Color originColor;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
            meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        originColor = meshRenderer.material.color;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(Flash());
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Flash()
    {
        foreach (var material in meshRenderer.materials)
        {
            material.color = flashColor;
        }
        yield return new WaitForSeconds(flashTime);
        foreach (var material in meshRenderer.materials)
        {
            material.color = originColor;
        }
    }
}
