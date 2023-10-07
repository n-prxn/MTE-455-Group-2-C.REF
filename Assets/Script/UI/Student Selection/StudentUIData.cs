using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using TMPro;

public class StudentUIData : MonoBehaviour, IPointerClickHandler
{
    private Student studentData;
    public Student StudentData
    {
        get { return studentData; }
        set { studentData = value; }
    }
    [SerializeField] private Image portraitImage;
    [SerializeField] private Image borderImage;
    [SerializeField] private Image detailBox;
    [SerializeField] private TMP_Text detailText;
    [SerializeField] private GameObject statusIcon;
    [SerializeField] private Image icon;
    [SerializeField] private Sprite[] icons;

    public event Action<StudentUIData> OnStudentClicked;
    // Start is called before the first frame update

    void Awake()
    {
        //Deselect();
    }

    public void SetData(Student s, SortingMode sortingMode)
    {
        studentData = s;
        portraitImage.sprite = studentData.portrait;
        statusIcon.SetActive(false);

        //borderImage.color = new Color32(0, 177, 255, 255);
        //detailBox.color = new Color32(164, 235, 255, 255);
        string detail = "";

        switch (sortingMode)
        {
            case SortingMode.Name:
                detail = s.name;
                break;
            case SortingMode.Rarity:
                detail = s.name;
                break;
            case SortingMode.School:
                detail = s.school.ToString();
                break;
            case SortingMode.Club:
                detail = s.club;
                break;
            case SortingMode.PHYStat:
                detail = s.CurrentPHYStat.ToString();
                break;
            case SortingMode.INTStat:
                detail = s.CurrentINTStat.ToString();
                break;
            case SortingMode.COMStat:
                detail = s.CurrentCOMStat.ToString();
                break;
            case SortingMode.Stamina:
                detail = s.CurrentStamina.ToString();
                break;
        }

        switch (s.rarity)
        {
            case Rarity.Common:
                borderImage.color = new Color32(0, 173, 255, 255);
                detailBox.color = new Color32(164, 235, 255, 255);
                break;
            case Rarity.Uncommon:
                borderImage.color = new Color32(255, 249, 194, 255);
                detailBox.color = new Color32(255, 198, 0, 255);
                break;
            case Rarity.Rare:
                borderImage.color = new Color32(204, 0, 255, 255);
                detailBox.color = new Color32(236, 189, 255, 255);
                break;
        }
        detailText.text = detail;
    }

    public void SetStatus(int status)
    {
        statusIcon.SetActive(true);
        if(status == 3)
            icon.color = Color.white;
        else
            icon.color = new Color32(180,1,1,255);
        icon.sprite = icons[status];
    }

    public void RemoveStatus()
    {
        statusIcon.SetActive(false);
    }

    public void SetColor(Color color)
    {
        portraitImage.color = color;
    }

    public void Select()
    {
        borderImage.color = new Color32(27, 188, 38, 255);
        detailBox.color = new Color32(206, 255, 194, 255);
    }

    public void Deselect()
    {
        switch (studentData.rarity)
        {
            case Rarity.Common:
                borderImage.color = new Color32(0, 173, 255, 255);
                detailBox.color = new Color32(164, 235, 255, 255);
                break;
            case Rarity.Uncommon:
                borderImage.color = new Color32(255, 249, 194, 255);
                detailBox.color = new Color32(255, 198, 0, 255);
                break;
            case Rarity.Rare:
                borderImage.color = new Color32(204, 0, 255, 255);
                detailBox.color = new Color32(236, 189, 255, 255);
                break;
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
