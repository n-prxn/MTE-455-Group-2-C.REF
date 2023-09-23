using System.Collections;
using System.Collections.Generic;
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
    public int MaxItem{
        get {return maxItem;}
        set {maxItem = value;}
    }
    void Awake()
    {instance = this;
        /*if (instance == null)
        {
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/
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
        for (int i = 0; i < maxItem; i++)
        {
            GameObject furniture;
            do
            {
                furniture = furnitureWarehouse[Random.Range(0, furnitureWarehouse.Count - 1)];
            } while (todayFurnitureWarehouse.Contains(furniture));
            todayFurnitureWarehouse.Add(furniture);
        }
    }

    private void GenerateTodayPresents()
    {
        todayPresentWarehouse.Clear();
        for (int i = 0; i < maxItem; i++)
        {
            ItemSO present;
            do
            {
                present = presentWarehouse[Random.Range(0, presentWarehouse.Count - 1)];
            } while (todayPresentWarehouse.Contains(present));
            todayPresentWarehouse.Add(present);
        }
    }

    private void GenerateTodayTickets()
    {
        todayTicketWarehouse.Clear();
        for (int i = 0; i < maxItem; i++)
        {
            ItemSO ticket;
            do
            {
                ticket = ticketWarehouse[Random.Range(0, ticketWarehouse.Count - 1)];
            } while (todayTicketWarehouse.Contains(ticket));
            todayTicketWarehouse.Add(ticket);
        }
    }
}
