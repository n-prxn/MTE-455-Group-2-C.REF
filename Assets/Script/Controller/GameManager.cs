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
                currentTurn = currentTurn + 1;
                UpdateRequest();
                requestPool.DecreaseDays();
                requestPool.GenerateRequests();
            }
        }
    }


    public void UpdateRequest()
    {
        foreach (RequestSO request in RequestManager.instance.OperatingRequests)
        {
            Result(request);
        }
    }

    void Result(RequestSO request)
    {
        request.CurrentTurn--;
        if (request.CurrentTurn <= 0)
        {
            if (Random.Range(0, 100) <= request.SuccessRate)
            {
                Debug.Log(request.name + " has finished! with " + request.SuccessRate + "%");
                ReceiveRewards(request);
            }
            else
            {
                Debug.Log(request.name + " has failed! with " + request.SuccessRate + "%");
            }

            request.ResetSquad();
            request.CurrentTurn = request.duration;
            request.SuccessRate = 100;
            request.IsOperating = false;
            if (!request.isRepeatable)
                request.IsDone = true;
            RequestManager.instance.RemoveRequest(request);
            ;
        }
    }

    void ReceiveRewards(RequestSO request)
    {
        credits += request.credit;
        pyroxenes += request.pyroxene;
        currentXP += request.xp;
        happiness += request.happiness;
        crimeRate -= request.crimeRate;
    }
}
