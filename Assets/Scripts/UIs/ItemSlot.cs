using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public InventoryUI inventory;

    public ItemData itemData;
    public Transform itemSlot;
    public Image icon;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI equipMarkText;

    public int itemCount;

    public void ItemSlotClick()
    {
        inventory.SelectItem(this);
    }

    public void Select()
    {
        Outline outline = itemSlot.GetComponent<Outline>();
        if (outline != null)
            outline.enabled = true;
    }

    public void UnSelect()
    {
        Outline outline = itemSlot.GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;
    }

    public void AddItem(ItemData itemData)
    {
        if (this.itemData != itemData)
        {
            this.itemData = itemData;
            itemCount = 0;
            countText.gameObject.SetActive(itemData.stackable);
            icon.gameObject.SetActive(true);
            icon.sprite = itemData.icon;
        }
        if (itemData.stackable)
        {
            itemCount++;
            countText.text = itemCount.ToString();
        }
        else
        {
            itemCount = 1;
            countText.text = "";
        }
    }

    public void Equip()
    {
        if (itemData.itemType != ItemType.Equipable) return;

        PlayerManager.Instance.Player.Equip(itemData);
        equipMarkText.gameObject.SetActive(true);
    }

    public void UnEquip()
    {
        if (itemData.itemType != ItemType.Equipable) return;

        PlayerManager.Instance.Player.UnEquip();
        equipMarkText.gameObject.SetActive(false);
    }

    public void Use()
    {
        if (itemData.itemType != ItemType.Consumable) return;

        foreach (Consumable consumable in itemData.consumables)
        {
            switch (consumable.consumableType)
            {
                case ConsumableType.Health:
                    PlayerManager.Instance.Player.health.Add(consumable.amount);
                    break;
                case ConsumableType.Stamina:
                    PlayerManager.Instance.Player.stamina.Add(consumable.amount);
                    break;
                case ConsumableType.MoveSpeed:
                case ConsumableType.HealthRegen:
                case ConsumableType.StaminaRegen:
                    PlayerManager.Instance.Player.Buff(consumable.consumableType, consumable.amount);
                    break;
                case ConsumableType.Scroll:
                    PlayerManager.Instance.Player.GameEnd();
                    break;
            }
        }

        itemCount--;
        countText.text = itemCount.ToString();
        if (itemCount <= 0)
        {
            Clear();
            inventory.UnSelectItem();
        }
    }

    public void Drop()
    {
        if (itemData == null) return;

        itemCount--;
        countText.text = itemCount.ToString();

        PlayerManager.Instance.Player.DropItem(itemData);
        if (itemCount <= 0)
        {
            Clear();
            inventory.UnSelectItem();
        }
    }

    public void Clear()
    {
        itemData = null;
        icon.sprite = null;
        countText.text = "";
        icon.gameObject.SetActive(false);
        countText.gameObject.SetActive(false);
        equipMarkText.gameObject.SetActive(false);
    }
}
