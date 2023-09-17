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
    [SerializeField] GameObject idlePanel;
    [SerializeField] GameObject headerPanel;
    [SerializeField] GameObject trainingPanel;
    [SerializeField] int slotIndex;
    private List<StudentUIData> studentUIDatas = new();
    [SerializeField] private Student currentSelectedStudent;
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

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = SquadController.instance.gameObject.GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        InitializeStudents();
        CheckStatus();
        if (currentSelectedStudent != null)
        {
            studentDescription.SetDescription(currentSelectedStudent);
            studentDescription.SetRemove();
            studentUIDatas.Find(x => x.StudentData.id == currentSelectedStudent.id).Select();
        }
    }

    void Update()
    {

    }

    public void InitializeStudents()
    {
        currentSelectedStudent = null;
        studentUIDatas.Clear();
        ClearStudentUI();

        headerPanel.SetActive(false);
        idlePanel.SetActive(true);
        studentDescription.HideButton();

        foreach (Student student in SquadController.instance.Students)
        {
            GameObject studentCard = Instantiate(studentPortraitPrefab, studentListParent.transform);
            StudentUIData studentUIData = studentCard.GetComponent<StudentUIData>();
            studentUIData.SetData(student);
            studentUIData.OnStudentClicked += HandleStudentSelection;
            studentUIDatas.Add(studentUIData);
        }
        ResetSelection();
    }

    public void ClearStudentUI()
    {
        foreach (Transform studentCard in studentListParent.transform)
        {
            Destroy(studentCard.gameObject);
        }
    }

    public void CheckStatus()
    {
        foreach (StudentUIData studentUIData in studentUIDatas)
        {
            if (studentUIData.StudentData.IsOperating)
            {
                studentUIData.SetColor(Color.gray);
                studentUIData.SetStatus(0);
                continue;
            }

            if (studentUIData.StudentData.IsAssign)
            {
                studentUIData.SetColor(Color.gray);
                studentUIData.SetStatus(2);
                continue;
            }

            if (studentUIData.StudentData.IsTraining)
            {
                studentUIData.SetColor(Color.gray);
                studentUIData.SetStatus(1);
                continue;
            }

            studentUIData.SetColor(Color.white);
            studentUIData.RemoveStatus();
        }
    }

    void HandleStudentSelection(StudentUIData obj)
    {
        ResetSelection();
        headerPanel.SetActive(true);
        idlePanel.SetActive(false);

        studentDescription.SetDescription(obj.StudentData);
        currentSelectedStudent = obj.StudentData;
        if (obj.StudentData.IsAssign)
            studentDescription.SetRemove();
        else
            studentDescription.SetAssign();

        if (obj.StudentData.IsTraining || obj.StudentData.IsOperating)
            studentDescription.HideButton();
        obj.Select();
    }

    private void ResetSelection()
    {
        studentDescription.ResetDescription();

        DeselectAllStudents();
    }

    private void DeselectAllStudents()
    {
        foreach (StudentUIData student in studentUIDatas)
        {
            student.Deselect();
        }
    }

    public void AssignStudent()
    {
        if (RequestManager.instance.CurrentRequest.squad[slotIndex] != null)
        {
            RequestManager.instance.CurrentRequest.squad[slotIndex].IsAssign = false;
        }

        RequestManager.instance.CurrentRequest.squad[slotIndex] = currentSelectedStudent;
        RequestManager.instance.Calculate();
        currentSelectedStudent.IsAssign = true;

        RequestManager.instance.UpdateRequest();
        PlayStudentVoice(currentSelectedStudent);

        studentUIDatas.Find(x => x.StudentData.id == currentSelectedStudent.id).Deselect();
        currentSelectedStudent = null;
        CloseSelectionPanel();
    }

    public void AssignTrainingStudent()
    {
        if (TrainingManager.instance.GetCurrentStudentsInBuilding()[slotIndex] != null)
        {
            TrainingManager.instance.GetCurrentStudentsInBuilding()[slotIndex].IsTraining = false;
        }

        TrainingManager.instance.SetStudentInBuilding(slotIndex, currentSelectedStudent);

        currentSelectedStudent.IsTraining = true;
        currentSelectedStudent.TrainingDuration = TrainingManager.instance.GetCurrentBuilding().TrainingDuration;
        TrainingManager.instance.Calculate();

        PlayStudentVoice(currentSelectedStudent);

        studentUIDatas.Find(x => x.StudentData.id == currentSelectedStudent.id).Deselect();
        currentSelectedStudent = null;

        trainingPanel.SetActive(true);
        CloseSelectionPanel();
    }

    public void RemoveStudent()
    {
        for (int i = 0; i < 4; i++)
        {
            if (RequestManager.instance.CurrentRequest.squad[i] != null)
            {
                if (RequestManager.instance.CurrentRequest.squad[i].id == currentSelectedStudent.id)
                    RequestManager.instance.CurrentRequest.squad[i] = null;
                else
                    continue;
            }
        }
        RequestManager.instance.Calculate();
        currentSelectedStudent.IsAssign = false;
        RequestManager.instance.UpdateRequest();
        studentUIDatas.Find(x => x.StudentData.id == currentSelectedStudent.id).Deselect();
        currentSelectedStudent = null;
        CloseSelectionPanel();
    }

    public void CloseSelectionPanel()
    {
        gameObject.SetActive(false);
    }

    public void PlayStudentVoice(Student student)
    {
        if (student.studentVoices.Length > 0)
        {
            AudioClip audioClip = student.studentVoices[2];
            audioSource.PlayOneShot(audioClip);
        }
    }
}
