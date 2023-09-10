using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Present,
    Ticket
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    [Header("Item Info")]
    public byte id;
    public new string name;
    [TextArea] public string description;
    public Sprite sprite;
    public ItemType itemType;
    public int cost;
    [SerializeField] private int amount = 0;
    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }
}
