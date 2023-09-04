using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Building{
    Dormitory,
    Gym,
    Library,
    Cafe
}

public abstract class Furniture : MonoBehaviour
{
    [SerializeField] private byte id;
    [SerializeField] private new string name;
    [SerializeField] private int cost;
    [SerializeField] private Building building;
}
