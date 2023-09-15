using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrainingCardData : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image portraitImage;
    [SerializeField] private TMP_Text studentName;
    [SerializeField] private TMP_Text trainingDaysText;

    [Header("Panel")]
    [SerializeField] private GameObject freeSlot;

    [Header("Status Text")]
    [SerializeField] private TMP_Text PHYStat;
    [SerializeField] private TMP_Text INTStat;
    [SerializeField] private TMP_Text COMStat;
    [SerializeField] private TMP_Text stamina;

    [Header("Status Bar")]
    [SerializeField] private Image currentPHYBar;
    [SerializeField] private Image currentINTBar;
    [SerializeField] private Image currentCOMBar;
    [SerializeField] private Image staminaBar;

    [Header("Trained Bar")]
    [SerializeField] private Image trainedPHYBar;
    [SerializeField] private Image trainedINTBar;
    [SerializeField] private Image trainedCOMBar;
    [SerializeField] private Image restedStaminaBar;

    private Student trainingStudent;
    public Student TrainingStudent
    {
        get { return trainingStudent; }
        set { trainingStudent = value; }
    }

    private int index;
    public int Index
    {
        get { return index; }
        set { index = value; }
    }

    public event Action<TrainingCardData> OnStudentClicked;

    public void SetData(int index, Student student, BuildingSO building)
    {
        this.index = index;
        if (student != null)
        {
            trainingStudent = student;
            studentName.text = trainingStudent.name;
            portraitImage.sprite = trainingStudent.portrait;

            PHYStat.text = student.CurrentPHYStat.ToString() + "(+" + building.BonusPHYTraining.ToString() + ")";
            INTStat.text = student.CurrentINTStat.ToString() + "(+" + building.BonusINTTraining.ToString() + ")";
            COMStat.text = student.CurrentCOMStat.ToString() + "(+" + building.BonusCOMTraining.ToString() + ")";
            stamina.text = student.CurrentStamina.ToString() + "(+" + building.BonusStaminaRested.ToString() + ")";

            currentPHYBar.fillAmount = student.CurrentPHYStat / 60f;
            currentINTBar.fillAmount = student.CurrentINTStat / 60f;
            currentCOMBar.fillAmount = student.CurrentCOMStat / 60f;
            staminaBar.fillAmount = student.CurrentStamina / 60f;

            trainedPHYBar.fillAmount = (student.CurrentPHYStat + building.BonusPHYTraining) / 60f;
            trainedINTBar.fillAmount = (student.CurrentINTStat + building.BonusINTTraining) / 60f;
            trainedCOMBar.fillAmount = (student.CurrentCOMStat + building.BonusCOMTraining) / 60f;
            restedStaminaBar.fillAmount = (student.CurrentStamina + building.BonusStaminaRested) / 60f;

            trainingDaysText.text = "Remaining " + student.TrainingDuration.ToString() + " Day(s)";

            freeSlot.SetActive(false);
        }
        else
        {
            freeSlot.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData data)
    {
        PointerEventData pointerEventData = data;
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            OnStudentClicked?.Invoke(this);
        }
    }
}
