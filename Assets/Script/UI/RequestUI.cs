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

    [SerializeField] GameObject selectionPanel;
    [SerializeField] List<SquadSlotData> squadSlots = new List<SquadSlotData>();

    //[SerializeField] private RequestSO currentRequest;

    // Start is called before the first frame update
    void Start()
    {
        InitializeSquad();
        UpdateRequestInfo(RequestManager.instance.CurrentRequest);
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateRequestInfo();
    }

    void InitializeSquad()
    {
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
        UpdateRequestRequirement(request);
        UpdateSquadUI(RequestManager.instance.CurrentRequest);
        UpdateCurrentStat(RequestManager.instance.TotalPHYStat, RequestManager.instance.TotalINTStat, RequestManager.instance.TotalCOMStat);
    }

    void UpdateSquadUI(RequestSO request)
    {
        for (int i = 0; i < 4; i++)
        {
            if (RequestManager.instance.CurrentRequest.squad[i] == null)
            {
                squadSlots[i].ShowBlankSlot();
                squadSlots[i].Student = null;
            }
            else
            {
                Student student = RequestManager.instance.CurrentRequest.squad[i];
                squadSlots[i].SetData(i, student);
                squadSlots[i].ShowStudent();
            }
        }
    }

    void UpdateRequestRequirement(RequestSO request)
    {
        PHYReqBar.rectTransform.localPosition = new Vector2((float)request.phyStat / 300f * 485f, PHYReqBar.rectTransform.localPosition.y);
        INTReqBar.rectTransform.localPosition = new Vector2((float)request.intStat / 300f * 485f, INTReqBar.rectTransform.localPosition.y);
        COMReqBar.rectTransform.localPosition = new Vector2((float)request.comStat / 300f * 485f, COMReqBar.rectTransform.localPosition.y);
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
        selectionPanel.GetComponent<StudentSelectionUI>().SlotIndex = obj.Index;
        if (obj.Student != null)
        {
            selectionPanel.GetComponent<StudentSelectionUI>().CurrentSelectedStudent = obj.Student;
            selectionPanel.GetComponent<StudentSelectionUI>().Select(obj.Student);
        }

    }

    void ToggleSelectionPanel()
    {
        selectionPanel.SetActive(true);
    }

    public void ClearSquad()
    {
        RequestManager.instance.CurrentRequest.ResetSquad();
        RequestManager.instance.ClearTotalStatus();
        RequestManager.instance.UpdateRequest();
    }
}
