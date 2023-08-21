using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class StudentSelectionUI : MonoBehaviour
{
    [SerializeField] GameObject studentListParent;
    [SerializeField] GameObject studentPortraitPrefab;
    [SerializeField] StudentDescription studentDescription;

    // Start is called before the first frame update
    void Start()
    {
        InitializeStudents();
        studentDescription.ResetDescription();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeStudents(){
        foreach(Student student in SquadController.instance.Students){
            GameObject studentCard = Instantiate(studentPortraitPrefab, studentListParent.GetComponent<Transform>());
            studentCard.GetComponent<StudentUIData>().SetData(student);
            studentCard.GetComponent<StudentUIData>().OnStudentClicked += HandleStudentSelection;
        }
    }

    void HandleStudentSelection(StudentUIData obj){
        ResetSelection();
        studentDescription.SetDescription(obj.StudentData);
        obj.Select();
    }

    public void Show(){
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void Hide(){
        gameObject.SetActive(false);
    }

    private void ResetSelection(){
        studentDescription.ResetDescription();
        DeselectAllStudents();
    }

    private void DeselectAllStudents(){
        foreach(Transform student in studentListParent.GetComponent<Transform>()){
            student.GetComponent<StudentUIData>().Deselect();
        }
    }
}
