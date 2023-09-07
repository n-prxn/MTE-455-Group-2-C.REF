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
    private GameObject furnitureModel;

    public GameObject buildingCursor;
    public static FurniturePlacementManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        currentCursorPos = GridPlacement.instance.GetCurrentTilePosition();

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10000f, Color.green);

        if (Input.GetKeyDown(KeyCode.Escape))
            CancelPlacement();

        if (isPlacing)
        {
            buildingCursor.transform.position = currentCursorPos;
            gridPlane.SetActive(true);

            OnRightClick();
            OnLeftClick();
        }
        else if (isEditing)
        {
            if (buildingCursor == null)
            {
                Debug.Log("Find Furniture");
                if (Input.GetMouseButtonUp(0))
                    MoveFurniture();
            }
            else
            {
                Debug.Log("Ready to Place");
                buildingCursor.transform.position = currentCursorPos;

                OnRightClick();
                OnLeftClick();
            }
        }
        else
        {
            gridPlane.SetActive(false);
        }
    }

    public void EnableEditMode()
    {
        gridPlane.SetActive(true);
        isEditing = true;
        UIDisplay.instance.TogglePanel(furnitureWarehouseUI);
    }

    public void MoveFurniture()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            GameObject obj = hit.collider.gameObject;

            if (obj.tag == "Furniture")
            {
                furnitureModel = Instantiate(obj, currentCursorPos, obj.transform.rotation);
                FurniturePlacement furniturePlacement = furnitureModel.GetComponent<FurniturePlacement>();
                furniturePlacement.Plane.SetActive(true);

                buildingCursor = furnitureModel;
                Destroy(obj);

                Debug.Log("Move");
            }
        }
    }

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
        furnitureObj.GetComponent<FurniturePlacement>().Plane.SetActive(false);
        
        CancelPlacement();
    }

    private void RotateBuilding()
    {
        furnitureModel.transform.rotation *= Quaternion.Euler(Vector3.up * 90);
    }

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

    private void CancelPlacement()
    {
        isPlacing = false;
        isEditing = false;
        buildingCursor = null;

        if (furnitureModel != null)
            Destroy(furnitureModel);

        UIDisplay.instance.TogglePanel(furnitureWarehouseUI);
    }
}
