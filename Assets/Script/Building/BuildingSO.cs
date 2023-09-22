using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class BuildingSO : ScriptableObject
{
    [Header("Building Info")]
    [SerializeField] public byte id;
    [SerializeField] private BuildingType buildingType;
    public BuildingType BuildingType
    {
        get { return buildingType; }
    }
    public int unlockedRank = 1;

    [Header("Capacity")]
    [SerializeField] private int studentCapacity = 3;
    public int StudentCapacity
    {
        get { return studentCapacity; }
        set { studentCapacity = value; }
    }
    [SerializeField] private int furnitureCapacity = 3;
    public int FurnitureCapacity
    {
        get { return furnitureCapacity; }
        set { furnitureCapacity = value; }
    }

    private int currentFurnitureAmount = 0;
    public int CurrentFurnitureAmount
    {
        get { return currentFurnitureAmount; }
        set { currentFurnitureAmount = value; }
    }
    [SerializeField] private int trainingDuration = 3;
    public int TrainingDuration
    {
        get { return trainingDuration; }
        set { trainingDuration = value; }
    }

    private int bonusPHYTraining = 0;
    public int BonusPHYTraining { get => bonusPHYTraining; set => bonusPHYTraining = value; }
    private int bonusINTTraining = 0;
    public int BonusINTTraining { get => bonusINTTraining; set => bonusINTTraining = value; }
    private int bonusCOMTraining = 0;
    public int BonusCOMTraining { get => bonusCOMTraining; set => bonusCOMTraining = value; }
    private int bonusStaminaRested = 0;
    public int BonusStaminaRested { get => bonusStaminaRested; set => bonusStaminaRested = value; }
    private bool isAvailable = false;
    public bool IsAvailable
    {
        get { return isAvailable; }
        set { isAvailable = value; }
    }

    public void InitializeBuilding()
    {
        bonusPHYTraining = 0;
        bonusINTTraining = 0;
        bonusCOMTraining = 0;
        bonusStaminaRested = 0;
        isAvailable = false;

        studentCapacity = 3;
        furnitureCapacity = 3;
        currentFurnitureAmount = 0;
    }
}
