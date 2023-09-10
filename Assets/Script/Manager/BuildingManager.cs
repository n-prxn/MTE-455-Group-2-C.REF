using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BuildingManager : MonoBehaviour
{
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        OnLeftClick();
    }

    void OnLeftClick(){
        if(Input.GetMouseButtonDown(0)){
            TogglePanel();
        }
    }

    private void TogglePanel()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Building")))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            GameObject building = hit.collider.gameObject;

            switch (building.tag)
            {
                case "Shopping":
                    Debug.Log("Shopping");
                    GameManager.instance.sceneManager.LoadScene("Dorm");
                    break;
            }
        }
    }
}
