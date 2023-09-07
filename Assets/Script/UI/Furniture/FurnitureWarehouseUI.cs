using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureWarehouseUI : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject cardParent;

    [SerializeField] private FurnitureDescription furnitureDescription;
    private GameObject currentSelectedFurniture;
    public GameObject CurrentSelectedFurniture
    {
        get { return currentSelectedFurniture; }
        set { currentSelectedFurniture = value; }
    }

    private List<FurnitureCardData> furnitureCardDatas = new List<FurnitureCardData>();
    void OnEnable()
    {
        InitializeWarehouse();
    }

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeWarehouse()
    {
        ResetWarehouse();
        currentSelectedFurniture = null;
        foreach (GameObject furniture in FurnitureManager.instance.FurnitureList)
        {
            if (!furniture.GetComponent<Furniture>().IsPlaced)
            {
                GameObject furnitureCard = Instantiate(cardPrefab, cardParent.transform);
                FurnitureCardData furnitureCardData = furnitureCard.GetComponent<FurnitureCardData>();
                furnitureCardData.SetData(furniture.GetComponent<Furniture>(), furniture);
                furnitureCardData.OnFurnitureCardClicked += HandleFurnitureCardSelected;

                furnitureCardDatas.Add(furnitureCardData);
            }
        }
    }

    public void ResetWarehouse()
    {
        furnitureCardDatas.Clear();
        if (cardParent.transform.childCount > 0)
        {
            foreach (Transform card in cardParent.transform)
            {
                Destroy(card.gameObject);
            }
        }
    }

    public void HandleFurnitureCardSelected(FurnitureCardData obj)
    {
        furnitureDescription.SetDescription(obj.Furniture);
        currentSelectedFurniture = obj.FurniturePrefab;
    }

    public void PlaceFurniture()
    {
        FurniturePlacementManager.instance.FurniturePlacement(currentSelectedFurniture);
        currentSelectedFurniture.GetComponent<Furniture>().IsPlaced = true;
        UIDisplay.instance.TogglePanel(gameObject);
    }
}
