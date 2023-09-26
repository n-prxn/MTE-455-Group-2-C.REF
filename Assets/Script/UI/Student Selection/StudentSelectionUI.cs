using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;
using System.Linq;

public enum SelectionMode
{
    Squad,
    Training
}

public enum SortingMode
{
    Name = 0,
    Rarity = 1,
    School = 2,
    Club = 3,
    PHYStat = 4,
    INTStat = 5,
    COMStat = 6,
    Stamina = 7
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
    [SerializeField] GameObject filterPanel;
    private SortingMode currentSortingMode = SortingMode.Name;

    [Header("Panel")]
    [SerializeField] GameObject idlePanel;
    [SerializeField] GameObject headerPanel;
    [SerializeField] GameObject trainingPanel;

    [Header("Student Data")]
    [SerializeField] int slotIndex;
    private List<StudentUIData> studentUIDatas = new();
    private List<Student> selectableStudents = new();
    [SerializeField] private Student currentSelectedStudent;
    [Header("Unavailable Panel")]
    [SerializeField] private GameObject availablePanel;
    [SerializeField] private GameObject unavailablePanel;
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

    void Awake()
    {
        audioSource = GameObject.FindGameObjectWithTag("Voice Audio").GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        audioSource.Stop();
        selectableStudents = SquadController.instance.Students.OrderBy(x => x.name).ToList();
        ResetSelectionPanel();
    }

    public void InitializeStudents()
    {
        studentUIDatas.Clear();
        ClearStudentUI();

        studentDescription.HideButton();

        foreach (Student student in selectableStudents)
        {
            //Debug.Log(student.name);
            GameObject studentCard = Instantiate(studentPortraitPrefab, studentListParent.transform);
            StudentUIData studentUIData = studentCard.GetComponent<StudentUIData>();
            studentUIData.SetData(student, currentSortingMode);
            studentUIData.OnStudentClicked += HandleStudentSelection;
            studentUIDatas.Add(studentUIData);
        }
        ResetSelection();
    }

    public void ResetSelectionPanel()
    {
        if (SquadController.instance.Students.Count <= 0)
        {
            availablePanel.SetActive(false);
            unavailablePanel.SetActive(true);
        }
        else
        {
            availablePanel.SetActive(true);
            unavailablePanel.SetActive(false);
        }


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

            studentDescription.SetDescription(currentSelectedStudent);
            studentUIDatas.Find(x => x.StudentData.id == currentSelectedStudent.id).Select();
        }
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
            CloseSelectionPanel();
        }

    }

    public void SearchStudent()
    {
        studentUIDatas.Clear();
        ClearStudentUI();

        if (searchField.text == "")
        {
            foreach (Student student in selectableStudents)
            {
                GameObject studentCard = Instantiate(studentPortraitPrefab, studentListParent.transform);
                StudentUIData studentUIData = studentCard.GetComponent<StudentUIData>();
                studentUIData.SetData(student, currentSortingMode);
                studentUIData.OnStudentClicked += HandleStudentSelection;
                studentUIDatas.Add(studentUIData);
            }
        }
        else
        {
            foreach (Student student in selectableStudents)
            {
                if (student.name.ToLower().StartsWith(searchField.text.ToLower()))
                {
                    GameObject studentCard = Instantiate(studentPortraitPrefab, studentListParent.transform);
                    StudentUIData studentUIData = studentCard.GetComponent<StudentUIData>();
                    studentUIData.SetData(student, currentSortingMode);
                    studentUIData.OnStudentClicked += HandleStudentSelection;
                    studentUIDatas.Add(studentUIData);
                }
            }
        }


    }

    public void CloseSelectionPanel()
    {
        currentSelectedStudent = null;
        if (selectionMode == SelectionMode.Training)
        {
            trainingPanel.SetActive(true);
            GameObject.FindGameObjectWithTag("Student Parent").GetComponent<StudentSpawner>().InitializeStudents();
        }

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

    public void ToggleFiterPanel()
    {
        if (!filterPanel.activeSelf)
        {
            filterPanel.SetActive(true);
        }
        else
        {
            filterPanel.GetComponent<Animator>().SetTrigger("Close");
        }
    }

    public void HandleFilterMode(int filterMode)
    {
        switch (filterMode)
        {
            case 0:
                selectableStudents = SquadController.instance.Students.OrderBy(x => x.name).ToList();
                currentSortingMode = SortingMode.Name;
                break;
            case 1:
                selectableStudents = SquadController.instance.Students.OrderBy(x => x.rarity).ToList();
                currentSortingMode = SortingMode.Rarity;
                break;
            case 2:
                selectableStudents = SquadController.instance.Students.OrderBy(x => x.school).ToList();
                currentSortingMode = SortingMode.School;
                break;
            case 3:
                selectableStudents = SquadController.instance.Students.OrderBy(x => x.club).ToList();
                currentSortingMode = SortingMode.Club;
                break;
            case 4:
                selectableStudents = SquadController.instance.Students.OrderBy(x => x.CurrentPHYStat).ToList();
                currentSortingMode = SortingMode.PHYStat;
                break;
            case 5:
                selectableStudents = SquadController.instance.Students.OrderBy(x => x.CurrentINTStat).ToList();
                currentSortingMode = SortingMode.INTStat;
                break;
            case 6:
                selectableStudents = SquadController.instance.Students.OrderBy(x => x.CurrentCOMStat).ToList();
                currentSortingMode = SortingMode.COMStat;
                break;
            case 7:
                selectableStudents = SquadController.instance.Students.OrderBy(x => x.CurrentStamina).ToList();
                currentSortingMode = SortingMode.Stamina;
                break;
            default:
                break;
        }
        filterPanel.GetComponent<Animator>().SetTrigger("Close");
        ResetSelectionPanel();
    }

    public void ToggleOrderingMode()
    {
        selectableStudents.Reverse();
        ResetSelectionPanel();
    }
}
