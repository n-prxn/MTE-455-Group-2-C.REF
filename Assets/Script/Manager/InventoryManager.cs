using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
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
    [SerializeField] private List<GameObject> presentList = new List<GameObject>();
    public List<GameObject> PresentList{
        get { return presentList; }
        set { presentList = value; }
    }

    [Header("Ticket")]
    [SerializeField] private List<GameObject> ticketList = new List<GameObject>();
    public List<GameObject> TicketList{
        get { return ticketList; }
        set { ticketList = value; }
    }

    void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
