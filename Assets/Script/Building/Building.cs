using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

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
    }
}
