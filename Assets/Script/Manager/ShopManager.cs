using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
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
}
