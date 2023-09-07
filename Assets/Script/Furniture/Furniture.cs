using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Building
{
    Dormitory,
    Gym,
    Library,
    Cafe
}

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
    [SerializeField] private Building building;
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
}
