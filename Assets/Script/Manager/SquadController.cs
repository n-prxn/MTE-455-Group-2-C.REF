using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class SquadController : MonoBehaviour, IData
{
    [Header("Students")]
    [SerializeField] private List<Student> students; //Main Inventory
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
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Receive(Student s)
    {
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

    public void LoadData(GameData data)
    {
        students.Clear();
        foreach(Student student in gachaPool.StudentsPool){
            bool isSquadCollect;
            data.studentSquad.TryGetValue(student.id, out isSquadCollect);
            student.SquadCollect = isSquadCollect;
            
            if(student.SquadCollect)
                students.Add(student);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.studentSquad.Clear();
        foreach(Student student in gachaPool.StudentsPool){
            data.studentSquad.Add(student.id, student.SquadCollect);
            EditorUtility.SetDirty(student);
        }
    }
}
