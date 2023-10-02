using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FurniturePlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject gridPlane;
    [SerializeField] private GameObject furniturePrefab;
    [SerializeField] private GameObject furnitureParent;
    [SerializeField] private GameObject furnitureWarehouseUI;
    private Camera cam;
    [SerializeField] private Vector3 currentCursorPos;
    public Vector3 CurrentCursorPos
    {
        get { return currentCursorPos; }
    }
    private bool isPlacing;
    private bool isEditing;
    private bool isStoring;
    private GameObject furnitureModel;

    public GameObject buildingCursor;
    public static FurniturePlacementManager instance;

    void OnEnable(){
        InitializeBuilding();
        //Debug.Log("Placing Furnitures");
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cam = Camera.main;
        furnitureWarehouseUI = GameObject.Find("UI Canvas").transform.GetChild(2).GetChild(3).GetChild(13).gameObject;
    }

    void Update()
    {
        currentCursorPos = GridPlacement.instance.GetCurrentTilePosition();

        if (Input.GetKeyDown(KeyCode.Escape))
            CancelPlacement();

        if (isPlacing)
        {
            buildingCursor.transform.position = currentCursorPos;
            gridPlane.SetActive(true);
            UIDisplay.instance.EnableMode(0);

            OnRightClick();
            OnLeftClick();
        }
        else if (isEditing)
        {
            UIDisplay.instance.EnableMode(1);
            if (buildingCursor == null)
            {
                MoveFurniture();
            }
            else
            {
                buildingCursor.transform.position = currentCursorPos;

                OnRightClick();
                OnLeftClick();
            }
        }
        else if (isStoring)
        {
            UIDisplay.instance.EnableMode(2);
            StoreFurniture();
        }
        else
        {
            UIDisplay.instance.DisableMode();
            gridPlane.SetActive(false);
        }
    }

    public void InitializeBuilding()
    {
        ResetBuilding();
        foreach (GameObject furniture in InventoryManager.instance.FurnitureList)
        {
            Furniture furnitureComp = furniture.GetComponent<Furniture>();
            if (furnitureComp.IsPlaced && TrainingManager.instance.CurrentBuilding == furnitureComp.Building)
            {
                GameObject furnitureObj = Instantiate(furniture, furnitureComp.Position, Quaternion.Euler(furnitureComp.Rotation), furnitureParent.transform);
                furnitureObj.name = furnitureObj.GetComponent<Furniture>().Name;
            }
        }
    }

    public void ResetBuilding(){
        if(furnitureParent.transform.childCount > 0){
            foreach(Transform furniture in furnitureParent.transform){
                Destroy(furniture.gameObject);
            }
        }
    }

    #region Editing Mode
    public void MoveFurniture()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10000f, Color.green);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Furniture")))
        {
            GameObject obj = hit.collider.gameObject;
            Debug.Log(obj.name);

            if (obj.tag == "Furniture")
            {
                if (Input.GetMouseButtonUp(0))
                {
                    furnitureModel = Instantiate(obj, currentCursorPos, obj.transform.rotation);
                    FurniturePlacement furniturePlacement = furnitureModel.GetComponent<FurniturePlacement>();
                    furniturePlacement.Plane.SetActive(true);

                    buildingCursor = furnitureModel;
                    Destroy(obj);
                }
            }
        }
    }
    public void EnableEditMode(GameObject panel)
    {
        gridPlane.SetActive(true);
        isEditing = true;
        UIDisplay.instance.TogglePanel(panel);
    }

    #endregion

    #region Placement Mode
    public void FurniturePlacement(GameObject furniturePrefab)
    {
        isPlacing = true;

        this.furniturePrefab = furniturePrefab;

        furnitureModel = Instantiate(this.furniturePrefab, currentCursorPos, Quaternion.identity);
        FurniturePlacement furniturePlacement = furnitureModel.GetComponent<FurniturePlacement>();
        furniturePlacement.Plane.SetActive(true);

        buildingCursor = furnitureModel;
    }

    private void Place()
    {
        if (!buildingCursor.GetComponent<FurniturePlacement>().CanBuild)
            return;

        GameObject furnitureObj = Instantiate(furnitureModel, currentCursorPos, furnitureModel.transform.rotation, furnitureParent.transform);
        Furniture currentFurniture = InventoryManager.instance.FurnitureList.Find(x => x.GetComponent<Furniture>().ID == furnitureObj.GetComponent<Furniture>().ID).GetComponent<Furniture>();
        currentFurniture.SetTransform(furnitureModel.transform.position, furnitureModel.transform.localEulerAngles);
        currentFurniture.Building = TrainingManager.instance.CurrentBuilding;
        currentFurniture.IsPlaced = true;

        furnitureObj.name = furnitureObj.GetComponent<Furniture>().Name;
        furnitureObj.GetComponent<FurniturePlacement>().Plane.SetActive(false);

        if (isEditing)
        {
            buildingCursor = null;
            Destroy(furnitureModel);

            return;
        }

        CountFurniture();
        CancelPlacement();
    }

    private void RotateBuilding()
    {
        furnitureModel.transform.rotation *= Quaternion.Euler(Vector3.up * 90);
    }

    private void CancelPlacement()
    {
        isPlacing = false;
        isEditing = false;
        isStoring = false;
        buildingCursor = null;

        if (furnitureModel != null)
            Destroy(furnitureModel);

        UIDisplay.instance.TogglePanel(furnitureWarehouseUI);
    }

    #endregion

    #region Store Mode

    public void StoreFurniture()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10000f, Color.green);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Furniture")))
        {
            GameObject obj = hit.collider.gameObject;

            if (obj.tag == "Furniture")
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Furniture currentFurniture = InventoryManager.instance.FurnitureList.Find(x => x.GetComponent<Furniture>().ID == obj.GetComponent<Furniture>().ID).GetComponent<Furniture>();
                    currentFurniture.IsPlaced = false;
                    currentFurniture.Building = BuildingType.Inventory;
                    currentFurniture.SetTransform(Vector3.zero, Vector3.zero);

                    Destroy(obj);
                    CountFurniture();
                }
            }
        }
    }

    public void EnableStoreMode(GameObject panel)
    {
        gridPlane.SetActive(true);
        isStoring = true;
        UIDisplay.instance.TogglePanel(panel);
    }
    #endregion

    #region Mouse Controller
    private void OnLeftClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (isPlacing || isEditing)
                Place();
        }
    }

    private void OnRightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RotateBuilding();
        }
    }
    #endregion

    public void CountFurniture()
    {
        int amount = 0;
        foreach (GameObject furniture in InventoryManager.instance.FurnitureList)
        {
            Furniture furnitureComp = furniture.GetComponent<Furniture>();
            if (furnitureComp.IsPlaced && furnitureComp.Building == TrainingManager.instance.CurrentBuilding)
                amount++;
        }
        TrainingManager.instance.GetCurrentBuilding().CurrentFurnitureAmount = amount;
    }
}
