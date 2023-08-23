using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequestListUI : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] GameObject requestCardPrefab;

    [Header("UI")]
    [SerializeField] GameObject cardParent;
    [SerializeField] RequestListDescription requestListDescription;
    [SerializeField] GameObject squadPanel;

    List<RequestCardData> requestCardDatas = new List<RequestCardData>();
    RequestCardData currentSelectedRequest;
    // Start is called before the first frame update
    void Start()
    {
        InitializeRequestCard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeRequestCard()
    {
        foreach (RequestSO requestSO in RequestManager.instance.TodayRequests)
        {
            GameObject requestCard = Instantiate(requestCardPrefab, cardParent.transform);
            RequestCardData requestCardData = requestCard.GetComponent<RequestCardData>();
            requestCardData.SetData(requestSO);
            requestCardData.OnCardClicked += HandleCardSelection;
            requestCardDatas.Add(requestCardData);
        }
    }

    private void HandleCardSelection(RequestCardData data)
    {
        currentSelectedRequest = data;
        requestListDescription.SetDescription(data.RequestData);
        if(!data.RequestData.IsRead){
            data.RequestData.IsRead = true;
        }
        currentSelectedRequest.HideNoticeSymbol();
    }

    public void AcceptRequest()
    {
        RequestManager.instance.CurrentRequest = currentSelectedRequest.RequestData;
        RequestManager.instance.CurrentRequest.ResetSquad();
        squadPanel.SetActive(true);
    }
}
