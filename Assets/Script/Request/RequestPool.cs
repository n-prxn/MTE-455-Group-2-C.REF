using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RequestPool : MonoBehaviour
{
    [SerializeField] private List<RequestSO> requestsPool;
    public List<RequestSO> RequestsPool { get => requestsPool; set => requestsPool = value; }

    [SerializeField] private int cooldownEmergency = 20;
    private int emergencyTimeCount = 0;

    // Start is called before the first frame update
    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        //GenerateRequests();
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
                    request = RequestsPool[Random.Range(1, 100)];
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

    public void GenerateEmergencyRequest()
    {
        if (emergencyTimeCount >= cooldownEmergency)
        {
            int chance = GameManager.Instance.crimeRate <= 20 ? 0 : GameManager.Instance.crimeRate;
            if (Random.Range(1, 100) <= chance)
            {
                RequestSO request = RequestsPool[Random.Range(101, 105)];
                request.IsRead = false;
                request.IsShow = true;
                RequestManager.instance.TodayRequests.Add(request);

                RequestManager.instance.IsEmergency = true;
                emergencyTimeCount = 0;

                UIDisplay.instance.HasPlayEmergencyScreen = false;
            }
        }else{
            emergencyTimeCount++;
        }
    }

    public void DeleteExpireRequests()
    {
        for (int i = 0; i < RequestManager.instance.TodayRequests.Count; i++)
        {
            RequestSO request = RequestManager.instance.TodayRequests[i];
            if (request.ExpireCount >= request.availableDuration)
            {
                request.IsShow = false;
                RequestManager.instance.TodayRequests.RemoveAt(i);
            }
        }
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
        int rank = GameManager.Instance.rank;
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
