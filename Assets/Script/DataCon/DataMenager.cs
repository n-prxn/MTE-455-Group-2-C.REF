using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataMenager : MonoBehaviour
{
    private GameData gameData;
    private List<IData> dataObject;

    private FileHandler fileHandler;

    public static DataMenager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    //Call LoadGame() at start
    private void Start()
    {
        this.fileHandler = new FileHandler();
        this.dataObject = FindAllDataObject();
        LoadGame();
    }

    //Call Wheme gamedata is null
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    //Load gameData from GameDataSave.json whene exists
    public void LoadGame()
    {
        this.gameData = fileHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No Data");
            NewGame();
        }

        foreach (IData dataOBJ in dataObject)
        {
            dataOBJ.LoadData(gameData);
        }
    }

    //save All gameData from all script Have MonoBehaviour and IData interface
    public void SaveGame()
    {
        foreach (IData dataOBJ in dataObject)
        {
            dataOBJ.SaveData(ref gameData);
        }

        fileHandler.Save(gameData);
    }

    public void ClearColleted()
    {
        gameData.studentCollected.Clear();
        fileHandler.Save(gameData);
    }

    //call saveGame() on quit game
    private void OnApplicationQuit()
    {
        SaveGame();
    }


    //Make a list of All script Have MonoBehaviour and IData interface
    private List<IData> FindAllDataObject()
    {
        IEnumerable<IData> dataObject = FindObjectsOfType<MonoBehaviour>().OfType<IData>();
        return new List<IData>(dataObject);
    }

}
