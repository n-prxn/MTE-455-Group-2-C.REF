using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject gridPlane;
    [SerializeField] private GameObject furniturePrefab;
    [SerializeField] private GameObject furnitureParent;
    private Camera cam;
    private Vector3 currentCursorPos;
    private bool isPlacing;

    public GameObject buildingCursor;

    void Start(){
        cam = Camera.main;
    }

    void Update(){
        currentCursorPos = GridPlacement.instance.GetCurrentTilePosition();

        if(isPlacing){
            buildingCursor.transform.position = currentCursorPos;
            gridPlane.SetActive(true);
        }else{
            gridPlane.SetActive(false);
        }
    
    }

    public void FurniturePlacement(GameObject furniturePrefab){
        isPlacing = true;

        this.furniturePrefab = furniturePrefab;
        GameObject furnitureModel = Instantiate(this.furniturePrefab, currentCursorPos, Quaternion.identity);
        furnitureModel.GetComponent<FurniturePlacement>().Plane.SetActive(true);

        buildingCursor = furnitureModel;
        buildingCursor.SetActive(true);
    }
}
