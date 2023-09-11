using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public enum Rarity
{
    Common,
    Uncommon,
    Rare
}

public enum School
{
    Abydos,
    Arius,
    Gehenna,
    Hyakkiyako,
    Millennium,
    RedWinter,
    Shanhaijin,
    SRT,
    Trinity,
    Valkyrie
}

[CreateAssetMenu(fileName = "New Student", menuName = "Student")]
public class Student : ScriptableObject
{
    [Header("Student Info")]
    public byte id;
    public new string name;
    [JsonIgnore] public Sprite portrait;
    //[JsonIgnore] public Sprite portraitBig;
    [JsonIgnore] public Sprite artwork;
    public string club;
    public School school;
    [JsonIgnore][TextArea] public string detail;

    [Header("Gacha Info")]
    public Rarity rarity;
    [SerializeField] private bool collected;
    public bool Collected
    {
        get { return collected; }
        set { collected = value; }
    }
    [SerializeField] private bool squadCollect;
    public bool SquadCollect
    {
        get { return squadCollect; }
        set { squadCollect = value; }
    }

    //Status
    [Header("Basic Stats")]
    [JsonIgnore] public int phyStat;
    [JsonIgnore] public int intStat;
    [JsonIgnore] public int comStat;
    [JsonIgnore] public int stamina;

    [Header("Current Stats")]
    private int currentPHYStat;
    public int CurrentPHYStat
    {
        get { return currentPHYStat; }
        set { currentPHYStat = value; }
    }
    private int currentINTStat;
    public int CurrentINTStat
    {
        get { return currentINTStat; }
        set { currentINTStat = value; }
    }
    private int currentCOMStat;
    public int CurrentCOMStat
    {
        get { return currentCOMStat; }
        set { currentCOMStat = value; }
    }
    private int currentStamina;
    public int CurrentStamina
    {
        get { return currentStamina; }
        set { currentStamina = value; }
    }

    [JsonIgnore]
    private float gachaRate;
    [JsonIgnore]
    public float GachaRate
    {
        get { return gachaRate; }
        set { gachaRate = value; }
    }

    [Header("Squad")]
    [SerializeField] private bool isAssign = false;
    public bool IsAssign
    {
        get { return isAssign; }
        set { isAssign = value; }
    }

    [Header("Training")]
    [SerializeField] private bool isTraining = false;
    public bool IsTraining
    {
        get { return isTraining; }
        set { isTraining = value; }
    }

    public void InitializeStartStats()
    {
        currentPHYStat = phyStat;
        currentINTStat = intStat;
        currentCOMStat = comStat;
        currentStamina = stamina;
    }
}
