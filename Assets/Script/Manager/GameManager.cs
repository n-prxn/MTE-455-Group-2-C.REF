using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
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

    [Header("Schale Rank")]
    public int currentXP = 0;
    public int maxXP = 1000;
    public int rank = 1;

    [Header("Pools")]
    [SerializeField] RequestPool requestPool;
    [SerializeField] RequestListUI requestListUI;

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
        instance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
            }
        }
    }


    public void UpdateRequest()
    {
        if (RequestManager.instance.OperatingRequests.Count > 0)
        {
            foreach (RequestSO request in RequestManager.instance.OperatingRequests)
            {
                RequestProcess(request);
            }
        }

    }

    void CheckResult()
    {

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
                Debug.Log(request.name + " has finished! with " + request.SuccessRate + "%");
                //ReceiveRewards(request);
            }
            else
            {
                Debug.Log(request.name + " has failed! with " + request.SuccessRate + "%");
            }

            //request.ResetSquad();
            request.IsOperating = false;
            request.IsDone = true;
            RequestManager.instance.RemoveRequest(request);
            ;
        }
    }
}
