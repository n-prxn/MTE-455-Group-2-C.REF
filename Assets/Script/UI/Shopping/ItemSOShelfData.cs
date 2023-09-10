using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSOShelfData : ItemShelfData
{
    private ItemSO item;
    public void SetData(ItemSO item)
    {
        this.item = item;

        itemNameText.text = this.item.name;
        itemDescText.text = this.item.description;
        costText.text = this.item.cost.ToString();
        itemImage.sprite = this.item.sprite;
    }

    public void Buy()
    {
        if (GameManager.instance.credits < item.cost)
            return;

        IsPurchased = true;
        InventoryManager.instance.AddItemToInventory(item);

        GameManager.instance.credits -= item.cost;
        buyButton.SetActive(false);
        soldButton.SetActive(true);
    }
}
