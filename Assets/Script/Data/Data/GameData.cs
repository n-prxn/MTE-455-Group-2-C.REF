using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class FurnitureData
{
    public int id { get; set; }
    public bool isPlaced { get; set; }
    public string currentBuilding { get; set; }
    public float posX { get; set; }
    public float posY { get; set; }
    public float posZ { get; set; }
    public float rotX { get; set; }
    public float rotY { get; set; }
    public float rotZ { get; set; }
    public FurnitureData() { }
    public FurnitureData(GameObject furniture)
    {
        Furniture furnitureComp = furniture.GetComponent<Furniture>();
        id = furnitureComp.ID;
        isPlaced = furnitureComp.IsPlaced;
        currentBuilding = furnitureComp.Building.ToString();
        posX = furnitureComp.Position.x;
        posY = furnitureComp.Position.y;
        posZ = furnitureComp.Position.z;
        rotX = furnitureComp.Rotation.x;
        rotY = furnitureComp.Rotation.y;
        rotZ = furnitureComp.Rotation.z;
    }
}

public class ItemData
{
    public int id { get; set; }
    public int amount { get; set; }
    public ItemData() { }
    public ItemData(ItemSO item)
    {
        id = item.id;
        amount = item.Amount;
    }
}

public class StudentData
{
    public int id { get; set; }
    public bool collected { get; set; }
    public bool squadCollect { get; set; }
    public int currentPHYStat { get; set; }
    public int currentINTStat { get; set; }
    public int currentCOMStat { get; set; }
    public int currentStamina { get; set; }
    public bool isBuff { get; set; }
    public int buffDuration { get; set; }
    public StudentData() { }
    public StudentData(Student student)
    {
        id = student.id;
        collected = student.Collected;
        squadCollect = student.SquadCollect;
        currentPHYStat = student.CurrentPHYStat;
        currentINTStat = student.CurrentINTStat;
        currentCOMStat = student.CurrentCOMStat;
        currentStamina = student.CurrentStamina;

        isBuff = student.IsBuff;
        buffDuration = student.BuffDuration;
    }
}

public class RequestData
{
    public int id { get; set; }
    public int currentCredit { get; set; }
    public int currentXP { get; set; }
    public int currentHappiness { get; set; }
    public int currentCrimeRate { get; set; }
    public int currentDemeritHappiness { get; set; }
    public int currentDemeritCrimeRate { get; set; }
    public bool isOperating { get; set; }
    public bool isRead { get; set; }
    public bool isDone { get; set; }
    public bool isShow { get; set; }
    public bool isSuccess { get; set; }
    public int successRate { get; set; }
    public int currentTurn { get; set; }
    public int expiredCount { get; set; }
    public int[] squad = new int[4];
    public RequestData() { }
    public RequestData(RequestSO request)
    {
        id = request.id;
        currentCredit = request.CurrentCredit;
        currentXP = request.CurrentXP;
        currentHappiness = request.CurrentHappiness;
        currentCrimeRate = request.CurrentCrimeRate;

        currentDemeritHappiness = request.CurrentDemeritHappiness;
        currentDemeritCrimeRate = request.CurrentDemeritCrimeRate;

        isOperating = request.IsOperating;
        isRead = request.IsRead;
        isDone = request.IsDone;
        isShow = request.IsShow;
        isSuccess = request.IsSuccess;

        successRate = request.SuccessRate;
        currentTurn = request.CurrentTurn;
        expiredCount = request.ExpireCount;

        squad[0] = request.squad[0] == null ? -1 : request.squad[0].id;
        squad[1] = request.squad[1] == null ? -1 : request.squad[1].id;
        squad[2] = request.squad[2] == null ? -1 : request.squad[2].id;
        squad[3] = request.squad[3] == null ? -1 : request.squad[3].id;
    }
}

public class BuildingTrainingData
{
    public int id { get; set; }
    public bool isAvailable { get; set; }
    public int studentCapacity { get; set; }
    public int furnitureCapacity { get; set; }
    public List<int> students { get; set; }
    public BuildingTrainingData() { }
    public BuildingTrainingData(BuildingSO building, List<Student> students)
    {
        id = building.id;
        isAvailable = building.IsAvailable;
        studentCapacity = building.StudentCapacity;
        furnitureCapacity = building.FurnitureCapacity;

        this.students = new List<int>();
        for (int i = 0; i < 7; i++)
        {
            if (students[i] == null)
            {
                this.students.Add(-1);
            }
            else
            {
                this.students.Add(students[i].id);
            }
        }
    }
}

public class GameData
{
    //General
    public int currentTurn { get; set; }
    public int credits { get; set; }
    public int pyroxenes { get; set; }
    public int elephs { get; set; }
    public int happiness { get; set; }
    public int crimeRate { get; set; }
    public int rank { get; set; }
    public int currentXP { get; set; }
    public int successRequest { get; set; }
    public int failedRequest { get; set; }
    public int requestPerTurn { get; set; }
    public int maxRequestCapacity { get; set; }

    //Furniture
    public List<FurnitureData> furnitures { get; set; }

    //Students
    public List<StudentData> students { get; set; }

    //Request
    public List<RequestData> requests { get; set; }

    public List<ItemData> items { get; set; }
    public List<BuildingTrainingData> trainingBuildings { get; set; }

    //Gacha
    public int rollCount;

    [JsonConstructor]
    public GameData()
    {
        currentTurn = 0;
        credits = 100000;
        pyroxenes = 120;
        elephs = 0;
        happiness = 50;
        crimeRate = 50;
        rank = 0;
        currentXP = 0;
        successRequest = 0;
        failedRequest = 0;

        rollCount = 0;

        requestPerTurn = 1;
        maxRequestCapacity = 3;

        furnitures = new List<FurnitureData>();
        students = new List<StudentData>();
        requests = new List<RequestData>();
        items = new List<ItemData>();
        trainingBuildings = new List<BuildingTrainingData>();
    }
}
