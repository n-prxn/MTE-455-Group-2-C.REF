using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int rollCount;
    public Dictionary<int, bool> studentCollexted;

    public GameData()
    {
        this.rollCount = 0;
        studentCollexted = new Dictionary<int, bool>();
    }
}
