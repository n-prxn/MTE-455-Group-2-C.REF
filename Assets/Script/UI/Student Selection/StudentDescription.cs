using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StudentDescription : MonoBehaviour
{
    [Header("General Info")]
    [SerializeField] Image studentArtwork;
    [SerializeField] TextMeshProUGUI studentName;
    [SerializeField] TextMeshProUGUI schoolName;

    [Header("Status Bar")]
    [SerializeField] Image PHYBar;
    [SerializeField] Image BuffPHYBar; [Space(20)]
    [SerializeField] Image INTBar;
    [SerializeField] Image BuffINTBar; [Space(20)]
    [SerializeField] Image COMBar;
    [SerializeField] Image BuffCOMBar; [Space(20)]
    [SerializeField] Image staminaBar;

    [Header("Status Text")]
    [SerializeField] TextMeshProUGUI PHYText;
    [SerializeField] TextMeshProUGUI INTText;
    [SerializeField] TextMeshProUGUI COMText;
    [SerializeField] TextMeshProUGUI staminaText;

    [Header("Skill")]
    [SerializeField] Image skillIcon;
    [SerializeField] TextMeshProUGUI skillNameText;
    [SerializeField] TextMeshProUGUI skillDescText;

    [Header("Button")]
    [SerializeField] GameObject assignBtn;
    [SerializeField] GameObject removeBtn;
    [SerializeField] GameObject switchBtn;
    // Start is called before the first frame update

    void Awake()
    {
        ResetDescription();
    }

    public void SetDescription(Student s)
    {
        studentArtwork.gameObject.SetActive(true);
        studentArtwork.sprite = s.artwork;
        studentName.text = s.name;
        schoolName.text = s.school.ToString();
        PHYBar.fillAmount = s.TempPHYStat / 60f;
        INTBar.fillAmount = s.TempINTStat / 60f;
        COMBar.fillAmount = s.TempCOMStat / 60f;

        BuffPHYBar.fillAmount = (float)s.CurrentPHYStat / 60f;
        BuffINTBar.fillAmount = (float)s.CurrentINTStat / 60f;
        BuffCOMBar.fillAmount = (float)s.CurrentCOMStat / 60f;

        staminaBar.fillAmount = (float)s.CurrentStamina / (float)s.stamina;
        PHYText.text = s.CurrentPHYStat.ToString();
        INTText.text = s.CurrentINTStat.ToString();
        COMText.text = s.CurrentCOMStat.ToString();
        staminaText.text = s.CurrentStamina.ToString();

        skillIcon.sprite = s.skillIcon;
        skillNameText.text = s.skillName;
        skillDescText.text = s.skillDescription;
    }

    public void ResetDescription()
    {
        studentArtwork.gameObject.SetActive(false);
        studentName.text = "";
        schoolName.text = "";
        PHYBar.fillAmount = 0;
        INTBar.fillAmount = 0;
        COMBar.fillAmount = 0;
        staminaBar.fillAmount = 0;

        BuffPHYBar.fillAmount = 0;
        BuffINTBar.fillAmount = 0;
        BuffCOMBar.fillAmount = 0;

        PHYText.text = "0";
        INTText.text = "0";
        COMText.text = "0";
        staminaText.text = "0";
    }

    public void HideButton()
    {
        assignBtn.SetActive(false);
        removeBtn.SetActive(false);
        switchBtn.SetActive(false);
    }

    public void SetAssign()
    {
        assignBtn.SetActive(true);
        removeBtn.SetActive(false);
        switchBtn.SetActive(false);
    }

    public void SetRemove()
    {
        assignBtn.SetActive(false);
        removeBtn.SetActive(true);
        switchBtn.SetActive(false);
    }

    public void SetSwitch()
    {
        assignBtn.SetActive(false);
        removeBtn.SetActive(false);
        switchBtn.SetActive(true);
    }

    public void SetRemoveAndSwitch(){
        assignBtn.SetActive(false);
        removeBtn.SetActive(true);
        switchBtn.SetActive(true);
    }
}
