using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
    public static FurnitureManager instance;
    [SerializeField] private List<GameObject> furnitureList = new List<GameObject>();
    public List<GameObject> FurnitureList
    {
        get { return furnitureList; }
        set { furnitureList = value; }
    }

    void Awake()
    {
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
    
    public void AddFurniture(GameObject furniture)
    {
        furnitureList.Add(furniture);
    }

    public void RemoveFurniture(Furniture furniture){
        furnitureList.Remove(furnitureList.Find(x => x.GetComponent<Furniture>().ID == furniture.ID));
    }
}
