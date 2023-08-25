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
    public Sprite portrait;
    [TextArea] public new string description;
    public new string requesterName;
    public new Difficulty difficulty;
    public bool isRepeatable = false;
    public int duration = 1;
    public int availableDuration = 10;

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

    [Header("Squad")]
    public List<Student> squad = new List<Student>();

    [Header("Progress")]
    private bool isOperating = false;
    public bool IsOperating {
        get{ return isOperating; }
        set{ isOperating = value;}
    }
    private int successRate = 0;
    public int SuccessRate {
        get{ return successRate; }
        set{ successRate = value;}
    }

    private int currentTurn = 0;
    public int CurrentTurn{
        get{ return currentTurn; }
        set{ currentTurn = value;}
    }

    private int expiredCount = 0;
    public int ExpireCount{
        get{ return expiredCount; }
        set{ expiredCount = value;}
    }

    private bool isRead = false;
    public bool IsRead{
        get {return isRead; }
        set {isRead = value;}
    }

    private bool isDone = false;
    public bool IsDone{
        get{return isDone;}
        set {isDone = value;}
    }

    private bool isShow = false;
    public bool IsShow{
        get{ return isShow; }
        set {isShow = value;}
    }

    public void ResetSquad(){
        if(squad.Count == 0){
            for(int i = 0; i < 4 ; i++)
                squad.Add(null);
        }
        for(int i = 0; i < 4 ; i++){
            if(squad[i] != null)
                squad[i].IsAssign = false;
        }
        squad.Clear();
        for(int i = 0; i < 4 ; i++){
            squad.Add(null);
        }
    }

    public int TotalStat(){
        return phyStat + intStat + comStat;
    }
}
