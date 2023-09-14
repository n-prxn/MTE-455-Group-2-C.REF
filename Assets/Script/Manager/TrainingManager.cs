using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    [Header("Building")]
    private Dictionary<BuildingType, List<Student>> trainingGroup;
    public Dictionary<BuildingType, List<Student>> TrainingGroup
    {
        get { return trainingGroup; }
        set { trainingGroup = value; }
    }
    private BuildingType currentBuilding;
    public BuildingType CurrentBuilding{
        get {return currentBuilding;}
        set {currentBuilding = value;}
    }

    [Header("Capacity")]
    [SerializeField] private int studentCapacity = 3;
    public int StudentCapacity{
        get{return studentCapacity;}
        set{studentCapacity = value;}
    }
    public static TrainingManager instance;
    void Awake()
    {
        instance = this;
        InitializeTrainingGroup();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeTrainingGroup()
    {
        if (trainingGroup == null)
        {
            trainingGroup = new Dictionary<BuildingType, List<Student>>();
            List<Student> students = new List<Student>();
            for (int i = 0; i < studentCapacity; i++)
                students.Add(null);

            trainingGroup.Add(BuildingType.Gym, students);
            trainingGroup.Add(BuildingType.Library, students);
            trainingGroup.Add(BuildingType.Cafe, students);
            trainingGroup.Add(BuildingType.Dormitory, students);
        }
    }

    public List<Student> GetCurrentStudentsInBuilding(){
        return trainingGroup[currentBuilding];
    }

    public void SetStudentInBuilding(int slot, Student student){
        trainingGroup[currentBuilding][slot] = student;
    }
}
