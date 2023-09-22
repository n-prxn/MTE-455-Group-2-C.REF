using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;

public enum SelectionMode
{
    Squad,
    Training
}

public class StudentSelectionUI : MonoBehaviour
{
    [SerializeField] SelectionMode selectionMode;
    [Header("Student Prefab")]
    [SerializeField] GameObject studentListParent;
    [SerializeField] GameObject studentPortraitPrefab;

    [Header("Student Description")]
    [SerializeField] StudentDescription studentDescription;

    [Header("Student Search and Filter")]
    [SerializeField] TMP_InputField searchField;

    [Header("Panel")]
    [SerializeField] GameObject idlePanel;
    [SerializeField] GameObject headerPanel;
    [SerializeField] GameObject trainingPanel;

    [Header("Student Data")]
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

    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;

    void OnEnable()
    {
        InitializeStudents();
        CheckStatus();
        if (currentSelectedStudent == null)
        {
            headerPanel.SetActive(false);
            idlePanel.SetActive(true);
        }
        else
        {
            headerPanel.SetActive(true);
            idlePanel.SetActive(false);

            studentDescription.SetDescription(currentSelectedStudent);
            if (selectionMode == SelectionMode.Squad)
            {
                if (currentSelectedStudent.IsAssign)
                    studentDescription.SetRemove();
                else
                    studentDescription.SetAssign();
            }
            else
            {
                if (currentSelectedStudent.IsTraining)
                    studentDescription.SetRemove();
                else
                    studentDescription.SetAssign();
            }
            studentUIDatas.Find(x => x.StudentData.id == currentSelectedStudent.id).Select();
        }
    }

    public void InitializeStudents()
    {
        studentUIDatas.Clear();
        ClearStudentUI();

        //headerPanel.SetActive(false);
        //idlePanel.SetActive(true);
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

        if (selectionMode == SelectionMode.Squad)
        {
            if (RequestManager.instance.CurrentRequest.squad[slotIndex] == null)
            {
                if (obj.StudentData.IsAssign)
                    studentDescription.SetRemoveAndSwitch();
                else
                    studentDescription.SetAssign();
            }
            else
            {
                if (obj.StudentData.IsAssign)
                {
                    if (obj.StudentData.id == RequestManager.instance.CurrentRequest.squad[slotIndex].id)
                        studentDescription.SetRemove();
                    else
                        studentDescription.SetRemoveAndSwitch();
                }
                else
                    studentDescription.SetSwitch();
            }
        }
        else
        {
            if (TrainingManager.instance.GetCurrentStudentsInBuilding()[slotIndex] == null)
            {
                if (obj.StudentData.IsTraining)
                    studentDescription.SetRemoveAndSwitch();
                else
                    studentDescription.SetAssign();
            }
            else
            {
                if (obj.StudentData.IsTraining)
                {
                    if (obj.StudentData.id == TrainingManager.instance.GetCurrentStudentsInBuilding()[slotIndex].id)
                        studentDescription.SetRemove();
                    else
                        studentDescription.SetRemoveAndSwitch();
                }
                else
                    studentDescription.SetSwitch();
            }
        }

        if (obj.StudentData.IsOperating)
            studentDescription.HideButton();

        if (obj.StudentData.IsTraining && selectionMode == SelectionMode.Squad)
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

    public void RemoveTrainingStudent()
    {
        for (int i = 0; i < TrainingManager.instance.GetCurrentStudentsInBuilding().Count; i++)
        {
            if (TrainingManager.instance.GetCurrentStudentsInBuilding()[i] != null)
            {
                if (TrainingManager.instance.GetCurrentStudentsInBuilding()[i].id == currentSelectedStudent.id)
                {
                    TrainingManager.instance.GetCurrentStudentsInBuilding()[i].IsTraining = false;
                    TrainingManager.instance.GetCurrentStudentsInBuilding()[i].TrainingDuration = TrainingManager.instance.GetCurrentBuilding().TrainingDuration;
                    TrainingManager.instance.GetCurrentStudentsInBuilding()[i] = null;
                    Debug.Log("remove");
                }
                else
                    continue;
            }
        }
        TrainingManager.instance.Calculate();
        studentUIDatas.Find(x => x.StudentData.id == currentSelectedStudent.id).Deselect();
        currentSelectedStudent = null;
        CloseSelectionPanel();
    }

    public void SwitchStudent()
    {
        List<Student> students = RequestManager.instance.CurrentRequest.squad;
        if (!currentSelectedStudent.IsAssign)
        {
            AssignStudent();
        }
        else
        {
            Student tmp = students[slotIndex];

            for (int i = 0; i < 4; i++)
            {
                if (students[i] != null)
                {
                    if (students[i].id == currentSelectedStudent.id)
                    {
                        students[slotIndex] = currentSelectedStudent;
                        students[i] = tmp;
                        break;
                    }
                }
            }

            RequestManager.instance.Calculate();
            RequestManager.instance.UpdateRequest();
            studentUIDatas.Find(x => x.StudentData.id == currentSelectedStudent.id).Deselect();
            currentSelectedStudent = null;
            CloseSelectionPanel();
        }

    }

    public void SwitchTrainingStudent()
    {
        List<Student> students = TrainingManager.instance.GetCurrentStudentsInBuilding();
        if (!currentSelectedStudent.IsTraining)
        {
            AssignTrainingStudent();
        }
        else
        {
            Student tmp = students[slotIndex];

            for (int i = 0; i < students.Count; i++)
            {
                if (students[i] != null)
                {
                    if (students[i].id == currentSelectedStudent.id)
                    {
                        students[slotIndex] = currentSelectedStudent;
                        students[i] = tmp;
                        break;
                    }
                }
            }

            TrainingManager.instance.Calculate();

            studentUIDatas.Find(x => x.StudentData.id == currentSelectedStudent.id).Deselect();
            currentSelectedStudent = null;
            CloseSelectionPanel();
        }

    }

    public void SearchStudent()
    {
        studentUIDatas.Clear();
        ClearStudentUI();

        if (searchField.text == "")
        {
            foreach (Student student in SquadController.instance.Students)
            {
                GameObject studentCard = Instantiate(studentPortraitPrefab, studentListParent.transform);
                StudentUIData studentUIData = studentCard.GetComponent<StudentUIData>();
                studentUIData.SetData(student);
                studentUIData.OnStudentClicked += HandleStudentSelection;
                studentUIDatas.Add(studentUIData);
            }
        }
        else
        {
            foreach (Student student in SquadController.instance.Students)
            {
                if (student.name.ToLower().StartsWith(searchField.text.ToLower()))
                {
                    GameObject studentCard = Instantiate(studentPortraitPrefab, studentListParent.transform);
                    StudentUIData studentUIData = studentCard.GetComponent<StudentUIData>();
                    studentUIData.SetData(student);
                    studentUIData.OnStudentClicked += HandleStudentSelection;
                    studentUIDatas.Add(studentUIData);
                }
            }
        }


    }

    public void CloseSelectionPanel()
    {
        if (selectionMode == SelectionMode.Training)
            trainingPanel.SetActive(true);

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

    public void StopPlayingVoice()
    {
        audioSource.Stop();
    }
}
