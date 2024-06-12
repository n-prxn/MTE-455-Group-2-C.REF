using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BuildingManager : MonoBehaviour
{
    private Vector3 mouseDownOrigin;
    private Camera cam;

    [SerializeField] GameObject warningPanel;
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

    void OnLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownOrigin = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0) && mouseDownOrigin == Input.mousePosition)
        {
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
            Building buildingData = building.GetComponent<Building>();

            if (!buildingData.BuildingSO.IsAvailable)
            {
                // if (warningPanel = null)
                // {
                //     warningPanel = GameObject.Find("WarningPanel");
                // }
                StartCoroutine(OpenCloseWarningPanel());
                return;
            }

            if (building.tag == "Schale")
                UIDisplay.instance.TogglePanel(UIDisplay.instance.overallPanel);

            if (building.tag == "Shopping")
                UIDisplay.instance.TogglePanel(UIDisplay.instance.shopPanel);

            TrainingManager.instance.CurrentBuilding = buildingData.BuildingSO.BuildingType;

            switch (buildingData.BuildingSO.BuildingType)
            {
                case BuildingType.Dormitory:
                    GameManager.Instance.LoadScene("Dorm");
                    break;
                case BuildingType.Cafe:
                    GameManager.Instance.LoadScene("Cafe");
                    break;
                case BuildingType.Library:
                    GameManager.Instance.LoadScene("Library");
                    break;
                case BuildingType.Gym:
                    GameManager.Instance.LoadScene("Gym");
                    break;
                case BuildingType.Inventory:
                    break;
                default:
                    break;
            }
            //StudentSpawner.instance.GenerateStudentOnMap(TrainingManager.instance.GetCurrentStudentsInBuilding());
        }
    }

    IEnumerator OpenCloseWarningPanel()
    {
        warningPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        warningPanel.SetActive(false);

    }
}
