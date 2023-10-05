using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum BuildingType
{
    Dormitory,
    Gym,
    Library,
    Cafe,
    Inventory
}
public class Building : MonoBehaviour
{
    [SerializeField] private BuildingSO buildingSO;
    [SerializeField] private GameObject availableModel;
    [SerializeField] private GameObject unavailableModel;
    [SerializeField] private GameObject balloon;
    [SerializeField] private Sprite buildingIcon;
    public BuildingSO BuildingSO
    {
        get { return buildingSO; }
        set { buildingSO = value; }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.rank < buildingSO.unlockedRank)
        {
            availableModel.SetActive(false);
            unavailableModel.SetActive(true);
            buildingSO.IsAvailable = false;
        }
        else
        {
            availableModel.SetActive(true);
            unavailableModel.SetActive(false);
            buildingSO.IsAvailable = true;

            switch (buildingSO.BuildingType)
            {
                case BuildingType.Dormitory:
                    buildingSO.BonusStaminaRested = TrainingManager.instance.BonusTraining(buildingSO);
                    break;
                case BuildingType.Gym:
                    buildingSO.BonusPHYTraining = TrainingManager.instance.BonusTraining(buildingSO);
                    break;
                case BuildingType.Library:
                    buildingSO.BonusINTTraining = TrainingManager.instance.BonusTraining(buildingSO);
                    break;
                case BuildingType.Cafe:
                    buildingSO.BonusCOMTraining = TrainingManager.instance.BonusTraining(buildingSO);
                    break;
            }
        }

        if (!GameManager.Instance.uiIsOpen)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10000f, Color.green);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Building")))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.name == gameObject.name)
                {
                    if (buildingSO.IsAvailable)
                    {
                        balloon.SetActive(true);
                        balloon.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = buildingIcon;
                    }
                    else
                    {
                        balloon.SetActive(false);
                    }
                }
                else
                {
                    balloon.SetActive(false);
                }
            }
            else
            {
                balloon.SetActive(false);
            }
        }

    }
}
