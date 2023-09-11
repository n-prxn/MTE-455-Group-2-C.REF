using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingCardData : MonoBehaviour
{
    [SerializeField] private Image portraitImage;
    [SerializeField] private TMP_Text studentName;

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

    public void SetData(Student student)
    {
        if (student != null)
        {
            trainingStudent = student;
            studentName.text = trainingStudent.name;
            portraitImage.sprite = trainingStudent.portrait;

            freeSlot.SetActive(false);
        }else{
            freeSlot.SetActive(true);
        }

    }
}
