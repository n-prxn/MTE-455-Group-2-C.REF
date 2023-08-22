using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEditor.PackageManager.Requests;

public class StudentSelectionUI : MonoBehaviour
{
    [SerializeField] GameObject studentListParent;
    [SerializeField] GameObject studentPortraitPrefab;
    [SerializeField] StudentDescription studentDescription;

    [SerializeField] int slotIndex;
    private List<StudentUIData> studentUIDatas = new List<StudentUIData>();
    private Student currentSelectedStudent;
    public Student CurrentSelectedStudent
    {
        get { return currentSelectedStudent; }
        set { currentSelectedStudent = value; }
    }
    public int SlotIndex
    {
        get { return slotIndex; }
        set { slotIndex = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeStudents();
        //studentDescription.ResetDescription();
        studentDescription.SetDescription(SquadController.instance.Students[0]);
        CheckAssign();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeStudents()
    {
        studentUIDatas.Clear();
        foreach (Student student in SquadController.instance.Students)
        {
            GameObject studentCard = Instantiate(studentPortraitPrefab, studentListParent.transform);
            StudentUIData studentUIData = studentCard.GetComponent<StudentUIData>();
            studentUIData.SetData(student);
            studentUIData.OnStudentClicked += HandleStudentSelection;
            studentUIDatas.Add(studentUIData);

            //if (studentUIData.StudentData.id == currentSelectedStudent.id)
             //   StartSelectStudent(studentUIData);
        }
    }

    void CheckAssign()
    {
        foreach (StudentUIData studentUIData in studentUIDatas)
        {
            if (studentUIData.StudentData.IsAssign)
            {
                studentUIData.SetColor(Color.gray);
            }
            else
            {
                studentUIData.SetColor(Color.white);
            }
        }
    }

    void HandleStudentSelection(StudentUIData obj)
    {
        ResetSelection();
        studentDescription.SetDescription(obj.StudentData);
        currentSelectedStudent = obj.StudentData;
        if (obj.StudentData.IsAssign)
        {
            studentDescription.SetRemove();
        }
        else
        {
            studentDescription.SetAssign();
        }
        obj.Select();
    }

    // public void Select(Student student){
    //     foreach(StudentUIData studentUIData in studentUIDatas){
    //         if(studentUIData.StudentData.id == student.id){
    //             studentUIData.Select();
    //             studentDescription.SetDescription(student);
    //             studentDescription.SetRemove();
    //             break;
    //         }
    //     }
    // }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ResetSelection()
    {
        studentDescription.ResetDescription();
        DeselectAllStudents();
    }

    private void DeselectAllStudents()
    {
        foreach (Transform student in studentListParent.GetComponent<Transform>())
        {
            student.GetComponent<StudentUIData>().Deselect();
        }
    }

    private void StartSelectStudent(StudentUIData studentUIData)
    {
        studentUIData.Select();
        CheckAssign();
        studentDescription.SetRemove();
    }

    public void AssignStudent()
    {
        RequestManager.instance.CurrentRequest.squad[slotIndex] = currentSelectedStudent;
        RequestManager.instance.Calculate();
        currentSelectedStudent.IsAssign = true;
        RequestManager.instance.UpdateRequest();
        CloseSelectionPanel();
    }

    public void RemoveStudent()
    {
        RequestManager.instance.CurrentRequest.squad[slotIndex] = null;
        RequestManager.instance.Calculate();
        currentSelectedStudent.IsAssign = false;
        RequestManager.instance.UpdateRequest();
        CloseSelectionPanel();
    }

    public void CloseSelectionPanel()
    {
        CheckAssign();
        this.gameObject.SetActive(false);
    }
}
