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

    [SerializeField] private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {

    }

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
            if (currentSelectedStudent.IsAssign)
                studentDescription.SetRemove();
            else
                studentDescription.SetAssign();
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

        if (RequestManager.instance.CurrentRequest.squad[slotIndex] == null)
        {
            if (obj.StudentData.IsAssign)
            {
                studentDescription.SetRemoveAndSwitch();
            }
            else
            {
                studentDescription.SetAssign();
            }
        }
        else
        {
            if (obj.StudentData.IsAssign)
            {
                if (obj.StudentData.id == RequestManager.instance.CurrentRequest.squad[slotIndex].id)
                {
                    studentDescription.SetRemove();
                }
                else
                {
                    studentDescription.SetRemoveAndSwitch();
                }
            }
            else
            {
                studentDescription.SetSwitch();
            }
        }

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

    public void StopPlayingVoice()
    {
        audioSource.Stop();
    }
}
