using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager : MonoBehaviour, IData
{
    [Header("Capacity")]
    public int requestPerTurn = 1;
    public int maxRequestCapacity = 3;
    public static RequestManager instance;
    [SerializeField] private List<RequestSO> todayRequests = new List<RequestSO>();
    public List<RequestSO> TodayRequests
    {
        get { return todayRequests; }
        set { todayRequests = value; }
    }

    [SerializeField] private RequestSO currentRequest;
    public RequestSO CurrentRequest
    {
        get { return currentRequest; }
        set { currentRequest = value; }
    }

    [SerializeField] private List<RequestSO> operatingRequests = new List<RequestSO>();
    public List<RequestSO> OperatingRequests
    {
        get { return operatingRequests; }
        set { operatingRequests = value; }
    }

    [SerializeField] private RequestUI requestUI;
    [SerializeField] private RequestListUI requestListUI;
    [SerializeField] private RequestPool requestPool;
    [SerializeField] private GachaPool gachaPool;

    private int totalPHYStat = 0;
    public int TotalPHYStat
    {
        get { return totalPHYStat; }
        set { totalPHYStat = value; }
    }
    private int totalINTStat = 0;
    public int TotalINTStat
    {
        get { return totalINTStat; }
        set { totalINTStat = value; }
    }
    private int totalCOMStat = 0;
    public int TotalCOMStat
    {
        get { return totalCOMStat; }
        set { totalCOMStat = value; }
    }

    private int staminaComsumption = 0;
    public int StaminaComsumption { get => staminaComsumption; set => staminaComsumption = value; }
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
        //operatingRequests.Clear();
    }

    public void InitializeRequest()
    {
        TodayRequests.Add(requestPool.RequestsPool[0]);
        requestPool.GenerateRequests();
    }

    public void Calculate()
    {
        ClearTotalStatus();
        currentRequest.InitializeCurrentReward();
        staminaComsumption = currentRequest.stamina;
        foreach (Student student in currentRequest.squad)
        {
            if (student != null)
            {
                totalPHYStat += student.CurrentPHYStat;
                totalINTStat += student.CurrentINTStat;
                totalCOMStat += student.CurrentCOMStat;

                if (student.skill != null)
                    student.skill.PerformSkill(student);
            }
        }
    }

    public void ClearTotalStatus()
    {
        totalPHYStat = 0;
        totalINTStat = 0;
        totalCOMStat = 0;
        staminaComsumption = 0;
    }

    public void AddAdditionalStatus(int phyStat, int intStat, int comStat)
    {
        totalPHYStat += phyStat;
        totalINTStat += intStat;
        totalCOMStat += comStat;
    }

    public int CalculateSuccessRate()
    {
        float successRate = 100f;
        if (totalPHYStat < currentRequest.multipliedPhyStat)
        {
            successRate -= 10f;
            successRate -= (currentRequest.multipliedPhyStat - totalPHYStat) * 100f / currentRequest.TotalStat();
        }
        if (totalINTStat < currentRequest.multipliedIntStat)
        {
            successRate -= 10f;
            successRate -= (currentRequest.multipliedIntStat - totalINTStat) * 100f / currentRequest.TotalStat();
        }
        if (totalCOMStat < currentRequest.multipliedComStat)
        {
            successRate -= 10f;
            successRate -= (currentRequest.multipliedComStat - totalCOMStat) * 100f / currentRequest.TotalStat();
        }

        successRate += currentRequest.BonusSuccessRate;
        if (successRate > 100)
            successRate = 100;
        if (successRate < 0)
            successRate = 0;
        return (int)successRate;
    }

    public void UpdateRequest()
    {
        requestUI.UpdateRequestInfo(currentRequest);
    }

    public void AddOperatingQuest()
    {
        operatingRequests.Add(currentRequest);
        todayRequests.Remove(currentRequest);
        currentRequest.IsOperating = true;
        foreach (Student student in currentRequest.squad)
        {
            if (student != null)
            {
                student.IsOperating = true;
                student.CurrentStamina -= staminaComsumption;
            }
        }
    }

    public void ClearSquad()
    {
        currentRequest.ResetSquad();
        ClearTotalStatus();
        UpdateRequest();
    }

    public void RemoveRequest(RequestSO request)
    {
        foreach (RequestSO requestSO in OperatingRequests)
        {
            if (requestSO.id == request.id)
            {
                operatingRequests.Remove(requestSO);
                break;
            }
        }
    }

    public void SendSquad()
    {
        AddOperatingQuest();
        currentRequest.SuccessRate = CalculateSuccessRate();
        //currentRequest = null;
    }

    public bool isNotice()
    {
        foreach (RequestSO request in todayRequests)
        {
            if (!request.IsRead)
                return true;
        }
        return false;
    }

    public void LoadData(GameData data)
    {
        maxRequestCapacity = data.maxRequestCapacity;
        requestPerTurn = data.requestPerTurn;

        todayRequests.Clear();
        OperatingRequests.Clear();
        foreach (RequestData rData in data.requests)
        {
            RequestSO request = requestPool.RequestsPool.Find(x => x.id == rData.id);
            request.CurrentCredit = rData.currentCredit;
            request.CurrentXP = rData.currentXP;
            request.CurrentHappiness = rData.currentHappiness;
            request.CurrentCrimeRate = rData.currentCrimeRate;

            request.CurrentDemeritHappiness = rData.currentDemeritHappiness;
            request.CurrentDemeritCrimeRate = rData.currentDemeritCrimeRate;

            request.IsOperating = rData.isOperating;
            request.IsRead = rData.isRead;
            request.IsDone = rData.isDone;
            request.IsShow = rData.isShow;
            request.IsSuccess = rData.isSuccess;

            request.SuccessRate = rData.successRate;
            request.CurrentTurn = rData.currentTurn;
            request.ExpireCount = rData.expiredCount;

            //Debug.Log(request.name);
            for (int i = 0; i < rData.squad.Length; i++)
            {
                //Debug.Log(i);
                if (rData.squad[i] == -1)
                {
                    request.squad[i] = null;
                }
                else
                {
                    request.squad[i] = gachaPool.StudentsPool.Find(x => x.id == rData.squad[i]);
                }
            }

            //Debug.Log(request.name);
            if (request.IsOperating)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (request.squad[i] == null)
                        continue;

                    request.squad[i].IsOperating = true;
                }
                operatingRequests.Add(request);
                //Debug.Log(request.name + "operated");
            }

            if (request.IsShow && !request.IsOperating)
                todayRequests.Add(request);

            if (request.IsDone && !request.IsOperating && request.SquadAmount() > 0)
                requestListUI.GenerateCompleteCard(request);
        }
        requestListUI.GenerateRequestCard();
    }

    public void SaveData(ref GameData data)
    {
        data.requestPerTurn = requestPerTurn;
        data.maxRequestCapacity = maxRequestCapacity;
        data.requests = new List<RequestData>();
        foreach (RequestSO request in requestPool.RequestsPool)
        {
            data.requests.Add(new RequestData(request));
        }
    }
}
