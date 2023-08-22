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
    public Student CurrentSelectedStudent{
        get{ return currentSelectedStudent; }
        set{ currentSelectedStudent = value;}
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
        }
    }

    void HandleStudentSelection(StudentUIData obj)
    {
        ResetSelection();
        studentDescription.SetDescription(obj.StudentData);
        if(obj.StudentData.IsAssign)
            studentDescription.SetRemove();
        else
            studentDescription.SetAssign();
        currentSelectedStudent = obj.StudentData;
        obj.Select();
    }

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

    public void AssignStudent()
    {
        currentSelectedStudent.IsAssign = true;
        RequestManager.instance.CurrentRequest.squad[slotIndex] = currentSelectedStudent;
        RequestManager.instance.AddStatusToRequest(currentSelectedStudent);
        CloseSelectionPanel();
    }

    public void RemoveStudent(){
        currentSelectedStudent.IsAssign = false;
        RequestManager.instance.CurrentRequest.squad[slotIndex] = null;
        RequestManager.instance.DecreaseStatus(currentSelectedStudent);
        CloseSelectionPanel();
    }

    public void CloseSelectionPanel(){
        this.gameObject.SetActive(false);
    }
}
