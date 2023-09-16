using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.PackageManager.Requests;

public class DataManager : MonoBehaviour
{
    private GameData gameData;
    private List<IData> dataObject;

    private FileHandler fileHandler;
    [SerializeField] GachaPool gachaPool;
    [SerializeField] RequestPool requestPool;
    public static DataManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

        foreach(RequestSO request in requestPool.RequestsPool)
            request.InitializeRequest();

        foreach(Student student in gachaPool.StudentsPool)
        {
            student.InitializeStudent();
            
            gameData.studentSquad.Clear();
            gameData.studentSquad.Add(student.id, student.SquadCollect);
            
            gameData.studentCollected.Clear();
            gameData.studentCollected.Add(student.id, student.Collected);
        }

        foreach(BuildingSO building in TrainingManager.instance.Buildings)
            building.InitializeBuilding();
    }

    //Load gameData from GameDataSave.json whene exists
    public void LoadGame()
    {
        this.gameData = fileHandler.Load();

        if (this.gameData == null)
        {
            //Debug.Log("No Data");
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
