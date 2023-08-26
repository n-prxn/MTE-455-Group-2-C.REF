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
    [SerializeField] RequestUI requestUI;
    [SerializeField] GameObject squadPanel;
    [SerializeField] GameObject inProgressParent;
    [SerializeField] GameObject completeParent;
    [SerializeField] GameObject contentParent;

    List<RequestCardData> requestCardDatas = new List<RequestCardData>();
    RequestCardData currentSelectedRequest;
    // Start is called before the first frame update
    void Start()
    {
        GenerateRequestCard();
    }

    void Awake()
    {
        contentParent.GetComponent<VerticalLayoutGroup>().spacing += 0.01f;
        //GenerateRequestCard();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.IsPlayable = false;
    }

    public void GenerateCompleteCard(RequestSO request)
    {
        GameObject completeCard = Instantiate(requestCardPrefab, completeParent.transform);
        RequestCardData requestCardData = completeCard.GetComponent<RequestCardData>();
        requestCardData.SetData(request);
        requestCardData.HideNoticeSymbol();
        requestCardData.OnCardClicked += HandleCardSelection;
    }

    public void GenerateRequestCard()
    {
        ResetList();
        foreach (RequestSO requestSO in RequestManager.instance.TodayRequests)
        {
            Transform parent;
            if (requestSO.IsOperating)
                parent = inProgressParent.transform;
            else
                parent = cardParent.transform;

            GameObject requestCard = Instantiate(requestCardPrefab, parent);
            RequestCardData requestCardData = requestCard.GetComponent<RequestCardData>();
            requestCardData.SetData(requestSO);
            requestCardData.OnCardClicked += HandleCardSelection;
            if (requestCardData.RequestData.IsRead)
                requestCardData.HideNoticeSymbol();
            requestCardDatas.Add(requestCardData);
        }

        if (inProgressParent.transform.childCount <= 1)
            inProgressParent.SetActive(false);
        else
            inProgressParent.SetActive(true);

        contentParent.GetComponent<VerticalLayoutGroup>().spacing -= 0.01f;
    }

    private void HandleCardSelection(RequestCardData data)
    {
        currentSelectedRequest = data;
        //requestListDescription.SetDescription(data.RequestData , RequestMode.Available);
        //Debug.Log(data.transform.parent.name);

        UpdateDescription(data);

        if (!data.RequestData.IsRead)
        {
            data.RequestData.IsRead = true;
        }
        currentSelectedRequest.HideNoticeSymbol();
    }

    public void UpdateDescription(RequestCardData data)
    {
        if (data.transform.parent.name == "GeneralList")
            requestListDescription.SetDescription(data.RequestData, RequestMode.Available);

        if (data.transform.parent.name == "OngoingList")
            requestListDescription.SetDescription(data.RequestData, RequestMode.InProgress);

        if (data.transform.parent.name == "SuccessList")
            requestListDescription.SetDescription(data.RequestData, RequestMode.WaitResult);
    }

    public void UpdateDescription(RequestSO request)
    {
        RequestCardData data = requestCardDatas.Find(x => x.RequestData.id == request.id);
        Debug.Log(data.transform.parent.name);
        if (data.transform.parent.name == "GeneralList")
            requestListDescription.SetDescription(request, RequestMode.Available);

        if (data.transform.parent.name == "OngoingList")
            requestListDescription.SetDescription(request, RequestMode.InProgress);

        if (data.transform.parent.name == "SuccessList")
            requestListDescription.SetDescription(request, RequestMode.WaitResult);
    }

    public void AcceptRequest()
    {
        RequestManager.instance.CurrentRequest = currentSelectedRequest.RequestData;
        RequestManager.instance.CurrentRequest.ResetSquad();
        squadPanel.SetActive(true);
        RequestManager.instance.UpdateRequest();
        gameObject.SetActive(false);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        GameManager.instance.IsPlayable = true;
    }

    public void ResetList()
    {
        foreach (Transform transform in cardParent.transform)
        {
            Destroy(transform.gameObject);
        }

        foreach (Transform transform in inProgressParent.transform)
        {
            if (transform.gameObject.name == "In Progress Header")
                continue;
            Destroy(transform.gameObject);
        }

        requestCardDatas.Clear();
    }
}
