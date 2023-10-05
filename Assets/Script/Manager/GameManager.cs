using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour, IData
{
    [Header("Turn")]
    public int currentTurn = 1;
    public int lastTurn = 150;

    [Header("Resource Amount")]
    public int credits = 0;
    public int pyroxenes = 0;
    public int elephs = 0;
    public int happiness = 50;
    public int crimeRate = 50;
    public int rollCost = 0;
    public int elephsCost = 0;
    [Header("Overall Stats")]
    public int successRequest = 0;
    public int failedRequest = 0;

    [Header("Schale Rank")]
    public int currentXP = 0;
    public int maxXP = 220;
    public int rank = 0;
    private bool[] rankRewardCheck = new bool[10];

    [Header("Pools")]
    [SerializeField] RequestPool requestPool;
    [SerializeField] RequestListUI requestListUI;

    [Header("Shop")]
    [SerializeField] FurnitureShopUI furnitureShopUI;
    [SerializeField] ItemSOShopUI presentShopUI;
    [SerializeField] ItemSOShopUI ticketShopUI;

    [Header("Scene")]
    public SceneManager sceneManager;

    [Header("Setting SO")]
    public SettingSO setting;

    public bool uiIsOpen;

    [SerializeField] private bool isPlayable = true;
    public bool IsPlayable
    {
        get { return isPlayable; }
        set { isPlayable = value; }
    }

    private DataManager dataManager;

    public static GameManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        //Instance = this;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        GameObject.FindWithTag("Student Parent").GetComponent<StudentSpawner>().InitializeStudents();
    }

    private void OnEnable()
    {
        AudioSource audioSource = GameObject.FindGameObjectWithTag("Music Audio").GetComponent<AudioSource>();
        audioSource.loop = false;

        audioSource.Play();
        StartCoroutine(AudioFade.StartFade(audioSource, 2f, setting.backgroundMusic / 100f));
    }

    // Update is called once per frame
    void Update()
    {
        //RankUp();
    }

    public void NextTurn()
    {
        if (isPlayable)
        {
            if (currentTurn < lastTurn)
            {
                UIDisplay.instance.PlaySplashScreen();
                StartCoroutine(DelayGameProcess());
            }
        }
    }

    IEnumerator DelayGameProcess()
    {
        yield return new WaitForSeconds(1.5f);
        currentTurn++;
        happiness--;
        crimeRate++;
        UpdateRequest();
        requestPool.DecreaseDays();
        requestPool.GenerateRequests();

        ShopManager.instance.GenerateShopItems();
        presentShopUI.InitializeItemSOShelf();
        furnitureShopUI.InitializeFurnitureShelf();

        SquadController.instance.UpdateStudentBuff();

        TrainingManager.instance.UpdateStudentTraining();
        TrainingManager.instance.UpdateStudentResting();

        GameObject.FindWithTag("Student Parent").GetComponent<StudentSpawner>().InitializeStudents();
    }

    public void UpdateRequest()
    {
        if (RequestManager.instance.OperatingRequests.Count > 0)
        {
            List<RequestSO> requests = new List<RequestSO>();
            foreach (RequestSO request in RequestManager.instance.OperatingRequests)
            {
                request.CurrentTurn--;
                if (request.CurrentTurn <= 0)
                {
                    RequestProcess(request);
                    continue;
                }
                requests.Add(request);
            }

            RequestManager.instance.OperatingRequests.Clear();
            RequestManager.instance.OperatingRequests = requests;
        }
    }

    public void RankUp()
    {
        while (currentXP >= maxXP)
        {
            rank++;
            if (rank > 10)
                rank = 10;

            pyroxenes += 1200;
            credits += 40000;

            ShopManager.instance.MaxItem++; // Bug
            RequestManager.instance.maxRequestCapacity++;

            switch (rank)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    RequestManager.instance.requestPerTurn++;
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    RequestManager.instance.requestPerTurn++;
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
            }
            currentXP -= maxXP;
            //Add Item
        }
    }

    void RequestProcess(RequestSO request)
    {
        requestListUI.GenerateCompleteCard(request);

        if (Random.Range(0, 100) <= request.SuccessRate)
        {
            request.IsSuccess = true;
            Debug.Log(request.name + " has finished! with " + request.SuccessRate + "%");
            successRequest++;
        }
        else
        {
            request.IsSuccess = false;
            Debug.Log(request.name + " has failed! with " + request.SuccessRate + "%");
            failedRequest++;
        }
        request.IsOperating = false;
        request.IsDone = true;
    }

    /*void TrainingProcess()
    {
        foreach (KeyValuePair<BuildingType, List<Student>> group in TrainingManager.instance.TrainingGroup)
        {
            List<Student> students = group.Value;
            foreach (Student student in students.ToList())
            {
                if (student == null)
                    continue;

                if (student.IsTraining)
                {
                    student.TrainingDuration--;
                    if (student.TrainingDuration <= 0)
                    {
                        student.IsTraining = false;
                        student.UpdateTrainedStats();
                        TrainingManager.instance.RemoveAfterFinishTraining(student);
                    }
                }

                Debug.Log(student.name + " remains " + student.TrainingDuration + " Days");
            }
        }
    }*/

    public void BackToPreviousScene()
    {
        dataManager.SaveGame();
        sceneManager.LoadAsyncPreviousScene();
    }

    public void LoadScene(int buildIndex)
    {
        dataManager.SaveGame();
        if (buildIndex == 0)
            GameObject.Find("AudioController").GetComponent<AudioController>().PlayTitleMusic();
        sceneManager.LoadSceneAsync(buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        dataManager.SaveGame();
        if (sceneName == "Menu")
            GameObject.Find("AudioController").GetComponent<AudioController>().PlayTitleMusic();
        sceneManager.LoadSceneAsync(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void AddEXP(int EXP)
    {
        currentXP += EXP;
        RankUp();
    }
    public void LoadData(GameData data)
    {
        currentTurn = data.currentTurn;
        credits = data.credits;
        pyroxenes = data.pyroxenes;
        elephs = data.elephs;
        happiness = data.happiness;
        crimeRate = data.crimeRate;
        rank = data.rank;
        currentXP = data.currentXP;
        successRequest = data.successRequest;
        failedRequest = data.failedRequest;
    }

    public void SaveData(ref GameData data)
    {
        data.currentTurn = currentTurn;
        data.credits = credits;
        data.pyroxenes = pyroxenes;
        data.elephs = elephs;
        data.happiness = happiness;
        data.crimeRate = crimeRate;
        data.rank = rank;
        data.currentXP = currentXP;
        data.successRequest = successRequest;
        data.failedRequest = failedRequest;
        Debug.Log("Game Manager Saved " + data.pyroxenes);
    }
}
