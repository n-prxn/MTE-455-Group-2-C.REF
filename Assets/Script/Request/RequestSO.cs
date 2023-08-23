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

    [Header("Squad")]
    public List<Student> squad = new List<Student>();

    private bool isRead;
    public bool IsRead{
        get {return isRead; }
        set {isRead = value;}
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
}
