using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class RequestManager : MonoBehaviour
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
    [SerializeField] private RequestPool requestPool;

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
        operatingRequests.Clear();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

        return successRate < 0 ? 0 : (int)successRate;
    }

    public void UpdateRequest()
    {
        requestUI.UpdateRequestInfo(currentRequest);
    }

    public void AddOperatingQuest()
    {
        operatingRequests.Add(currentRequest);
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
        RequestSO targetRequest = TodayRequests.Find(x => x.id == request.id);
        if (targetRequest != null)
        {
            operatingRequests.Remove(targetRequest);
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
}
