using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureWarehouseUI : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject cardParent;
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private GameObject idlePanel;

    [SerializeField] private FurnitureDescription furnitureDescription;
    [SerializeField] private Button editButton;
    [SerializeField] private Button storeButton;
    private GameObject currentSelectedFurniture;
    public GameObject CurrentSelectedFurniture
    {
        get { return currentSelectedFurniture; }
        set { currentSelectedFurniture = value; }
    }

    private List<FurnitureCardData> furnitureCardDatas = new List<FurnitureCardData>();
    private FurniturePlacementManager furniturePlacementManager;
    void OnEnable()
    {
        InitializeWarehouse();
        furniturePlacementManager = GameObject.Find("FurnitureManager").GetComponent<FurniturePlacementManager>();
        if (furniturePlacementManager != null)
        {
            editButton.onClick.RemoveAllListeners();
            storeButton.onClick.RemoveAllListeners();

            editButton.onClick.AddListener(() => furniturePlacementManager.EnableEditMode(gameObject));
            editButton.onClick.AddListener(() => UIDisplay.instance.ToggleBlackBackground(false));

            storeButton.onClick.AddListener(() => furniturePlacementManager.EnableStoreMode(gameObject));
            storeButton.onClick.AddListener(() => UIDisplay.instance.ToggleBlackBackground(false));
        }
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
        foreach (GameObject furniture in InventoryManager.instance.FurnitureList)
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

        descriptionPanel.SetActive(false);
        idlePanel.SetActive(true);
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
        descriptionPanel.SetActive(true);
        idlePanel.SetActive(false);

        furnitureDescription.SetDescription(obj.Furniture);
        currentSelectedFurniture = obj.FurniturePrefab;
    }

    public void PlaceFurniture()
    {
        FurniturePlacementManager.instance.FurniturePlacement(currentSelectedFurniture);
        UIDisplay.instance.TogglePanel(gameObject);
    }
}
