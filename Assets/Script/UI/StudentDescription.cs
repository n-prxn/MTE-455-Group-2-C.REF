using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StudentDescription : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Image studentPortrait;
    [SerializeField] TextMeshProUGUI studentName;
    [SerializeField] Image PHYBar;
    [SerializeField] Image INTBar;
    [SerializeField] Image COMBar;
    [SerializeField] Image staminaBar;
    [SerializeField] TextMeshProUGUI PHYText;
    [SerializeField] TextMeshProUGUI INTText;
    [SerializeField] TextMeshProUGUI COMText;
    [SerializeField] TextMeshProUGUI staminaText;
    // Start is called before the first frame update

    void Awake(){
        ResetDescription();
    }

    public void SetDescription(Student s){
        studentPortrait.gameObject.SetActive(true);
        studentPortrait.sprite = s.portrait;
        studentName.text = s.name;
        PHYBar.fillAmount = (float)s.CurrentPHYStat/60f;
        INTBar.fillAmount = (float)s.CurrentINTStat/60f;
        COMBar.fillAmount = (float)s.CurrentCOMStat/60f;
        staminaBar.fillAmount = (float)s.CurrentStamina/(float)s.stamina;
        PHYText.text = s.CurrentPHYStat.ToString();
        INTText.text = s.CurrentINTStat.ToString();
        COMText.text = s.CurrentCOMStat.ToString();
        staminaText.text = s.CurrentStamina.ToString();
    }

    public void ResetDescription(){
        studentPortrait.gameObject.SetActive(false);
        studentName.text = "";
        PHYBar.fillAmount = 0;
        INTBar.fillAmount = 0;
        COMBar.fillAmount = 0;
        staminaBar.fillAmount = 0;
        PHYText.text = "0";
        INTText.text = "0";
        COMText.text = "0";
        staminaText.text = "0";
    }
}
