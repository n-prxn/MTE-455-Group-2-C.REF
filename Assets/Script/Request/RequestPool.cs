using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.VersionControl;
using UnityEngine;

public class RequestPool : MonoBehaviour
{
    [SerializeField] private List<RequestSO> requestsPool;
    public List<RequestSO> RequestsPool { get => requestsPool; set => requestsPool = value; }

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        ResetOperatedRequest();
        RequestManager.instance.TodayRequests.Add(RequestsPool[0]);
        GenerateRequests();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateRequests()
    {
        DeleteExpireRequests();
        for (int i = 0; i < RequestManager.instance.requestPerTurn; i++)
        {
            if (RequestManager.instance.TodayRequests.Count < RequestManager.instance.maxRequestCapacity)
            {
                RequestSO request;
                do
                {
                    request = RequestsPool[Random.Range(0, 101)];
                } while (request.IsOperating || (request.IsDone && !request.isRepeatable) || request.IsShow || !IsUnlockedDifficulty(request));

                request.IsRead = false;
                request.IsShow = true;

                if (request.id != 0)
                {
                    switch (request.difficulty)
                    {
                        case Difficulty.Easy:
                            request.credit = Random.Range(5000, 10000);
                            break;
                        case Difficulty.Hardcore:
                            request.credit = Random.Range(30000, 50000);
                            break;
                        case Difficulty.Extreme:
                            request.credit = Random.Range(50000, 100000);
                            break;
                        case Difficulty.Insane:
                            request.credit = Random.Range(100000, 200000);
                            break;
                    }
                }

                RequestManager.instance.TodayRequests.Add(request);
            }
        }
    }

    public void DeleteExpireRequests()
    {
        List<RequestSO> requestList = new List<RequestSO>();
        for (int i = 0; i < RequestManager.instance.TodayRequests.Count; i++)
        {
            RequestSO request = RequestManager.instance.TodayRequests[i];
            if (request.ExpireCount >= request.availableDuration)
            {
                //Debug.Log(request.name + " expired!");
                request.IsDone = true;
                continue;
            }
            requestList.Add(request);
        }
        RequestManager.instance.TodayRequests = requestList;
        //Debug.Log(RequestManager.instance.TodayRequests);
    }

    public void ResetOperatedRequest()
    {
        foreach (RequestSO request in RequestsPool)
        {
            request.ResetSquad();
            request.IsOperating = false;
            request.IsShow = false;
            request.IsDone = false;
            request.ExpireCount = 0;
            request.CurrentTurn = 0;
        }
    }

    public void DecreaseDays()
    {
        foreach (RequestSO request in RequestManager.instance.TodayRequests)
        {
            request.ExpireCount++;
        }
    }

    public bool IsUnlockedDifficulty(RequestSO request)
    {
        int rank = GameManager.instance.rank;
        if (rank >= 5)
            return true;
        else if (rank >= 4)
        {
            switch (request.difficulty)
            {
                case Difficulty.Easy: return true;
                case Difficulty.Hardcore: return true;
                case Difficulty.Extreme: return true;
                case Difficulty.Insane: return false;
            }
        }
        else if (rank >= 3)
        {
            switch (request.difficulty)
            {
                case Difficulty.Easy: return true;
                case Difficulty.Hardcore: return true;
                case Difficulty.Extreme: return false;
                case Difficulty.Insane: return false;
            }
        }
        else
        {
            switch (request.difficulty)
            {
                case Difficulty.Easy: return true;
                case Difficulty.Hardcore: return false;
                case Difficulty.Extreme: return false;
                case Difficulty.Insane: return false;
            }
        }

        return false;
    }
}
