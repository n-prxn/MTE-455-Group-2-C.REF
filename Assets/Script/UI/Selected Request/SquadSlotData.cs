using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SquadSlotData : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject studentSlot;
    [SerializeField] private GameObject blankSlot;
    [SerializeField] private Image portraitImage;
    [SerializeField] private Image portraitFrame; 
    [SerializeField] private TextMeshProUGUI studentName;
    private int index;
    public int Index{
        get{ return index; }
        set{ index = value;}
    }
    [SerializeField] private Student student;
    public Student Student{
        get{ return student; }
        set{ student = value;}
    }

    public event Action<SquadSlotData> OnSlotClicked;

    void Awake(){
        ShowStudent();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetData(int index, Student student){
        this.student = student;
        this.index = index;
        studentName.text = this.student.name;
        portraitImage.sprite = this.student.portrait;
    }

    public void SetData(int index, string name, Sprite portrait){
        this.index = index;
        studentName.text = name;
        portraitImage.sprite = portrait;
    }

    public void ShowStudent(){
        studentSlot.SetActive(true);
        blankSlot.SetActive(false);
    }

    public void ShowBlankSlot(){
        studentSlot.SetActive(false);
        blankSlot.SetActive(true);
    }

    public void OnPointerClick(PointerEventData data)
    {
        PointerEventData pointerEventData = data;
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            OnSlotClicked?.Invoke(this);
        }
    }
}
