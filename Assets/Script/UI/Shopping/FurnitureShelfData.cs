using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class FurnitureShelfData : ItemShelfData
{
    private Furniture furniture;
    private GameObject furniturePrefab;
    public GameObject FurniturePrefab
    {
        get { return furniturePrefab; }
        set { furniturePrefab = value; }
    }

    public void SetData(GameObject furniture)
    {
        furniturePrefab = furniture;
        this.furniture = furniture.GetComponent<Furniture>();

        itemNameText.text = this.furniture.Name;
        itemDescText.text = this.furniture.Description;
        costText.text = this.furniture.Cost.ToString();
        itemImage.sprite = this.furniture.FurnitureSprite;
    }

    public void Buy(){
        if(GameManager.Instance.credits < furniture.Cost)
            return;

        furniture.IsPurchased = true;
        InventoryManager.instance.FurnitureList.Add(furniturePrefab);

        GameManager.Instance.credits -= furniture.Cost;
        buyButton.SetActive(false);
        soldButton.SetActive(true);
    }
}
