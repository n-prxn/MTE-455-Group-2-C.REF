using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IData
{
    public static InventoryManager instance;

    [Header("Furniture")]
    [SerializeField] private List<GameObject> furnitureList = new List<GameObject>();
    public List<GameObject> FurnitureList
    {
        get { return furnitureList; }
        set { furnitureList = value; }
    }

    [Header("Present")]
    [SerializeField] private List<ItemSO> presentList = new List<ItemSO>();
    public List<ItemSO> PresentList
    {
        get { return presentList; }
        set { presentList = value; }
    }

    [Header("Ticket")]
    [SerializeField] private List<ItemSO> ticketList = new List<ItemSO>();
    public List<ItemSO> TicketList
    {
        get { return ticketList; }
        set { ticketList = value; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItemToInventory(ItemSO item)
    {
        if (item.itemType == ItemType.Present)
        {
            if (presentList.Find(x => x.id == item.id) == null)
                presentList.Add(item);
            presentList.Find(x => x.id == item.id).Amount++;
        }
        else
        {
            if (ticketList.Find(x => x.id == item.id) == null)
                ticketList.Add(item);
            ticketList.Find(x => x.id == item.id).Amount++;
        }
    }

    public void AddFurniture(GameObject furniture)
    {
        furnitureList.Add(furniture);
    }

    public void LoadData(GameData data)
    {
        furnitureList.Clear();
        foreach (FurnitureData fData in data.furnitures)
        {
            GameObject furniture = ShopManager.instance.FurnitureWarehouse.Find(x => x.GetComponent<Furniture>().ID == fData.id);
            furnitureList.Add(furniture);
        }

        foreach(ItemData iData in data.items){
            ItemSO item = ShopManager.instance.PresentWarehouse.Find(x => x.id == iData.id);
            if(item != null)
                presentList.Add(item);

            item = ShopManager.instance.TicketWarehouse.Find(x => x.id == iData.id);
            if(item != null)
                ticketList.Add(item);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.furnitures = new List<FurnitureData>();
        foreach (GameObject furniture in FurnitureList)
        {
            data.furnitures.Add(new FurnitureData(furniture));
        }

        data.items = new List<ItemData>();
        foreach(ItemSO item in PresentList){
            data.items.Add(new ItemData(item));
        }

        foreach(ItemSO item in TicketList){
            data.items.Add(new ItemData(item));
        }
    }
}
