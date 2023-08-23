using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestPool : MonoBehaviour
{
    [Header("UI Panel")]
    [SerializeField] GameObject requestListPanel;
    [SerializeField] GameObject requestSquadPanel;

    [SerializeField] private List<RequestSO> requestsPool = new List<RequestSO>();
    // Start is called before the first frame update

    void Awake()
    {
        for (int i = 0; i < GameManager.instance.requestPerTurn; i++)
        {
            RequestManager.instance.TodayRequests.Add(requestsPool[Random.Range(0, 101)]);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
