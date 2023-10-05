using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    [SerializeField] private byte id;
    public byte ID{
        get{ return id; }
        set{ id = value;}
    }
    [SerializeField] private new string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    [TextArea][SerializeField] private string description;
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    [SerializeField] private int cost;
    public int Cost{
        get {return cost;}
        set {cost = value;}
    }
    [SerializeField] private BuildingType building;
    public BuildingType Building{
        get{return building;}
        set{building = value;}
    }
    [SerializeField] private Sprite furnitureSprite;
    public Sprite FurnitureSprite
    {
        get { return furnitureSprite; }
        set { furnitureSprite = value; }
    }
    [SerializeField] private bool isPlaced = false;
    public bool IsPlaced{
        get { return isPlaced; }
        set { isPlaced = value; }
    }
    [SerializeField] private bool isPurchased = false;
    public bool IsPurchased{
        get {return isPurchased;}
        set {isPurchased = value;}
    }
    [SerializeField] private Vector3 position = new Vector3(0,0,0);
    public Vector3 Position{
        get{return position;}
    }
    [SerializeField] private Vector3 rotation = new Vector3(0,0,0);
    public Vector3 Rotation{
        get{return rotation;}
    }

    public void SetTransform(Vector3 position, Vector3 rotation){
        this.position = position;
        this.rotation = rotation;
    }
}
