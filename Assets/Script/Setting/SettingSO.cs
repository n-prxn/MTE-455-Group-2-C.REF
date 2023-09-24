using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Setting", menuName = "Setting")]
public class SettingSO : ScriptableObject
{
    [Header("Graphic Setting")]
    public bool screenMode = true;
    public int screenResWidth = 1080, screenResLength = 1920;
    [Header("Audio Setting")]
    public float backgroundMusic = 100;
    public float voice = 100;
    public float soundEffect = 100;
    [Header("Save")]
    public bool hasSaveGame = false;
    [Header("Title Voice")]
    public bool isTitleVoicePlay = false;
    [Header("Guarantee Pull")]
    public bool isGuaranteePull = false;
}
