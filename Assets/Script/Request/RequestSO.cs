using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public enum Difficulty
{
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
    public Sprite portrait;
    [TextArea] public string description;
    [TextArea] public string chatStatus;
    public string requesterName;
    public Difficulty difficulty;
    public bool isRepeatable = false;
    public int duration = 1;
    public int availableDuration = 10;
    public int demeritHappiness = 0;
    public int demeritCrimeRate = 0;

    [Header("Status Requirement")]
    public int phyStat;
    public int intStat;
    public int comStat;
    public int stamina;

    [Header("Multiplied Status")]
    public int multipliedPhyStat;
    public int multipliedIntStat;
    public int multipliedComStat;

    [Header("Rewards")]
    public int credit;
    public int xp;
    public int happiness;
    public int crimeRate;
    public int pyroxene;

    [Header("Current Rewards")]
    [SerializeField] private int currentCredit;
    [SerializeField] private int currentXP;
    [SerializeField] private int currentHappiness;
    [SerializeField] private int currentCrimeRate;
    [SerializeField] private int currentDemeritHappiness;
    [SerializeField] private int currentDemeritCrimeRate;
    public int CurrentCrimeRate { get => currentCrimeRate; set => currentCrimeRate = value; }
    public int CurrentHappiness { get => currentHappiness; set => currentHappiness = value; }
    public int CurrentXP { get => currentXP; set => currentXP = value; }
    public int CurrentCredit { get => currentCredit; set => currentCredit = value; }
    public int CurrentDemeritHappiness { get => currentDemeritHappiness; set => currentDemeritHappiness = value; }
    public int CurrentDemeritCrimeRate { get => currentDemeritCrimeRate; set => currentDemeritCrimeRate = value; }

    [Header("Squad")]
    public List<Student> squad = new List<Student>();

    [Header("Progress")]
    private bool isOperating = false;
    public bool IsOperating
    {
        get { return isOperating; }
        set { isOperating = value; }
    }
    private int successRate = 0;
    public int SuccessRate
    {
        get { return successRate; }
        set { successRate = value; }
    }

    private int currentTurn = 0;
    public int CurrentTurn
    {
        get { return currentTurn; }
        set { currentTurn = value; }
    }

    private int expiredCount = 0;
    public int ExpireCount
    {
        get { return expiredCount; }
        set { expiredCount = value; }
    }

    private bool isRead = false;
    public bool IsRead
    {
        get { return isRead; }
        set { isRead = value; }
    }

    private bool isDone = false;
    public bool IsDone
    {
        get { return isDone; }
        set { isDone = value; }
    }

    private bool isShow = false;
    public bool IsShow
    {
        get { return isShow; }
        set { isShow = value; }
    }

    private bool isSuccess = false;
    public bool IsSuccess
    {
        get { return isSuccess; }
        set { isSuccess = value; }
    }

    public void InitializeRequest()
    {
        squad.Clear();
        for (int i = 0; i < 4; i++)
            squad.Add(null);

        currentTurn = 0;
        expiredCount = 0;
        successRate = 0;
        isRead = false;
        isShow = false;
        isDone = false;
        isSuccess = false;
        isOperating = false;

        currentCredit = credit;
        currentXP = xp;
        currentHappiness = happiness;
        currentCrimeRate = crimeRate;
    }

    public void InitializeCurrentReward()
    {
        currentCredit = credit;
        currentXP = xp;
        currentHappiness = happiness;
        currentCrimeRate = crimeRate;

        currentDemeritCrimeRate = demeritCrimeRate;
        currentDemeritHappiness = demeritHappiness;
    }
    public void DecreaseStamina(int stamina)
    {
        for (int i = 0; i < 4; i++)
        {
            if (squad[i] != null)
                squad[i].CurrentStamina -= stamina;
        }
    }

    public void ResetSquad()
    {
        if (squad.Count == 0)
        {
            for (int i = 0; i < 4; i++)
                squad.Add(null);
        }
        for (int i = 0; i < 4; i++)
        {
            if (squad[i] != null)
            {
                squad[i].IsAssign = false;
                squad[i].IsOperating = false;
            }
        }
        squad.Clear();
        for (int i = 0; i < 4; i++)
        {
            squad.Add(null);
        }
    }

    public int SquadAmount(){
        int amount = 0;
        for(int i = 0 ; i < 4; i++){
            if(squad[i] != null)
                amount++;
        }
        return amount;
    }

    public int TotalStat()
    {
        return multipliedPhyStat + multipliedIntStat + multipliedComStat;
    }
}
