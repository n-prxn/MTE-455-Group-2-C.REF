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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}
