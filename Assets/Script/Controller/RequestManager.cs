using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    public static RequestManager instance;
    [SerializeField] private RequestSO currentRequest;
    private List<RequestSO> todayRequests = new List<RequestSO>();
    public List<RequestSO> TodayRequests
    {
        get { return todayRequests; }
        set { todayRequests = value; }
    }
    public RequestSO CurrentRequest
    {
        get { return currentRequest; }
        set { currentRequest = value; }
    }
    [SerializeField] private RequestUI requestUI;

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

    public void UpdateRequest()
    {
        requestUI.UpdateRequestInfo(currentRequest);
    }
}
