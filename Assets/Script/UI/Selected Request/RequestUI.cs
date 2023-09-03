using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.PackageManager.Requests;

public class RequestUI : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] GameObject slotParent;
    [SerializeField] GameObject slotPrefab;

    [Header("UI")]
    [SerializeField] Image currentPHYBar;
    [SerializeField] Image currentINTBar;
    [SerializeField] Image currentCOMBar;

    [SerializeField] Image PHYReqBar;
    [SerializeField] Image INTReqBar;
    [SerializeField] Image COMReqBar;

    [SerializeField] StudentSelectionUI selectionPanel;
    [SerializeField] RequestListUI requestListPanel;

    [Header("Detail UI")]
    [SerializeField] TextMeshProUGUI requestText;
    [SerializeField] TextMeshProUGUI requesterText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI creditText;
    [SerializeField] TextMeshProUGUI pyroxeneText;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI happinessText;
    [SerializeField] TextMeshProUGUI crimeRateText;
    [SerializeField] TextMeshProUGUI successRate;

    List<SquadSlotData> squadSlots = new List<SquadSlotData>();

    //[SerializeField] private RequestSO currentRequest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        InitializeSquad();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.IsPlayable = false;
        //UpdateRequestInfo();
    }

    public void InitializeSquad()
    {
        RequestManager.instance.CurrentRequest.ResetSquad();
        RequestManager.instance.ClearTotalStatus();
        for (int i = 0; i < 4; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent.transform);
            SquadSlotData squadSlot = slot.GetComponent<SquadSlotData>();
            squadSlot.SetData(i, "Assign", null);
            squadSlot.OnSlotClicked += HandleSlotSelection;
            squadSlot.ShowBlankSlot();
            squadSlots.Add(squadSlot);
        }
    }
    public void UpdateRequestInfo(RequestSO request)
    {
        request.CurrentTurn = request.duration;
        UpdateRequestRequirement(request);
        UpdateSquadUI(request);
        UpdateCurrentStat(RequestManager.instance.TotalPHYStat, RequestManager.instance.TotalINTStat, RequestManager.instance.TotalCOMStat);
        UpdateDescription(request);
    }

    void UpdateDescription(RequestSO request)
    {
        requestText.text = request.name;
        requesterText.text = request.requesterName;
        descriptionText.text = request.description;
        creditText.text = request.credit.ToString();
        pyroxeneText.text = request.pyroxene.ToString();
        expText.text = request.xp.ToString();
        happinessText.text = request.happiness.ToString();
        crimeRateText.text = request.crimeRate.ToString();
    }

    void UpdateSquadUI(RequestSO request)
    {
        for (int i = 0; i < 4; i++)
        {
            Student stdInSquad = RequestManager.instance.CurrentRequest.squad[i];
            if (stdInSquad == null)
            {
                squadSlots[i].ShowBlankSlot();
                squadSlots[i].Student = null;
            }
            else
            {
                squadSlots[i].SetData(i, stdInSquad);
                squadSlots[i].ShowStudent();
            }
        }
    }

    void UpdateRequestRequirement(RequestSO request)
    {
        PHYReqBar.rectTransform.localPosition = new Vector2((float)request.phyStat / 300f * 485f, PHYReqBar.rectTransform.localPosition.y);
        INTReqBar.rectTransform.localPosition = new Vector2((float)request.intStat / 300f * 485f, INTReqBar.rectTransform.localPosition.y);
        COMReqBar.rectTransform.localPosition = new Vector2((float)request.comStat / 300f * 485f, COMReqBar.rectTransform.localPosition.y);
        successRate.text = "Success Rate : " + RequestManager.instance.CalculateSuccessRate() + "%";
    }

    void UpdateCurrentStat(int PHYStat, int INTStat, int COMStat)
    {
        currentPHYBar.fillAmount = (float)PHYStat / 300f;
        currentINTBar.fillAmount = (float)INTStat / 300f;
        currentCOMBar.fillAmount = (float)COMStat / 300f;
        //Debug.Log(currentPHYBar.fillAmount);
    }

    void HandleSlotSelection(SquadSlotData obj)
    {
        ToggleSelectionPanel();
        selectionPanel.SlotIndex = obj.Index;
        if (obj.Student != null)
        {
            selectionPanel.CurrentSelectedStudent = obj.Student;
            selectionPanel.Select(obj.Student);
        }

    }

    void ToggleSelectionPanel()
    {
        selectionPanel.InitializeStudents();
        selectionPanel.gameObject.SetActive(true);
    }

    public void SendSquad()
    {
        if (RequestManager.instance.CurrentRequest.squad.Count > 0)
        {
            RequestManager.instance.SendSquad();
            Back();
        }
    }

    public void Back()
    {
        requestListPanel.GenerateRequestCard();
        requestListPanel.UpdateDescription(RequestManager.instance.CurrentRequest);
        requestListPanel.ShowIdleWindow();
        requestListPanel.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
