using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class RequestPool : MonoBehaviour
{
    [Header("UI Panel")]
    [SerializeField] GameObject requestListPanel;
    [SerializeField] GameObject requestSquadPanel;

    [SerializeField] private List<RequestSO> requestsPool = new List<RequestSO>();

    string path = "";
    // Start is called before the first frame update

    void Awake()
    {
        RequestManager.instance.TodayRequests.Add(requestsPool[0]);
        GenerateRequests();
    }

    void Start()
    {
        ResetOperatedRequest();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateRequests()
    {
        for (int i = 0; i < GameManager.instance.requestPerTurn ; i++)
        {
            RequestSO request;
            do
            {
                request = requestsPool[Random.Range(0, 101)];
            } while (request.IsOperating);
            request.IsRead = false;
            RequestManager.instance.TodayRequests.Add(request);
        }
    }

    public void ResetOperatedRequest(){
        foreach(RequestSO request in requestsPool){
            request.IsOperating = false;
        }
    }
}
