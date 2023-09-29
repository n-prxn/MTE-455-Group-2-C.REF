using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;
using UnityEditor.Playables;
using UnityEditor;

public class DataManager : MonoBehaviour
{
    private GameData gameData;
    private List<IData> dataObject;

    private FileHandler fileHandler;
    [SerializeField] List<Student> studentPool;
    [SerializeField] List<RequestSO> requestPool;
    [SerializeField] List<BuildingSO> buildings;
    [SerializeField] SettingSO setting;
    public static DataManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //Call LoadGame() at start
    private void Start()
    {
        this.fileHandler = new FileHandler();

        /*this.dataObject = FindAllDataObject();
        LoadGame();*/
    }

    private void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Menu")
        {
            if (GameObject.FindGameObjectsWithTag("Gameplay Elements") != null)
            {
                foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("Gameplay Elements")){
                    Destroy(gameObject);
                }
            }
        }
    }

    public void InitializeGame()
    {
        //this.fileHandler = new FileHandler();
        this.dataObject = FindAllDataObject();
        LoadGame();
    }

    public void StartNewGame()
    {
        NewGame();
    }

    //Call Wheme gamedata is null
    public void NewGame()
    {
        this.gameData = new GameData();

        foreach (RequestSO request in requestPool)
        {
            request.InitializeRequest();
            gameData.requests.Add(new RequestData(request));
        }

        foreach (Student student in studentPool)
        {
            student.InitializeStudent();
            gameData.students.Add(new StudentData(student));
        }

        foreach (BuildingSO building in buildings)
            building.InitializeBuilding();

        setting.isGuaranteePull = false;
        //Debug.Log("New Game");
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
        if (dataObject != null)
        {
            foreach (IData dataOBJ in dataObject)
            {
                dataOBJ.SaveData(ref gameData);
            }
        }

        fileHandler.Save(gameData);
        #if UNITY_EDITOR
        EditorUtility.SetDirty(setting);
        #endif
        //Debug.Log("Data Manager Save");
    }

    public void ClearColleted()
    {
        foreach (StudentData student in gameData.students)
        {
            student.collected = false;
        }
        fileHandler.Save(gameData);
    }

    //call saveGame() on quit game
    private void OnApplicationQuit()
    {
        setting.isTitleVoicePlay = false;
        SaveGame();
    }


    //Make a list of All script Have MonoBehaviour and IData interface
    private List<IData> FindAllDataObject()
    {
        IEnumerable<IData> dataObject = FindObjectsOfType<MonoBehaviour>().OfType<IData>();
        return new List<IData>(dataObject);
    }

}
