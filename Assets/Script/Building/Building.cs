using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (GameManager.instance.rank < buildingSO.unlockedRank)
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

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10000f, Color.green);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Building")))
        {
            GameObject obj = hit.collider.gameObject;
            if (obj.name == gameObject.name)
            {  
                balloon.SetActive(true);
            }
            else{
                balloon.SetActive(false);
            }
        }
    }
}
