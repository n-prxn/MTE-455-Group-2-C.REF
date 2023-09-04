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
