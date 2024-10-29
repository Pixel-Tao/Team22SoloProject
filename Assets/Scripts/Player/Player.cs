using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public GameObject generalUIPrefab;
    public GameObject inventoryUIPrefab;

    public Transform rightHandSlot;
    public ConditionUI condition;
    public BuffUI buff;
    public InventoryUI inventory;
    public WarningUI warning;
    public DamageIndicatorUI damageIndicator;
    public Equip equip;

    private PlayerMovement movement;

    public ConditionSlot health => condition.health;
    public ConditionSlot stamina => condition.stamina;

    public BuffSlot healthRegenBuff => buff.healthRegenBuff;
    public BuffSlot staminaRegenBuff => buff.staminaRegenBuff;
    public BuffSlot moveSpeedBuff => buff.moveSpeedBuff;

    private void Awake()
    {
        PlayerManager.Instance.SetPlayer(this);
        movement = GetComponent<PlayerMovement>();

        Instantiate(generalUIPrefab);
        Instantiate(inventoryUIPrefab);
    }

    private void Update()
    {
        if (buff.healthRegenBuff.isActiveAndEnabled)
            condition.health.Add(healthRegenBuff.ActivatePassiveValue * Time.deltaTime);
        if (buff.staminaRegenBuff.isActiveAndEnabled)
            condition.stamina.Add((stamina.passiveValue + staminaRegenBuff.ActivatePassiveValue) * Time.deltaTime);
        else
            condition.stamina.Add(stamina.passiveValue * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        condition.health.Subtract(damage);
        damageIndicator.Flash();

        if (condition.health.currentValue <= 0)
            Die();
    }

    public void Heal(int heal)
    {
        condition.health.Add(heal);
    }

    public void UseStamina(int cost)
    {
        condition.stamina.Subtract(cost);
    }

    public bool CanUseStamina(int cost)
    {
        return condition.stamina.currentValue >= cost;
    }

    public void Buff(ConsumableType type, float duration)
    {
        switch (type)
        {
            case ConsumableType.HealthRegen:
                healthRegenBuff.Buff(duration);
                break;
            case ConsumableType.StaminaRegen:
                staminaRegenBuff.Buff(duration);
                break;
            case ConsumableType.MoveSpeed:
                moveSpeedBuff.Buff(duration);
                break;
        }

    }

    public void Die()
    {
        transform.position = Vector3.zero;
        condition.health.currentValue = condition.health.maxValue;
        condition.stamina.currentValue = condition.stamina.maxValue;
    }

    public void SuperJump()
    {
        movement.SuperJump();
    }

    public void Equip(ItemData itemData)
    {
        GameObject go = Instantiate(itemData.equipPrefab, rightHandSlot);
        go.transform.localPosition = Vector3.zero;
        equip = go.GetComponent<Equip>();
    }

    public void UnEquip()
    {
        equip = null;
        foreach (Transform weapon in rightHandSlot)
        {
            Destroy(weapon.gameObject);
        }

    }

    public void DropItem(ItemData itemData)
    {
        Vector3 position = transform.position;

        GameObject go = Instantiate(itemData.interactPrefab, position, Quaternion.identity);
        Rigidbody rigidbody = go.GetComponent<Rigidbody>();
        Vector3 randDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), 8, UnityEngine.Random.Range(-1f, 1f));
        rigidbody.AddForce(randDirection, ForceMode.Impulse);
    }

}
