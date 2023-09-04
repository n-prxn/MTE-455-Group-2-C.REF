using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridPlacement : MonoBehaviour
{
    [SerializeField] private int gridSize = 5;
    private Camera cam;

    public static GridPlacement instance;
    
    void Awake(){
        instance = this;
    }

    void Start(){
        cam = Camera.main;
    }

    public Vector3 GetCurrentTilePosition(){
        if(EventSystem.current.IsPointerOverGameObject())
            return new Vector3(0, -99, 0);

        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        float rayDistance = 0.0f;

        if(plane.Raycast(ray, out rayDistance)){
            Vector3 newPos = ray.GetPoint(rayDistance);
            newPos = new Vector3(Mathf.RoundToInt(newPos.x / gridSize) * gridSize, 0.0f, Mathf.RoundToInt(newPos.z / gridSize) * gridSize);
            return newPos;
        }

        return new Vector3(0, -99, 0);
    }
}
