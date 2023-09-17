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
    public int happiness = 50;
    public int crimeRate = 50;
    public int rollCost = 0;
    [Header("Overall Stats")]
    public int successRequest = 0;
    public int failedRequest = 0;

    [Header("Schale Rank")]
    public int currentXP = 0;
    public int maxXP = 220;
    public int rank = 0;

    [Header("Pools")]
    [SerializeField] RequestPool requestPool;
    [SerializeField] RequestListUI requestListUI;

    [Header("Shop")]
    [SerializeField] FurnitureShopUI furnitureShopUI;
    [SerializeField] ItemSOShopUI presentShopUI;
    [SerializeField] ItemSOShopUI ticketShopUI;

    [Header("Scene")]
    public SceneManager sceneManager;

    private bool isPlayable = true;
    public bool IsPlayable
    {
        get { return isPlayable; }
        set { isPlayable = value; }
    }

    public static GameManager instance;

    // Start is called before the first frame update
    void Awake()
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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RankUp();
    }

    public void NextTurn()
    {
        if (isPlayable)
        {
            if (currentTurn < lastTurn)
            {
                currentTurn++;
                UpdateRequest();
                requestPool.DecreaseDays();
                requestPool.GenerateRequests();

                ShopManager.instance.GenerateShopItems();
                presentShopUI.InitializeItemSOShelf();
                furnitureShopUI.InitializeFurnitureShelf();

                TrainingProcess();
            }
        }
    }

    public void UpdateRequest()
    {
        if (RequestManager.instance.OperatingRequests.Count > 0)
            foreach (RequestSO request in RequestManager.instance.OperatingRequests)
                RequestProcess(request);
    }

    void RankUp()
    {
        if (currentXP >= maxXP)
        {
            rank++;
            if (rank > 10)
                rank = 10;

            pyroxenes += 1200;
            credits += 40000;

            ShopManager.instance.MaxItem++;
            RequestManager.instance.maxRequestCapacity++;

            if (rank == 3)
            {
                RequestManager.instance.requestPerTurn++;
            }
            else if (rank == 6)
            {
                RequestManager.instance.requestPerTurn++;
            }

            currentXP = currentXP - maxXP;
            //Add Item
        }
    }

    void RequestProcess(RequestSO request)
    {
        request.CurrentTurn--;
        if (request.CurrentTurn <= 0)
        {

            if (requestListUI.CompleteCardDatas.Find(x => x.RequestData.id == request.id) == null)
            {
                requestListUI.GenerateCompleteCard(request);
            }

            if (Random.Range(0, 100) <= request.SuccessRate)
            {
                request.IsSuccess = true;
                Debug.Log(request.name + " has finished! with " + request.SuccessRate + "%");
                successRequest++;
                //ReceiveRewards(request);
            }
            else
            {
                request.IsSuccess = false;
                Debug.Log(request.name + " has failed! with " + request.SuccessRate + "%");
                failedRequest++;
            }

            //request.ResetSquad();
            request.IsOperating = false;
            request.IsDone = true;
            RequestManager.instance.RemoveRequest(request);
        }
    }

    void TrainingProcess()
    {
        foreach (KeyValuePair<BuildingType, List<Student>> group in TrainingManager.instance.TrainingGroup)
        {
            List<Student> students = group.Value;
            for (int i = 0; i < group.Value.Count; i++)
            {
                if (students[i] == null)
                    continue;

                if (students[i].IsTraining)
                {
                    students[i].TrainingDuration--;
                }

                if (students[i].TrainingDuration <= 0)
                {
                    students[i].IsTraining = false;
                    students[i].UpdateTrainedStats();
                    students[i] = null;
                }
            }
        }
    }

    public void BackToPreviousScene()
    {
        sceneManager.LoadPreviousScene();
    }

    public void IncreaseXP(int xp)
    {
        currentXP += xp;
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void LoadData(GameData data)
    {
        currentTurn = data.currentTurn;
        credits = data.credits;
        pyroxenes = data.pyroxenes;
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
        data.happiness = happiness;
        data.crimeRate = crimeRate;
        data.rank = rank;
        data.currentXP = currentXP;
        data.successRequest = successRequest;
        data.failedRequest = failedRequest;
    }
}
