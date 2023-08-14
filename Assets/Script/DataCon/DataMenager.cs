using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataMenager : MonoBehaviour
{
    private GameData gameData;
    private List<IData> dataObject;

    public static DataMenager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        this.dataObject = FindAllDataObject();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        if (this.gameData == null)
        {
            Debug.Log("No Data");
            NewGame();
        }

        foreach (IData dataOBJ in dataObject)
        {
            dataOBJ.LoadData(gameData);
        }

        Debug.Log("Roll count on Load " + gameData.rollCount);
    }

    public void SaveGame()
    {
        foreach (IData dataOBJ in dataObject)
        {
            dataOBJ.SaveData(ref gameData);
        }

        Debug.Log("Roll count on Save " + gameData.rollCount);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }


    private List<IData> FindAllDataObject()
    {
        IEnumerable<IData> dataObject = FindObjectsOfType<MonoBehaviour>().OfType<IData>();
        return new List<IData>(dataObject);
    }

}
