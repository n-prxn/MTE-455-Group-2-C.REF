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
        instance = this;
    }

    void Start(){

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Receive(Student s){
        students.Add(s);
    }

    public void ClearStudentList(){
        students.Clear();
    }
}
