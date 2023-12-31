using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField] TextMeshProUGUI PHYReqAmount;
    [SerializeField] TextMeshProUGUI INTReqAmount;
    [SerializeField] TextMeshProUGUI COMReqAmount;

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
    void OnEnable()
    {
        RequestManager.instance.Calculate();
    }

    void Awake()
    {
        InitializeSquad();
    }

    // Update is called once per frame
    void Update()
    {
        //GameManager.instance.IsPlayable = false;
        RequestManager.instance.UpdateRequest();
    }

    public void InitializeSquad()
    {
        ResetSlot();
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
        creditText.text = request.CurrentCredit.ToString();
        pyroxeneText.text = request.pyroxene.ToString();
        expText.text = request.CurrentXP.ToString();
        happinessText.text = request.CurrentHappiness.ToString();
        crimeRateText.text = request.CurrentCrimeRate.ToString();
    }

    public void ResetSlot()
    {
        if (slotParent.transform.childCount > 0)
        {
            foreach (Transform slot in slotParent.transform)
            {
                Destroy(slot.gameObject);
            }
        }
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
        int phyStat = request.phyStat, intStat = request.intStat, comStat = request.comStat;

        float multiplier = 0f;
        int happiness = GameManager.Instance.happiness;
        if (happiness >= 70)
        {
            multiplier = 1f;
        }
        else if (happiness >= 50)
        {
            multiplier = 1.05f;
        }
        else if (happiness >= 30)
        {
            multiplier = 1.1f;
        }
        else
            multiplier = 1.2f;

        PHYReqAmount.text = RequestManager.instance.TotalPHYStat.ToString() + " / " + phyStat.ToString() + " (+" + (int)(phyStat * (multiplier - 1)) + ")";
        INTReqAmount.text = RequestManager.instance.TotalINTStat.ToString() + " / " + intStat.ToString() + " (+" + (int)(intStat * (multiplier - 1)) + ")";
        COMReqAmount.text = RequestManager.instance.TotalCOMStat.ToString() + " / " + comStat.ToString() + " (+" + (int)(comStat * (multiplier - 1)) + ")";

        phyStat = (int)(phyStat * multiplier) <= 300 ? (int)(phyStat * multiplier) : 300;
        intStat = (int)(intStat * multiplier) <= 300 ? (int)(intStat * multiplier) : 300;
        comStat = (int)(comStat * multiplier) <= 300 ? (int)(comStat * multiplier) : 300;

        PHYReqBar.rectTransform.localPosition = new Vector2((float)phyStat / 300f * 485f, PHYReqBar.rectTransform.localPosition.y);
        INTReqBar.rectTransform.localPosition = new Vector2((float)intStat / 300f * 485f, INTReqBar.rectTransform.localPosition.y);
        COMReqBar.rectTransform.localPosition = new Vector2((float)comStat / 300f * 485f, COMReqBar.rectTransform.localPosition.y);
        successRate.text = "Success Rate : " + RequestManager.instance.CalculateSuccessRate() + "%";

        request.multipliedPhyStat = phyStat;
        request.multipliedIntStat = intStat;
        request.multipliedComStat = comStat;
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
        selectionPanel.SlotIndex = obj.Index;
        if (obj.Student != null)
            selectionPanel.CurrentSelectedStudent = obj.Student;
        ToggleSelectionPanel();
    }

    void ToggleSelectionPanel()
    {
        selectionPanel.gameObject.SetActive(true);
    }

    public void SendSquad()
    {
        int studentAmount = 0;
        foreach (Student student in RequestManager.instance.CurrentRequest.squad)
        {
            if (student != null)
                studentAmount++;
        }

        if(studentAmount <= 0)
            return;

        RequestManager.instance.SendSquad();
        requestListPanel.UpdateDescription(RequestManager.instance.CurrentRequest);
        requestListPanel.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ClearSquad()
    {
        RequestManager.instance.CurrentRequest.ResetSquad();
        RequestManager.instance.ClearTotalStatus();
        RequestManager.instance.UpdateRequest();
    }

    public void Back()
    {
        requestListPanel.UpdateDescription(RequestManager.instance.CurrentRequest);
        requestListPanel.gameObject.SetActive(true);
        RequestManager.instance.CurrentRequest.ResetSquad();
        gameObject.SetActive(false);
    }

    public void ToggleTutorialSelection(){
        HandleSlotSelection(squadSlots[0]);
    }
}
