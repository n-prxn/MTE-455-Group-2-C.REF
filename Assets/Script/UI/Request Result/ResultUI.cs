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
    [SerializeField] GameObject requestListPanel;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI requestText;

    private RequestSO currentSelectedRequest;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        //squadParent.GetComponent<VerticalLayoutGroup>().spacing += 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.IsPlayable = false;
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

        ReceiveRewards(currentSelectedRequest);
        currentSelectedRequest.ResetSquad();
        currentSelectedRequest.CurrentTurn = currentSelectedRequest.duration;
        currentSelectedRequest.SuccessRate = 100;

        //squadParent.GetComponent<VerticalLayoutGroup>().spacing -= 0.1f;
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
        requestListPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    void ReceiveRewards(RequestSO request)
    {
        GameManager.instance.credits += request.credit;
        GameManager.instance.pyroxenes += request.pyroxene;
        GameManager.instance.currentXP += request.xp;
        GameManager.instance.happiness += request.happiness;
        GameManager.instance.crimeRate -= request.crimeRate;
    }
}
