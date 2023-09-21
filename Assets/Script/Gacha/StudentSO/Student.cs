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

[CreateAssetMenu(fileName = "New Student", menuName = "Student/Student")]
public class Student : ScriptableObject
{
    #region Student
    [Header("Student Info")]
    public byte id;
    public new string name;
    [JsonIgnore] public Sprite portrait;
    //[JsonIgnore] public Sprite portraitBig;
    [JsonIgnore] public Sprite artwork;
    public string club;
    public School school;

    [Header("Skill")]
    public SkillSO skill;
    public string skillName;
    [TextArea] public string skillDescription;
    public Sprite skillIcon;
    [JsonIgnore][TextArea] public string detail;

    [Header("Gacha Info")]
    public Rarity rarity;
    private bool collected;
    public bool Collected
    {
        get { return collected; }
        set { collected = value; }
    }
    private bool squadCollect;
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

    [Header("Trained Stats")]
    private int trainedPHYStat;
    public int TrainedPHYStat
    {
        get { return trainedPHYStat; }
        set { trainedPHYStat = value; }
    }

    private int trainedINTStat;
    public int TrainedINTStat
    {
        get { return trainedINTStat; }
        set { trainedINTStat = value; }
    }

    private int trainedCOMStat;
    public int TrainedCOMStat
    {
        get { return trainedCOMStat; }
        set { trainedCOMStat = value; }
    }

    private int restedStamina;
    public int RestedStamina
    {
        get { return restedStamina; }
        set { restedStamina = value; }
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
    private bool isAssign = false;
    public bool IsAssign
    {
        get { return isAssign; }
        set { isAssign = value; }
    }
    private bool isOperating = false;
    public bool IsOperating { get => isOperating; set => isOperating = value; }

    [Header("Training")]
    private bool isTraining = false;
    public bool IsTraining
    {
        get { return isTraining; }
        set { isTraining = value; }
    }
    private int trainingDuration = 0;
    public int TrainingDuration
    {
        get { return trainingDuration; }
        set { trainingDuration = value; }
    }

    [Header("Item Effect")]
    private bool isBuff = false;
    public bool IsBuff { get => isBuff; set => isBuff = value; }
    [SerializeField] private int buffDuration = 0;
    public int BuffDuration { get => buffDuration; set => buffDuration = value; }
    private int tempPHYStat = 0;
    private int tempINTStat = 0;
    private int tempCOMStat = 0;
    public int TempPHYStat { get => tempPHYStat; set => tempPHYStat = value; }
    public int TempINTStat { get => tempINTStat; set => tempINTStat = value; }
    public int TempCOMStat { get => tempCOMStat; set => tempCOMStat = value; }

    [Header("Voice")]
    public AudioClip[] studentVoices;

    public void InitializeStudent()
    {
        currentPHYStat = phyStat;
        currentINTStat = intStat;
        currentCOMStat = comStat;
        currentStamina = stamina;

        tempPHYStat = phyStat;
        tempINTStat = intStat;
        tempCOMStat = comStat;

        isBuff = false;
        isAssign = false;
        isOperating = false;
        isTraining = false;
        trainingDuration = 0;

        collected = false;
        squadCollect = false;
    }

    public void UpdateTrainedStats()
    {
        currentPHYStat = trainedPHYStat;
        currentINTStat = trainedINTStat;
        currentCOMStat = trainedCOMStat;
        currentStamina = restedStamina;

        ResetTrainedStat();
    }

    public void BuffStudentStats(float phyMultiplier, float intMultiplier, float comMultiplier)
    {
        tempPHYStat = currentPHYStat;
        tempINTStat = currentINTStat;
        tempCOMStat = currentCOMStat;

        currentPHYStat += (int)(currentPHYStat * phyMultiplier);
        currentINTStat += (int)(currentINTStat * intMultiplier);
        currentCOMStat += (int)(currentCOMStat * comMultiplier);
    }

    public void SetStudentStatToNormal()
    {
        currentPHYStat = tempPHYStat;
        currentINTStat = tempINTStat;
        currentCOMStat = tempCOMStat;
    }

    public void ResetTrainedStat()
    {
        trainedPHYStat = currentPHYStat;
        trainedINTStat = currentINTStat;
        trainedCOMStat = currentCOMStat;
        restedStamina = currentStamina;
    }

    public int TotalCurrentStat()
    {
        return currentPHYStat + currentINTStat + currentCOMStat;
    }
    #endregion
}
