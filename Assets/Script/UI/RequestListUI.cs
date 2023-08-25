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
    [SerializeField] GameObject inProgressParent;
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
        //GenerateRequestCard();
    }

    // Update is called once per frame
    void Update()
    {

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

        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
    }

    private void HandleCardSelection(RequestCardData data)
    {
        currentSelectedRequest = data;
        requestListDescription.SetDescription(data.RequestData);
        if (!data.RequestData.IsRead)
        {
            data.RequestData.IsRead = true;
        }
        currentSelectedRequest.HideNoticeSymbol();
    }

    public void AcceptRequest()
    {
        RequestManager.instance.CurrentRequest = currentSelectedRequest.RequestData;
        RequestManager.instance.CurrentRequest.ResetSquad();
        squadPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
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
