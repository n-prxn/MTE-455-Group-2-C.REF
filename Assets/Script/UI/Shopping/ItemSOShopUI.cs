using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSOShopUI : ItemShopUI
{
    [SerializeField] private ItemType itemType;
    private List<ItemSOShelfData> itemSOShelfDatas = new List<ItemSOShelfData>();
    // Start is called before the first frame update
    void OnEnable()
    {
        
    }

    void Start(){
        InitializeItemSOShelf();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeItemSOShelf()
    {
        itemSOShelfDatas.Clear();
        ResetShelf();

        foreach (ItemSO item in
                itemType == ItemType.Present ? ShopManager.instance.TodayPresentWarehouse : ShopManager.instance.TodayTicketWarehouse)
        {
            GameObject itemSOshelf = Instantiate(shelfPrefab, shelfParent.transform);
            ItemSOShelfData itemSOShelfData = itemSOshelf.GetComponent<ItemSOShelfData>();
            itemSOShelfData.SetData(item);

            itemSOShelfDatas.Add(itemSOShelfData);
        }
    }
}
