using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SquadController : MonoBehaviour, IData
{
    [Header("Students")]
    [SerializeField] private List<Student> students = new List<Student>(); //Main Inventory
    [Header("Pools")]
    [SerializeField] GachaPool gachaPool;
    public List<Student> Students
    {
        get { return students; }
        set { students = value; }
    }
    public static SquadController instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void Receive(Student s)
    {
        s.InitializeStudent();
        students.Add(s);
    }

    public void ClearStudentList()
    {
        students.Clear();
    }

    public void ResetAssignStateAll()
    {
        foreach (Student student in students)
        {
            student.IsAssign = false;
        }
    }

    public int GetAllVacantStudentAmount()
    {
        int amount = 0;
        foreach (Student student in students)
        {
            if (!student.IsAssign && !student.IsOperating && !student.IsTraining)
                amount++;
        }
        return amount;
    }

    public void UpdateStudentBuff()
    {
        foreach (Student student in students)
        {
            if (student.IsBuff)
            {
                student.BuffDuration--;
                if (student.BuffDuration <= 0)
                {
                    student.SetStudentStatToNormal();
                    student.IsBuff = false;
                }
            }
        }
    }

    public void LoadData(GameData data)
    {
        students.Clear();
        foreach (StudentData sData in data.students)
        {
            Student student = gachaPool.StudentsPool[sData.id - 1];
            student.CurrentPHYStat = sData.currentPHYStat;
            student.CurrentINTStat = sData.currentINTStat;
            student.CurrentCOMStat = sData.currentCOMStat;
            student.CurrentStamina = sData.currentStamina;

            student.Collected = sData.collected;
            student.SquadCollect = sData.squadCollect;
            student.IsBuff = sData.isBuff;
            student.BuffDuration = sData.buffDuration;

            if (student.SquadCollect)
                students.Add(student);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.students = new List<StudentData>();
        foreach (Student student in students)
        {
            data.students.Add(new StudentData(student));
        }
    }
}
