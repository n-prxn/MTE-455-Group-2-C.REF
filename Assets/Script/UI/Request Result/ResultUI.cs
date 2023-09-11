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


    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        InitializeResult();
    }

    void Awake()
    {
        //squadParent.GetComponent<VerticalLayoutGroup>().spacing += 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        //GameManager.instance.IsPlayable = false;
    }

    public void InitializeResult()
    {
        currentSelectedRequest = RequestManager.instance.CurrentRequest;
        currentSelectedRequest.DecreaseStamina();
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

            pyroxeneText.text = currentSelectedRequest.pyroxene.ToString();
            creditText.text = currentSelectedRequest.credit.ToString();
            expText.text = currentSelectedRequest.xp.ToString();
            happinessText.text = currentSelectedRequest.happiness.ToString();
            crimeRateText.text = currentSelectedRequest.crimeRate.ToString();
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

        //squadParent.GetComponent<VerticalLayoutGroup>().spacing -= 0.1f;;[\]'/
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
        GameManager.instance.credits += request.credit;
        GameManager.instance.pyroxenes += request.pyroxene;
        GameManager.instance.currentXP += request.xp;
        GameManager.instance.happiness += request.happiness;
        GameManager.instance.crimeRate += request.crimeRate;
    }

    void ReceiveDemerit(RequestSO request){
        GameManager.instance.happiness += request.demeritHappiness;
        GameManager.instance.crimeRate += request.demeritCrimeRate;
    }
}
