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

    [Header("Squad")]
    public List<Student> squad = new List<Student>();

    public void ResetSquad(){
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
