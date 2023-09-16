using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

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
    [SerializeField] private GameObject statusIcon;
    [SerializeField] private Image icon;
    [SerializeField] private Sprite[] icons;

    public event Action<StudentUIData> OnStudentClicked;
    // Start is called before the first frame update

    void Awake()
    {
        Deselect();
    }

    public void SetData(Student s)
    {
        studentData = s;
        portraitImage.sprite = studentData.portrait;
        statusIcon.SetActive(false);
    }

    public void SetStatus(int status){
        statusIcon.SetActive(true);
        icon.sprite = icons[status];
    }

    public void RemoveStatus(){
        statusIcon.SetActive(false);
    }

    public void SetColor(Color color){
        portraitImage.color = color;
    }

    public void Select()
    {
        borderImage.enabled = true;
    }

    public void Deselect()
    {
        borderImage.enabled = false;
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
