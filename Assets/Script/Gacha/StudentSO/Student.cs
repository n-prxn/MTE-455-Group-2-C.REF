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
    public byte id;
    public new string name;
    [JsonIgnore] public Sprite portrait;
    public School school;
    [JsonIgnore] public string detail;
    public Rarity rarity;
    public bool collexted;

    //Status
    [JsonIgnore] public int phyStat;
    [JsonIgnore] public int intStat;
    [JsonIgnore] public int comStat;
    [JsonIgnore] public int stamina;

    [JsonIgnore]
    private float gachaRate;
    [JsonIgnore]
    public float GachaRate{
        get{return gachaRate;}
        set{gachaRate = value;}
    }
}
