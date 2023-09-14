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

public class GameData
{
    //General
    public int currentTurn { get; set; }
    public int credits { get; set; }
    public int pyroxenes { get; set; }
    public int happiness { get; set; }
    public int crimeRate { get; set; }
    public int rank { get; set; }
    public int currentXP { get; set; }

    //Furniture
    public List<FurnitureData> furnitures { get; set; }

    //Gacha
    public int rollCount;
    public Dictionary<int, bool> studentCollected { get; set; }
    public Dictionary<int, bool> studentSquad { get; set; }

    [JsonConstructor]
    public GameData()
    {
        currentTurn = 0;
        credits = 100000;
        pyroxenes = 1200;
        happiness = 50;
        crimeRate = 50;
        rank = 0;
        currentXP = 0;

        rollCount = 0;

        furnitures = new List<FurnitureData>();
        studentCollected = new Dictionary<int, bool>();
        studentSquad = new Dictionary<int, bool>();
    }
}
