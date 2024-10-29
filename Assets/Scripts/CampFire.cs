using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);
}

public class CampFire : MonoBehaviour
{
    public int damage;
    public float damageInterval;
    private float time;

    WaitForSeconds waitDamageInterval;

    private List<IDamageable> things = new List<IDamageable>();

    private void Start()
    {
        waitDamageInterval = new WaitForSeconds(damageInterval);
        StartCoroutine(CoDamageable());
    }

    IEnumerator CoDamageable()
    {
        while(true)
        {
            DealDamage();
            yield return waitDamageInterval;
        }
    }

    private void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakeDamage(damage);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            things.Add(damageable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            things.Remove(damageable);
        }
    }
}
