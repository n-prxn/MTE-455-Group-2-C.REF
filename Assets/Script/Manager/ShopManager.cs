using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour, IData
{
    public static ShopManager instance;
    [SerializeField] private List<GameObject> furnitureWarehouse = new List<GameObject>();
    public List<GameObject> FurnitureWarehouse
    {
        get { return furnitureWarehouse; }
        set { furnitureWarehouse = value; }
    }

    private List<GameObject> todayFurnitureWarehouse = new List<GameObject>();
    public List<GameObject> TodayFurnitureWarehouse
    {
        get { return todayFurnitureWarehouse; }
    }

    [SerializeField] private List<ItemSO> presentWarehouse;
    public List<ItemSO> PresentWarehouse
    {
        get { return presentWarehouse; }
        set { presentWarehouse = value; }
    }

    private List<ItemSO> todayPresentWarehouse = new List<ItemSO>();
    public List<ItemSO> TodayPresentWarehouse
    {
        get { return todayPresentWarehouse; }
        set { todayPresentWarehouse = value; }
    }

    [SerializeField] private List<ItemSO> ticketWarehouse;
    public List<ItemSO> TicketWarehouse
    {
        get { return ticketWarehouse; }
        set { ticketWarehouse = value; }
    }

    private List<ItemSO> todayTicketWarehouse = new List<ItemSO>();
    public List<ItemSO> TodayTicketWarehouse
    {
        get { return todayTicketWarehouse; }
        set { todayTicketWarehouse = value; }
    }

    [SerializeField] private int maxItem = 1;
    public int MaxItem
    {
        get { return maxItem; }
        set { maxItem = value; }
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
        GenerateShopItems();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateShopItems()
    {
        GenerateTodayFurnitures();
        GenerateTodayPresents();
        GenerateTodayTickets();
    }

    private void GenerateTodayFurnitures()
    {
        todayFurnitureWarehouse.Clear();
        ShuffleList(furnitureWarehouse);
        for (int i = 0; i < (furnitureWarehouse.Count < maxItem ? furnitureWarehouse.Count : maxItem); i++)
        {
            GameObject furniture;
            furniture = furnitureWarehouse[i];
            furniture.GetComponent<Furniture>().IsPurchased = false;
            todayFurnitureWarehouse.Add(furniture);
        }
    }

    private void GenerateTodayPresents()
    {
        todayPresentWarehouse.Clear();
        ShuffleList(presentWarehouse);
        for (int i = 0; i < (presentWarehouse.Count < maxItem ? presentWarehouse.Count : maxItem); i++)
        {
            ItemSO present;
            present = presentWarehouse[i];
            present.isPurchased = false;
            todayPresentWarehouse.Add(present);
        }
    }

    private void GenerateTodayTickets()
    {
        todayTicketWarehouse.Clear();
        ShuffleList(ticketWarehouse);
        for (int i = 0; i < (ticketWarehouse.Count < maxItem ? ticketWarehouse.Count : maxItem); i++)
        {
            ItemSO ticket;
            ticket = ticketWarehouse[i];
            ticket.isPurchased = false;
            todayTicketWarehouse.Add(ticket);
        }
    }

    void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            // Generate a random index between i and the end of the list
            int randomIndex = Random.Range(i, n);

            // Swap the elements at i and randomIndex
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    
    public void GiveReward(){
        
    }

    public void LoadData(GameData data)
    {
        maxItem = data.maxItem;

        todayFurnitureWarehouse.Clear();
        foreach(FurnitureShop fData in data.furnitureShops){
            GameObject furniture = furnitureWarehouse.Find(x => x.GetComponent<Furniture>().ID == fData.id);
            furniture.GetComponent<Furniture>().IsPurchased = fData.isPurchased;
            todayFurnitureWarehouse.Add(furniture);
        }

        todayPresentWarehouse.Clear();
        foreach(PresentShop pData in data.presentShops){
            ItemSO present = presentWarehouse.Find(x => x.id == pData.id);
            present.isPurchased = pData.isPurchased;
            todayPresentWarehouse.Add(present);
        }
        
        todayTicketWarehouse.Clear();
        foreach(TicketShop tData in data.ticketShops){
            ItemSO ticket = ticketWarehouse.Find(x => x.id == tData.id);
            ticket.isPurchased = tData.isPurchased;
            todayTicketWarehouse.Add(ticket);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.maxItem = maxItem;

        data.furnitureShops.Clear();
        foreach(GameObject furniture in todayFurnitureWarehouse){
            data.furnitureShops.Add(new FurnitureShop(furniture.GetComponent<Furniture>()));
        }

        data.presentShops.Clear();
        foreach(ItemSO present in todayPresentWarehouse){
            data.presentShops.Add(new PresentShop(present));
        }

        data.ticketShops.Clear();
        foreach(ItemSO ticket in todayTicketWarehouse){
            data.ticketShops.Add(new TicketShop(ticket));
        }
    }
}
