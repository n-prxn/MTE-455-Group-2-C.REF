using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FurniturePlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject gridPlane;
    [SerializeField] private GameObject furniturePrefab;
    [SerializeField] private GameObject furnitureParent;
    [SerializeField] private GameObject furnitureWarehouseUI;
    private Camera cam;
    [SerializeField] private Vector3 currentCursorPos;
    public Vector3 CurrentCursorPos{
        get{ return currentCursorPos; }
    }
    private bool isPlacing;
    private GameObject furnitureModel;

    public GameObject buildingCursor;
    public static FurniturePlacementManager instance;

    void Awake(){
        instance = this;
    }

    void Start(){
        cam = Camera.main;
    }

    void Update(){
        currentCursorPos = GridPlacement.instance.GetCurrentTilePosition();

        if(Input.GetKeyDown(KeyCode.Escape))
            CancelPlacement();

        if(isPlacing){
            buildingCursor.transform.position = currentCursorPos;
            gridPlane.SetActive(true);
            OnRightClick();
            OnLeftClick();
        }else{
            gridPlane.SetActive(false);
        }
    }

    public void FurniturePlacement(GameObject furniturePrefab){
        isPlacing = true;

        this.furniturePrefab = furniturePrefab;
        
        furnitureModel = Instantiate(this.furniturePrefab, currentCursorPos, Quaternion.identity);
        FurniturePlacement furniturePlacement = furnitureModel.GetComponent<FurniturePlacement>();
        furniturePlacement.Plane.SetActive(true);

        buildingCursor = furnitureModel;
        buildingCursor.SetActive(true);
    }

    private void Place(){
        if(!buildingCursor.GetComponent<FurniturePlacement>().CanBuild)
            return;

        GameObject furnitureObj = Instantiate(furnitureModel, currentCursorPos, furnitureModel.transform.rotation, furnitureParent.transform);
        furnitureObj.GetComponent<FurniturePlacement>().Plane.SetActive(false);
        CancelPlacement();
    }

    private void RotateBuilding()
    {
        furnitureModel.transform.rotation *= Quaternion.Euler(Vector3.up * 90);
    }

    private void OnLeftClick(){
        if(Input.GetMouseButtonUp(0)){
            if(isPlacing)
                Place();
        }
    }

    private void OnRightClick(){
        if(Input.GetMouseButtonDown(1)){
            RotateBuilding();
        }
    }

    private void CancelPlacement(){
        isPlacing = false;
        if(buildingCursor != null)
            buildingCursor.SetActive(false);

        if(furnitureModel != null)
            Destroy(furnitureModel);

        UIDisplay.instance.TogglePanel(furnitureWarehouseUI);
    }
}
