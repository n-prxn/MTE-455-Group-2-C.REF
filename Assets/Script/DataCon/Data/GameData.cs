using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int rollCount;
    public Dictionary<int, bool> studentCollected;
    public Dictionary<int, bool> studentSquad;

    public GameData()
    {
        this.rollCount = 0;
        studentCollected = new Dictionary<int, bool>();
        studentSquad = new Dictionary<int, bool>();
    }
}
