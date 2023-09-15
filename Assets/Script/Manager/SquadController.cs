using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    [Header("Students")]
    [SerializeField] private List<Student> students; //Main Inventory
    public List<Student> Students{
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
        InitializeStudentStat();
    }

    void Start(){

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeStudentStat(){
        foreach(Student student in students){
            student.CurrentPHYStat = student.phyStat;
            student.CurrentINTStat = student.intStat;
            student.CurrentCOMStat = student.comStat;
            student.CurrentStamina = student.stamina;

            student.IsAssign = false;
            student.IsTraining = false;
        }
    }

    public void Receive(Student s){
        students.Add(s);
    }

    public void ClearStudentList(){
        students.Clear();
    }

    public void ResetAssignStateAll(){
        foreach(Student student in students){
            student.IsAssign = false;
        }
    }
}
