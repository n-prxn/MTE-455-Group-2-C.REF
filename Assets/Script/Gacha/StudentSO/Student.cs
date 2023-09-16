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
    [SerializeField] private int currentPHYStat = 0;
    public int CurrentPHYStat
    {
        get { return currentPHYStat; }
        set { currentPHYStat = value; }
    }
    [SerializeField] private int currentINTStat = 0;
    public int CurrentINTStat
    {
        get { return currentINTStat; }
        set { currentINTStat = value; }
    }
    [SerializeField] private int currentCOMStat = 0;
    public int CurrentCOMStat
    {
        get { return currentCOMStat; }
        set { currentCOMStat = value; }
    }
    [SerializeField] private int currentStamina = 0;
    public int CurrentStamina
    {
        get { return currentStamina; }
        set { currentStamina = value; }
    }

    [Header("Trained Stats")]
    private int trainedPHYStat;
    public int TrainedPHYStat{
        get{ return trainedPHYStat;}
        set{ trainedPHYStat = value;}
    }

    private int trainedINTStat;
    public int TrainedINTtat{
        get{ return trainedINTStat;}
        set{ trainedINTStat = value;}
    }

    private int trainedCOMStat;
    public int TrainedCOMtat{
        get{ return trainedCOMStat;}
        set{ trainedCOMStat = value;}
    }

    private int restedStamina;
    public int RestedStamina{
        get{ return restedStamina;}
        set{ restedStamina = value;}
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
    [SerializeField] private bool isOperating = false;
    public bool IsOperating { get => isOperating; set => isOperating = value; }

    [Header("Training")]
    [SerializeField] private bool isTraining = false;
    public bool IsTraining
    {
        get { return isTraining; }
        set { isTraining = value; }
    }
    [SerializeField] private int trainingDuration = 0;
    public int TrainingDuration
    {
        get { return trainingDuration; }
        set { trainingDuration = value; }
    }

    public void InitializeStudent(){
        currentPHYStat = phyStat;
        currentINTStat = intStat;
        currentCOMStat = comStat;
        currentStamina = stamina;

        isAssign = false;
        isOperating = false;
        isTraining = false;
        trainingDuration = 0;

        collected = false;
        squadCollect = false;
    }

    public void UpdateTrainedStats(){
        currentPHYStat = trainedPHYStat;
        currentINTStat = trainedINTStat;
        currentCOMStat = trainedCOMStat;
        currentStamina = restedStamina;

        trainedPHYStat = 0;
        trainedINTStat = 0;
        trainedCOMStat = 0;
        restedStamina = 0;
    }
}
