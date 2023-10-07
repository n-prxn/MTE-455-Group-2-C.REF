using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultUI : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] GameObject squadParent;
    [SerializeField] GameObject studentStaminaPrefab;

    [Header("Panel")]
    [SerializeField] RequestListUI requestListPanel;
    [SerializeField] GameObject successPanel;
    [SerializeField] GameObject failedPanel;

    [Header("Reward")]
    [SerializeField] GameObject rewardPanel;
    [SerializeField] TMP_Text creditText;
    [SerializeField] TMP_Text pyroxeneText;
    [SerializeField] TMP_Text expText;
    [SerializeField] TMP_Text happinessText;
    [SerializeField] TMP_Text crimeRateText;

    [Header("Demerit")]
    [SerializeField] GameObject demeritPanel;
    [SerializeField] TMP_Text deHappinessText;
    [SerializeField] TMP_Text deCrimeRateText;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI requestText;

    private RequestSO currentSelectedRequest;

    void OnEnable()
    {
        InitializeResult();
    }

    public void InitializeResult()
    {
        currentSelectedRequest = RequestManager.instance.CurrentRequest;
        ClearSquadInfo();

        for (int i = 0; i < 4; i++)
        {
            Student student = currentSelectedRequest.squad[i];
            if (student == null)
                continue;

            GameObject studentStamina = Instantiate(studentStaminaPrefab, squadParent.transform);
            StudentStaminaData studentStaminaData = studentStamina.GetComponent<StudentStaminaData>();
            studentStaminaData.SetData(student);
        }
        requestText.text = currentSelectedRequest.name;

        currentSelectedRequest.ResetSquad();
        currentSelectedRequest.CurrentTurn = currentSelectedRequest.duration;
        currentSelectedRequest.SuccessRate = 100;

        if (currentSelectedRequest.IsSuccess)
        {
            ReceiveRewards(currentSelectedRequest);
            successPanel.SetActive(true);
            failedPanel.SetActive(false);
            rewardPanel.SetActive(true);
            demeritPanel.SetActive(false);

            if (currentSelectedRequest.difficulty == Difficulty.Emergency)
                RequestManager.instance.IsEmergency = false;

            pyroxeneText.text = currentSelectedRequest.pyroxene.ToString();
            creditText.text = currentSelectedRequest.CurrentCredit.ToString();
            expText.text = currentSelectedRequest.CurrentXP.ToString();
            happinessText.text = currentSelectedRequest.CurrentHappiness.ToString();
            crimeRateText.text = currentSelectedRequest.CurrentCrimeRate.ToString();
        }
        else
        {
            ReceiveDemerit(currentSelectedRequest);
            successPanel.SetActive(false);
            failedPanel.SetActive(true);
            rewardPanel.SetActive(false);
            demeritPanel.SetActive(true);

            deHappinessText.text = currentSelectedRequest.demeritHappiness.ToString();
            deCrimeRateText.text = currentSelectedRequest.demeritCrimeRate.ToString();
        }

        currentSelectedRequest.InitializeCurrentReward();
        GameManager.Instance.RankUp();
    }

    void ClearSquadInfo()
    {
        if (squadParent.transform.childCount > 0)
        {
            foreach (Transform student in squadParent.transform)
                Destroy(student.gameObject);
        }
    }

    public void ClosePanel()
    {
        currentSelectedRequest.ResetSquad();
        requestListPanel.gameObject.SetActive(true);
        requestListPanel.ShowIdleWindow();
        gameObject.SetActive(false);
    }

    void ReceiveRewards(RequestSO request)
    {
        GameManager.Instance.credits += request.id == 0 ? 10000 : request.CurrentCredit;
        GameManager.Instance.pyroxenes += request.id == 0 ? 1200 : request.pyroxene;
        GameManager.Instance.currentXP += request.id == 0 ? 250 : request.CurrentXP;
        GameManager.Instance.happiness += request.id == 0 ? 1 : request.CurrentHappiness;
        GameManager.Instance.crimeRate += request.id == 0 ? -1 : request.CurrentCrimeRate;
    }

    void ReceiveDemerit(RequestSO request)
    {
        GameManager.Instance.happiness += request.demeritHappiness;
        GameManager.Instance.crimeRate += request.demeritCrimeRate;
        if (request.difficulty == Difficulty.Emergency)
            RequestManager.instance.TodayRequests.Add(request);
    }
}
