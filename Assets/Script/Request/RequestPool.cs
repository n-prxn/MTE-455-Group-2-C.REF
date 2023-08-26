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
    [SerializeField] private List<RequestSO> requestsPool = new List<RequestSO>();

    string path = "";
    // Start is called before the first frame update

    void Awake()
    {

    }

    void Start()
    {
        ResetOperatedRequest();
        RequestManager.instance.TodayRequests.Add(requestsPool[0]);
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
                    request = requestsPool[Random.Range(0, 101)];
                } while (request.IsOperating || (request.IsDone && !request.isRepeatable) || request.IsShow);
                request.IsRead = false;
                request.IsShow = true;
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
                Debug.Log(request.name + " expired!");
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
        foreach (RequestSO request in requestsPool)
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
}
