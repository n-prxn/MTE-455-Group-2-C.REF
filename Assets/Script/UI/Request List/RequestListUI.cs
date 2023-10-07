using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequestListUI : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] GameObject requestCardPrefab;

    [Header("Panel")]
    [SerializeField] GameObject idlePanel;
    [SerializeField] GameObject requestPanel;
    [SerializeField] RequestUI squadPanel;

    [Header("Script")]
    [SerializeField] RequestListDescription requestListDescription;
    [SerializeField] ResultUI resultUI;

    [Header("Parent")]
    [SerializeField] GameObject cardParent;
    [SerializeField] GameObject inProgressParent;
    [SerializeField] GameObject completeParent;
    public GameObject CompleteParent { get => completeParent; set => completeParent = value; }
    [SerializeField] GameObject contentParent;

    List<RequestCardData> requestCardDatas = new List<RequestCardData>();
    List<RequestCardData> completeCardDatas = new List<RequestCardData>();
    public List<RequestCardData> CompleteCardDatas
    {
        get { return completeCardDatas; }
        set { completeCardDatas = value; }
    }

    RequestCardData currentSelectedRequest;
    // Start is called before the first frame update
    void Start()
    {
        //GenerateRequestCard();
    }

    void Awake()
    {
        ShowIdleWindow();
        contentParent.GetComponent<VerticalLayoutGroup>().spacing += 0.01f;
        //GenerateRequestCard();
    }

    void OnEnable()
    {
        GenerateRequestCard();
        ShowIdleWindow();
    }

    // Update is called once per frame
    void Update()
    {
        //GameManager.instance.IsPlayable = false;
    }

    public void GenerateCompleteCard(RequestSO request)
    {
        GameObject completeCard = Instantiate(requestCardPrefab, completeParent.transform);
        RequestCardData requestCardData = completeCard.GetComponent<RequestCardData>();

        requestCardData.SetData(request);
        requestCardData.HideNoticeSymbol();
        requestCardData.OnCardClicked += HandleCardSelection;

        //completeCardDatas.Add(requestCardData);
    }

    public void GenerateRequestCard()
    {
        ResetList();
        foreach (RequestSO requestSO in RequestManager.instance.TodayRequests)
        {
            GameObject requestCard = Instantiate(requestCardPrefab, cardParent.transform);
            RequestCardData requestCardData = requestCard.GetComponent<RequestCardData>();
            requestCardData.SetData(requestSO);
            requestCardData.OnCardClicked += HandleCardSelection;
            if (requestCardData.RequestData.IsRead)
                requestCardData.HideNoticeSymbol();
            requestCardDatas.Add(requestCardData);
        }

        foreach (RequestSO requestSO in RequestManager.instance.OperatingRequests)
        {
            GameObject requestCard = Instantiate(requestCardPrefab, inProgressParent.transform);
            RequestCardData requestCardData = requestCard.GetComponent<RequestCardData>();
            requestCardData.SetData(requestSO);
            requestCardData.OnCardClicked += HandleCardSelection;
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
        ShowDescriptionWindow();
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
        {
            if (data.RequestData.difficulty == Difficulty.Emergency)
            {
                requestListDescription.SetDescription(data.RequestData, RequestMode.Emergency);
            }
            else
            {
                requestListDescription.SetDescription(data.RequestData, RequestMode.Available);
            }
        }

        if (data.transform.parent.name == "OngoingList")
            requestListDescription.SetDescription(data.RequestData, RequestMode.InProgress);

        if (data.transform.parent.name == "SuccessList")
            requestListDescription.SetDescription(data.RequestData, RequestMode.WaitResult);
    }

    public void UpdateDescription(RequestSO request)
    {
        RequestCardData data = requestCardDatas.Find(x => x.RequestData.id == request.id);
        if (data.transform.parent.name == "GeneralList")
        {
            if (RequestManager.instance.IsEmergency)
            {
                if (request.difficulty == Difficulty.Emergency)
                {
                    requestListDescription.SetDescription(request, RequestMode.Emergency);
                }
                else
                {
                    requestListDescription.SetDescription(request, RequestMode.Available);
                }
            }
            else
                requestListDescription.SetDescription(request, RequestMode.Available);
        }

        if (data.transform.parent.name == "OngoingList")
            requestListDescription.SetDescription(request, RequestMode.InProgress);

        if (data.transform.parent.name == "SuccessList")
            requestListDescription.SetDescription(request, RequestMode.WaitResult);
    }

    public void AcceptRequest()
    {
        RequestManager.instance.CurrentRequest = currentSelectedRequest.RequestData;
        RequestManager.instance.CurrentRequest.ResetSquad();
        squadPanel.gameObject.SetActive(true);
        RequestManager.instance.ClearTotalStatus();
        RequestManager.instance.UpdateRequest();
        gameObject.SetActive(false);
    }

    public void ShowResult()
    {
        RequestManager.instance.CurrentRequest = currentSelectedRequest.RequestData;
        resultUI.gameObject.SetActive(true);
        DeleteCompleteRequest();
        gameObject.SetActive(false);
    }

    public void DeleteCompleteRequest()
    {
        foreach (Transform requestCard in completeParent.transform)
        {
            if (requestCard.GetComponent<RequestCardData>() != null)
            {
                if (requestCard.gameObject.GetComponent<RequestCardData>().RequestData.id == currentSelectedRequest.RequestData.id)
                {
                    requestCard.gameObject.GetComponent<RequestCardData>().RequestData.IsShow = false;
                    Destroy(requestCard.gameObject);
                }
            }
        }
    }

    public void ResetCompleteList()
    {
        foreach (Transform transform in completeParent.transform)
        {
            if (transform.GetComponent<RequestCardData>() == null)
                continue;
            Destroy(transform.gameObject);
        }
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

    public void ShowIdleWindow()
    {
        idlePanel.SetActive(true);
        requestPanel.SetActive(false);
    }

    public void ShowDescriptionWindow()
    {
        idlePanel.SetActive(false);
        requestPanel.SetActive(true);
    }

    public void ToggleTutorialRequest()
    {
        HandleCardSelection(requestCardDatas[0]);
    }
}
