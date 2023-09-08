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

    [SerializeField] private List<GameObject> presentWarehouse;
    public List<GameObject> PresentWarehouse
    {
        get { return presentWarehouse; }
        set { presentWarehouse = value; }
    }
    [SerializeField] private List<GameObject> ticketWarehouse;
    public List<GameObject> TicketWarehouse
    {
        get { return ticketWarehouse; }
        set { ticketWarehouse = value; }
    }

    [SerializeField] private int maxItem = 3;
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateTodayFurnitures();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateTodayFurnitures()
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
}
