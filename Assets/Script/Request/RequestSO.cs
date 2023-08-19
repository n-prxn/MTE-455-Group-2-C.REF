using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public enum Difficulty{
    Easy,
    Extreme,
    Hardcore,
    Insane,
    Emergency
}

[CreateAssetMenu(fileName = "New Request", menuName = "Request")]
public class RequestSO : ScriptableObject
{
    [Header("Request Info")]
    public byte id;
    public new string name;
    public new string description;
    public new string requesterName;
    public new Difficulty difficulty;
    public bool isRepeatable = false;

    [Header("Status Requirement")]
    public int phyStat;
    public int intStat;
    public int comStat;
    public int stamina;

    [Header("Rewards")]
    public int credit;
    public int xp;
    public int happiness;
    public int crimeRate;
    public int pyroxene;
}
