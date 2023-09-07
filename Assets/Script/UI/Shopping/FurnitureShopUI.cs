using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureShopUI : ItemShopUI
{
    private List<FurnitureShelfData> furnitureShelfDatas = new List<FurnitureShelfData>();
    // Start is called before the first frame update
    void OnEnable()
    {
        InitializeFurnitureShelf();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeFurnitureShelf(){
        furnitureShelfDatas.Clear();
        ResetShelf();

        foreach(GameObject furniture in ShopManager.instance.TodayFurnitureWarehouse){
            GameObject furnitureShelf = Instantiate(shelfPrefab, shelfParent.transform);
            FurnitureShelfData furnitureShelfData = furnitureShelf.GetComponent<FurnitureShelfData>();
            furnitureShelfData.SetData(furniture);

            furnitureShelfDatas.Add(furnitureShelfData);
        }
    }
}
