using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    private TrainingUI trainingUI;

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

    public void SetData(int index, Student student)
    {
        this.index = index;
        if (student != null)
        {
            trainingStudent = student;
            studentName.text = trainingStudent.name;
            portraitImage.sprite = trainingStudent.portrait;

            PHYStat.text = student.CurrentPHYStat.ToString() + "(+" + (student.TrainedPHYStat - student.CurrentPHYStat).ToString() + ")";
            INTStat.text = student.CurrentINTStat.ToString() + "(+" + (student.TrainedINTStat - student.CurrentINTStat).ToString() + ")";
            COMStat.text = student.CurrentCOMStat.ToString() + "(+" + (student.TrainedCOMStat - student.CurrentCOMStat).ToString() + ")";
            stamina.text = student.CurrentStamina.ToString() + "(+" + (student.RestedStamina - student.CurrentStamina).ToString() + ")";

            currentPHYBar.fillAmount = (float)student.CurrentPHYStat / 60f;
            currentINTBar.fillAmount = (float)student.CurrentINTStat / 60f;
            currentCOMBar.fillAmount = (float)student.CurrentCOMStat / 60f;
            staminaBar.fillAmount = (float)student.CurrentStamina / (float)student.stamina;

            trainedPHYBar.fillAmount = ((float)student.CurrentPHYStat + (float)(student.TrainedPHYStat - (float)student.CurrentPHYStat)) / 60f;
            trainedINTBar.fillAmount = (float)(student.CurrentINTStat + ((float)student.TrainedINTStat - (float)student.CurrentINTStat)) / 60f;
            trainedCOMBar.fillAmount = ((float)student.CurrentCOMStat + ((float)student.TrainedCOMStat - (float)student.CurrentCOMStat)) / 60f;
            restedStaminaBar.fillAmount = ((float)student.CurrentStamina + ((float)student.RestedStamina - (float)student.CurrentStamina)) / (float)student.stamina;

            trainingDaysText.text = "Remaining " + student.TrainingDuration.ToString() + " Day(s)";

            freeSlot.SetActive(false);
        }
        else
        {
            freeSlot.SetActive(true);
        }
    }

    public void Resign()
    {
        trainingStudent.IsTraining = false;
        TrainingManager.instance.RemoveStudentFromBuilding(trainingStudent);
        TrainingManager.instance.UpdateTrainingPanel();
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
