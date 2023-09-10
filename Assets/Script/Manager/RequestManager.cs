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
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
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
        foreach (Student student in currentRequest.squad)
        {
            if (student != null)
            {
                totalPHYStat += student.CurrentPHYStat;
                totalINTStat += student.CurrentINTStat;
                totalCOMStat += student.CurrentCOMStat;
            }
        }
    }

    public void ClearTotalStatus()
    {
        totalPHYStat = 0;
        totalINTStat = 0;
        totalCOMStat = 0;
    }

    public int CalculateSuccessRate()
    {
        float successRate = 100f;
        if (totalPHYStat < currentRequest.phyStat)
        {
            successRate -= 10f;
            successRate -= (currentRequest.phyStat - totalPHYStat) * 100f / currentRequest.TotalStat();
        }
        if (totalINTStat < currentRequest.intStat)
        {
            successRate -= 10f;
            successRate -= (currentRequest.intStat - totalINTStat) * 100f / currentRequest.TotalStat();
        }
        if (totalCOMStat < currentRequest.comStat)
        {
            successRate -= 10f;
            successRate -= (currentRequest.comStat - totalCOMStat) * 100f / currentRequest.TotalStat();
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
    }

    public void ClearSquad()
    {
        currentRequest.ResetSquad();
        ClearTotalStatus();
        UpdateRequest();
    }

    public void RemoveRequest(RequestSO request){
        RequestSO targetRequest = TodayRequests.Find(x => x.id == request.id);
        if(targetRequest != null){
            operatingRequests.Remove(targetRequest);
        }
    }

    public void SendSquad()
    {
        AddOperatingQuest();
        currentRequest.SuccessRate = CalculateSuccessRate();
        //currentRequest = null;
    }

    public bool isNotice(){
        foreach(RequestSO request in todayRequests){
            if(!request.IsRead)
                return true;
        }
        return false;
    }
}
