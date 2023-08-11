using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMenager : MonoBehaviour
{
    private GameData gameData;

    public static DataMenager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        LaodGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LaodGame()
    {
        if (this.gameData != null)
        {
            Debug.Log("No Data");
            NewGame();
        }
    }

    public void SaveGame()
    {

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

}
