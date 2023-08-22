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

    public event Action<StudentUIData> OnStudentClicked;
    // Start is called before the first frame update

    void Awake()
    {
        Deselect();
    }

    void Start()
    {
        //Deselect();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetData(Student s)
    {
        studentData = s;
        portraitImage.sprite = studentData.portrait;
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
        PointerEventData pointerEventData = (PointerEventData)data;
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            OnStudentClicked?.Invoke(this);
        }
    }
}
