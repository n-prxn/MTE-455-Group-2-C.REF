using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Sprite portrait;
    public School school;
    public string detail;
    public Rarity rarity;

    //Status
    public int phyStat;
    public int intStat;
    public int comStat;
    public int stamina;

    private float gachaRate;
    public float GachaRate{
        get{return gachaRate;}
        set{gachaRate = value;}
    }
}
