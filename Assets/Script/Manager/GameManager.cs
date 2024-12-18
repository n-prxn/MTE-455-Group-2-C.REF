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
    public int lowLevelMaxXP = 100;
    public int rank = 0;
    private bool[] rankRewardCheck = new bool[10];

    [Header("Pools")]
    [SerializeField] RequestPool requestPool;
    [SerializeField] RequestListUI requestListUI;

    [Header("Shop")]
    [SerializeField] FurnitureShopUI furnitureShopUI;
    [SerializeField] ItemSOShopUI presentShopUI;
    [SerializeField] ItemSOShopUI ticketShopUI;
    [Header("Tutorial")]
    [SerializeField] GameObject tutorial;

    [Header("Scene")]
    public SceneManager sceneManager;
    [Header("Reward Rank Up")]
    public GameObject rankUpNoticePanel;

    [Header("Setting SO")]
    public SettingSO setting;

    public bool uiIsOpen;

    [SerializeField] private bool isPlayable = true;
    public bool IsPlayable
    {
        get { return isPlayable; }
        set { isPlayable = value; }
    }

    [SerializeField] private TrainingUI trainingUI;
    private DataManager dataManager;
    private bool hasButtonClicked = false;
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
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        GameObject.FindWithTag("Student Parent").GetComponent<StudentSpawner>().InitializeStudents();
        AudioSource audioSource = GameObject.FindGameObjectWithTag("Music Audio").GetComponent<AudioSource>();
        audioSource.loop = false;

        audioSource.Play();
        StartCoroutine(AudioFade.StartFade(audioSource, 2f, setting.backgroundMusic / 100f));

        if (!setting.hasPlayTutorial)
        {
            tutorial.SetActive(true);
            // setting.hasPlayTutorial = true;
        }
        else
            tutorial.SetActive(false);
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
                trainingUI.OnNextTurn();
                StartCoroutine(DelayGameProcess());
            }
            else
            {
                ShowEnding();
            }
        }
    }

    IEnumerator DelayGameProcess()
    {
        yield return new WaitForSeconds(1.5f);
        currentTurn++;

        if (currentTurn % 3 == 0)
        {
            happiness--;
            crimeRate++;
        }

        UpdateRequest();

        if (RequestManager.instance.IsEmergency)
        {
            happiness--;
            crimeRate++;
        }
        else
        {
            requestPool.DecreaseDays();
            requestPool.GenerateRequests();
            requestPool.GenerateEmergencyRequest();
        }

        if (happiness < 0)
            happiness = 0;
        if (happiness > 100)
            happiness = 100;

        if (crimeRate < 0)
            crimeRate = 0;
        if (crimeRate > 100)
            crimeRate = 100;

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
                if (!request.IsOperating)
                {
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
        while (currentXP >= (rank < 4 ? lowLevelMaxXP : maxXP))
        {
            rank++;

            pyroxenes += 1200;
            credits += 40000;

            //Give Item
            ItemSO present = ShopManager.instance.PresentWarehouse[Random.Range(0, ShopManager.instance.PresentWarehouse.Count)];
            InventoryManager.instance.AddItemToInventory(present);

            if (rank > 10)
            {
                rank = 10;
                currentXP -= maxXP;
                UIDisplay.instance.GenerateLevelUpNotification(present);
                break;
            }

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
            if (rank < 4)
                currentXP -= lowLevelMaxXP;
            else
                currentXP -= maxXP;

            UIDisplay.instance.GenerateLevelUpNotification(present);
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

    public void BackToPreviousScene()
    {
        if (hasButtonClicked)
            return;

        hasButtonClicked = true;
        dataManager.SaveGame();
        sceneManager.LoadAsyncPreviousScene();
        StartCoroutine(WaitForAnotherClick());
    }

    IEnumerator WaitForAnotherClick()
    {
        yield return new WaitForSeconds(1);
        hasButtonClicked = false;
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

    public void ShowEnding()
    {
        int endingResult = (100 - happiness) + crimeRate;
        if (endingResult >= 150)
            sceneManager.LoadScene("Bad Ending");
        else if (endingResult <= 50)
            sceneManager.LoadScene("Good Ending");
        else
            sceneManager.LoadScene("True Ending");

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
