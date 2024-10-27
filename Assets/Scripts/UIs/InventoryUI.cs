using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemSlots;

    public ItemSlot selectedItemSlot;
    public ItemSlot equippedItemSlot;
    public TextMeshProUGUI itemTitleText;
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemStatusNameText;
    public TextMeshProUGUI itemStatusValueText;

    public Button useButton;
    public Button equipButton;
    public Button unEquipButton;
    public Button dropButton;

    public bool isOpen => gameObject.activeInHierarchy;

    private void Start()
    {
        PlayerManager.Instance.Player.inventory = this;
        UnSelectItem();
        Toggle();
    }

    public void SelectItem(ItemSlot itemSlot)
    {
        UnSelectItem();
        if (itemSlot.itemData == null) return;

        selectedItemSlot = itemSlot;
        itemTitleText.text = itemSlot.itemData.title;
        itemDescriptionText.text = itemSlot.itemData.description;
        string statusName = string.Empty;
        string statusValue = string.Empty;
        foreach (Consumable consumable in itemSlot.itemData.consumables)
        {
            statusName += $"{consumable.consumableType}\n";
            switch (consumable.consumableType)
            {
                case ConsumableType.HealthRegen:
                case ConsumableType.StaminaRegen:
                case ConsumableType.MoveSpeed:
                    statusValue += $"{consumable.amount} ÃÊ\n";
                    break;
                case ConsumableType.Health:
                case ConsumableType.Stamina:
                    statusValue += $"{consumable.amount} È¸º¹\n";
                    break;
            }
        }
        itemStatusNameText.text = statusName;
        itemStatusValueText.text = statusValue;

        useButton.gameObject.SetActive(itemSlot.itemData.itemType == ItemType.Consumable);
        equipButton.gameObject.SetActive(itemSlot.itemData.itemType == ItemType.Equipable && equippedItemSlot != selectedItemSlot);
        unEquipButton.gameObject.SetActive(itemSlot.itemData.itemType == ItemType.Equipable && equippedItemSlot == selectedItemSlot);
        dropButton.gameObject.SetActive(selectedItemSlot != null);

        selectedItemSlot.Select();
    }
    public void UnSelectItem()
    {
        if (selectedItemSlot != null) selectedItemSlot.UnSelect();
        selectedItemSlot = null;
        itemTitleText.text = string.Empty;
        itemDescriptionText.text = string.Empty;
        itemStatusNameText.text = string.Empty;
        itemStatusValueText.text = string.Empty;
        useButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        unEquipButton.gameObject.SetActive(false);
        dropButton.gameObject.SetActive(false);
    }

    public void EquipButtonClick()
    {
        if (selectedItemSlot == null) return;
        if (equippedItemSlot != null)
        {
            equippedItemSlot.UnEquip();
            equippedItemSlot = null;
        }

        selectedItemSlot.Equip();
        equippedItemSlot = selectedItemSlot;

        SelectItem(selectedItemSlot);
    }

    public void UnEquipButtonClick()
    {
        if (equippedItemSlot == null && selectedItemSlot != equippedItemSlot) return;

        selectedItemSlot.UnEquip();
        equippedItemSlot = null;

        SelectItem(selectedItemSlot);
    }

    public void UseButtonClick()
    {
        if (selectedItemSlot == null) return;

        selectedItemSlot.Use();
    }

    public void DropButtonClick()
    {
        if (selectedItemSlot == null) return;

        selectedItemSlot.Drop();
    }

    public void AddItem(ItemData itemData)
    {
        if (itemData.stackable)
        {
            ItemSlot slot = FindItemSlot(itemData);

            if (slot != null && itemData.maxStack >= slot.itemCount)
            {
                slot.AddItem(itemData);
            }
            else
            {
                slot = FindEmptySlot();
                if (slot == null)
                {
                    PlayerManager.Instance.Player.DropItem(itemData);
                    return;
                }
                slot.AddItem(itemData);
            }
        }
        else
        {
            ItemSlot slot = FindEmptySlot();
            if (slot == null)
            {
                PlayerManager.Instance.Player.DropItem(itemData);
            }
            else
            {
                slot.AddItem(itemData);
            }
        }

    }

    public ItemSlot FindItemSlot(ItemData itemData)
    {
        foreach (Transform itemSlot in itemSlots)
        {
            ItemSlot slot = itemSlot.GetComponent<ItemSlot>();
            if (slot.itemData == itemData)
                return slot;
        }
        return null;
    }

    public ItemSlot FindEmptySlot()
    {
        foreach (Transform itemSlot in itemSlots)
        {
            ItemSlot slot = itemSlot.GetComponent<ItemSlot>();
            if (slot.itemData == null)
                return slot;
        }

        return null;
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        Cursor.lockState = gameObject.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        UnSelectItem();
    }
}
