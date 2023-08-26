using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] GameObject gachaPanel;
    [SerializeField] GameObject requestListPanel;

    [Header("Turn UI")]
    [SerializeField] TMP_Text turnText;

    [Header("Resource UI")]
    [SerializeField] TMP_Text creditAmountText;
    [SerializeField] TMP_Text pyroxeneAmountText;

    [Header("Player Info UI")]
    [SerializeField] TMP_Text rankText;
    [SerializeField] Image xpBar;

    [Header("Side Menu")]
    [SerializeField] GameObject gachaButton;
    [SerializeField] GameObject requestButton;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateUIResource();
        UpdateButton();
    }

    void UpdateButton(){
        if(GameManager.instance.pyroxenes >= 1200){
            gachaButton.transform.GetChild(1).gameObject.SetActive(true);
        }else{
            gachaButton.transform.GetChild(1).gameObject.SetActive(false);
        }

        if(RequestManager.instance.isNotice()){
            requestButton.transform.GetChild(1).gameObject.SetActive(true);
        }else{
            requestButton.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    void UpdateUIResource()
    {
        //Update Turn
        turnText.text = GameManager.instance.currentTurn.ToString();

        //Update Resource
        creditAmountText.text = GameManager.instance.credits.ToString();
        pyroxeneAmountText.text = GameManager.instance.pyroxenes.ToString();

        //Update Rank
        rankText.text = "RANK " + GameManager.instance.rank.ToString();
        xpBar.fillAmount = (float)GameManager.instance.currentXP / (float)GameManager.instance.maxXP;
        //Debug.Log(GameManager.instance.currentXP/GameManager.instance.maxXP);
    }

    public void ToggleGachaPanel()
    {
        if (gachaPanel.activeSelf)
            gachaPanel.SetActive(false);
        else
            gachaPanel.SetActive(true);
    }

    public void ToggleRequestListPanel()
    {
        if (requestListPanel.activeSelf)
            requestListPanel.SetActive(false);
        else
        {
            requestListPanel.GetComponent<RequestListUI>().GenerateRequestCard(); 
            requestListPanel.GetComponent<RequestListUI>().ShowIdleWindow();
            requestListPanel.SetActive(true);
        }
    }

}
